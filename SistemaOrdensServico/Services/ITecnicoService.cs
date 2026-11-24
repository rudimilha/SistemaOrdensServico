using SistemaOrdensServico.Models;

namespace SistemaOrdensServico.Services.Interfaces
{
    public interface ITecnicoService
    {
        Task<IReadOnlyList<Tecnico>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default);

        Task<Tecnico?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task CriarAsync(
            Tecnico tecnico,
            CancellationToken cancellationToken = default);

        Task<bool> AtualizarAsync(
            Tecnico tecnico,
            CancellationToken cancellationToken = default);

        Task<bool> ExcluirAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}