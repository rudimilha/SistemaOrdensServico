using SistemaOrdensServico.Models;
using SistemaOrdensServico.Repositories.Interfaces;
using SistemaOrdensServico.Services.Interfaces;

namespace SistemaOrdensServico.Services
{
    public class TecnicoService(ITecnicoRepository repositorio)
        : ITecnicoService
    {
        public async Task<IReadOnlyList<Tecnico>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default)
        {
            return await repositorio.ListarAsync(termoBusca, cancellationToken);
        }

        public async Task<Tecnico?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await repositorio.ObterPorIdAsync(id, cancellationToken);
        }

        public async Task CriarAsync(
            Tecnico tecnico,
            CancellationToken cancellationToken = default)
        {
            await repositorio.AdicionarAsync(tecnico, cancellationToken);
            await repositorio.SalvarAsync(cancellationToken);
        }

        public async Task<bool> AtualizarAsync(
            Tecnico tecnico,
            CancellationToken cancellationToken = default)
        {
            if (!await repositorio.ExisteAlgumAsync(tecnico.Id, cancellationToken))
                return false;

            repositorio.Atualizar(tecnico);
            await repositorio.SalvarAsync(cancellationToken);

            return true;
        }

        public async Task<bool> ExcluirAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var tecnico = await repositorio.ObterPorIdAsync(id, cancellationToken);

            if (tecnico == null)
                return false;

            repositorio.Remover(tecnico);
            await repositorio.SalvarAsync(cancellationToken);

            return true;
        }
    }
}