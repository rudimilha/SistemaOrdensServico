
using Microsoft.EntityFrameworkCore;
using SistemaOrdensServico.Data;
using SistemaOrdensServico.Models;
using SistemaOrdensServico.Repositories.Interfaces;

namespace SistemaOrdensServico.Repositories
{
    public class ServicoRepository(ApplicationDbContext context)
        : IServicoRepository
    {
        public async Task<IReadOnlyList<Servico>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default)
        {
            var consulta = context.Servicos.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(termoBusca))
            {
                var termo = termoBusca.Trim();

                consulta = consulta.Where(servico =>
                    servico.Nome.Contains(termo) ||
                    servico.Descricao.Contains(termo));
            }

            return await consulta
                .OrderBy(servico => servico.Nome)
                .ToListAsync(cancellationToken);
        }

        public async Task<Servico?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.Servicos
                .FirstOrDefaultAsync(servico => servico.Id == id, cancellationToken);
        }

        public async Task<Servico?> ObterPorIdParaLeituraAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.Servicos
                .AsNoTracking()
                .FirstOrDefaultAsync(servico => servico.Id == id, cancellationToken);
        }

        public async Task<bool> ExisteAlgumAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.Servicos
                .AnyAsync(servico => servico.Id == id, cancellationToken);
        }

        public async Task AdicionarAsync(
            Servico servico,
            CancellationToken cancellationToken = default)
        {
            await context.Servicos.AddAsync(servico, cancellationToken);
        }

        public void Remover(Servico servico)
        {
            context.Servicos.Remove(servico);
        }

        public void Atualizar(Servico servico)
        {
            context.Servicos.Update(servico);
        }

        public async Task SalvarAsync(
            CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
