
using Microsoft.EntityFrameworkCore;
using SistemaOrdensServico.Data;
using SistemaOrdensServico.Models;
using SistemaOrdensServico.Repositories.Interfaces;

namespace SistemaOrdensServico.Repositories
{
    public class TecnicoRepository(ApplicationDbContext context)
        : ITecnicoRepository
    {
        public async Task<IReadOnlyList<Tecnico>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default)
        {
            var consulta = context.Tecnicos.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(termoBusca))
            {
                var termo = termoBusca.Trim();

                consulta = consulta.Where(tecnico =>
                    tecnico.Nome.Contains(termo) ||
                    tecnico.Especialidade.Contains(termo));
            }

            return await consulta
                .OrderBy(tecnico => tecnico.Nome)
                .ToListAsync(cancellationToken);
        }

        public async Task<Tecnico?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.Tecnicos
                .FirstOrDefaultAsync(tecnico => tecnico.Id == id, cancellationToken);
        }

        public async Task<Tecnico?> ObterPorIdParaLeituraAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.Tecnicos
                .AsNoTracking()
                .FirstOrDefaultAsync(tecnico => tecnico.Id == id, cancellationToken);
        }

        public async Task<bool> ExisteAlgumAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.Tecnicos
                .AnyAsync(tecnico => tecnico.Id == id, cancellationToken);
        }

        public async Task AdicionarAsync(
            Tecnico tecnico,
            CancellationToken cancellationToken = default)
        {
            await context.Tecnicos.AddAsync(tecnico, cancellationToken);
        }

        public void Remover(Tecnico tecnico)
        {
            context.Tecnicos.Remove(tecnico);
        }

        public void Atualizar(Tecnico tecnico)
        {
            context.Tecnicos.Update(tecnico);
        }

        public async Task SalvarAsync(
            CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
