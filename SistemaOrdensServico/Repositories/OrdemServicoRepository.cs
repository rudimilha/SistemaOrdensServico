
using Microsoft.EntityFrameworkCore;
using SistemaOrdensServico.Data;
using SistemaOrdensServico.Models;
using SistemaOrdensServico.Repositories.Interfaces;

namespace SistemaOrdensServico.Repositories
{
    public class OrdemServicoRepository(ApplicationDbContext context)
        : IOrdemServicoRepository
    {

        public async Task<IReadOnlyList<OrdemServico>> ListarAsync(
            string? termoBusca,
            string? status,
            CancellationToken cancellationToken = default)
        {
            IQueryable<OrdemServico> consulta = context.OrdensServico
                .AsNoTracking()
                .Include(ordem => ordem.Cliente)
                .Include(ordem => ordem.Equipamento)
                .Include(ordem => ordem.Tecnico)
                .Include(ordem => ordem.Servico);

            if (!string.IsNullOrWhiteSpace(termoBusca))
            {
                var termo = termoBusca.Trim();

                consulta = consulta.Where(ordem =>
                    ordem.Descricao.Contains(termo) ||
                    (ordem.Diagnostico != null &&
                     ordem.Diagnostico.Contains(termo)));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                consulta = consulta.Where(ordem =>
                    ordem.Status == status);
            }

            return await consulta
                .OrderByDescending(ordem => ordem.DataAbertura)
                .ToListAsync(cancellationToken);
        }


        public async Task<OrdemServico?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.OrdensServico
                .Include(ordem => ordem.Cliente)
                .Include(ordem => ordem.Equipamento)
                .Include(ordem => ordem.Tecnico)
                .Include(ordem => ordem.Servico)
                .FirstOrDefaultAsync(ordem => ordem.Id == id, cancellationToken);
        }

        public async Task<OrdemServico?> ObterPorIdParaLeituraAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.OrdensServico
                .AsNoTracking()
                .Include(ordem => ordem.Cliente)
                .Include(ordem => ordem.Equipamento)
                .Include(ordem => ordem.Tecnico)
                .Include(ordem => ordem.Servico)
                .FirstOrDefaultAsync(ordem => ordem.Id == id, cancellationToken);
        }

        public async Task<bool> ExisteAlgumAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.OrdensServico
                .AnyAsync(ordem => ordem.Id == id, cancellationToken);
        }

        public async Task AdicionarAsync(
            OrdemServico ordemServico,
            CancellationToken cancellationToken = default)
        {
            await context.OrdensServico.AddAsync(ordemServico, cancellationToken);
        }

        public void Remover(OrdemServico ordemServico)
        {
            context.OrdensServico.Remove(ordemServico);
        }

        public void Atualizar(OrdemServico ordemServico)
        {
            context.OrdensServico.Update(ordemServico);
        }

        public async Task SalvarAsync(
            CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
