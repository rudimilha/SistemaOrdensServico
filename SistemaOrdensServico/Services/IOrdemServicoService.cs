using SistemaOrdensServico.Models;

namespace SistemaOrdensServico.Services.Interfaces
{
    public interface IOrdemServicoService
    {
        Task<IReadOnlyList<OrdemServico>> ListarAsync(
            string? termoBusca,
            string? status,
            CancellationToken cancellationToken = default);

        Task<OrdemServico?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task CriarAsync(
            OrdemServico ordemServico,
            CancellationToken cancellationToken = default);

        Task<bool> AtualizarAsync(
            OrdemServico ordemServico,
            CancellationToken cancellationToken = default);

        Task<bool> AtualizarStatusAsync(
            int id,
            string novoStatus,
            CancellationToken cancellationToken = default);

        Task<bool> ReabrirAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<bool> ExcluirAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}