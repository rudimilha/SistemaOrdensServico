using SistemaOrdensServico.Models;

namespace SistemaOrdensServico.Services.Interfaces
{
    public interface IServicoService
    {
        Task<IReadOnlyList<Servico>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default);

        Task<Servico?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task CriarAsync(
            Servico servico,
            CancellationToken cancellationToken = default);

        Task<bool> AtualizarAsync(
            Servico servico,
            CancellationToken cancellationToken = default);

        Task<bool> ExcluirAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}