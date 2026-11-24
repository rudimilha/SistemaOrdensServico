
using Microsoft.EntityFrameworkCore;
using SistemaOrdensServico.Data;
using SistemaOrdensServico.Models;
using SistemaOrdensServico.Repositories.Interfaces;

namespace SistemaOrdensServico.Repositories
{
    public class EquipamentoRepository(ApplicationDbContext context)
        : IEquipamentoRepository
    {

        public async Task<IReadOnlyList<Equipamento>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Equipamento> consulta = context.Equipamentos
                .AsNoTracking()
                .Include(equipamento => equipamento.Cliente);

            if (!string.IsNullOrWhiteSpace(termoBusca))
            {
                var termo = termoBusca.Trim();

                consulta = consulta.Where(equipamento =>
                    equipamento.Nome.Contains(termo) ||
                    equipamento.Marca.Contains(termo) ||
                    equipamento.Modelo.Contains(termo));
            }

            return await consulta
                .OrderBy(equipamento => equipamento.Nome)
                .ToListAsync(cancellationToken);
        }


        public async Task<Equipamento?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.Equipamentos
                .Include(equipamento => equipamento.Cliente)
                .FirstOrDefaultAsync(equipamento => equipamento.Id == id, cancellationToken);
        }

        public async Task<Equipamento?> ObterPorIdParaLeituraAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.Equipamentos
                .AsNoTracking()
                .Include(equipamento => equipamento.Cliente)
                .FirstOrDefaultAsync(equipamento => equipamento.Id == id, cancellationToken);
        }

        public async Task<bool> ExisteAlgumAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.Equipamentos
                .AnyAsync(equipamento => equipamento.Id == id, cancellationToken);
        }

        public async Task AdicionarAsync(
            Equipamento equipamento,
            CancellationToken cancellationToken = default)
        {
            await context.Equipamentos.AddAsync(equipamento, cancellationToken);
        }

        public void Remover(Equipamento equipamento)
        {
            context.Equipamentos.Remove(equipamento);
        }

        public void Atualizar(Equipamento equipamento)
        {
            context.Equipamentos.Update(equipamento);
        }

        public async Task SalvarAsync(
            CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
