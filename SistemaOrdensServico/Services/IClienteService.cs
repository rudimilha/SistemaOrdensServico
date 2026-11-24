using SistemaOrdensServico.Models;

namespace SistemaOrdensServico.Services.Interfaces
{
    public interface IClienteService
    {
        Task<IReadOnlyList<Cliente>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default);

        Task<Cliente?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task CriarAsync(
            Cliente cliente,
            CancellationToken cancellationToken = default);

        Task<bool> AtualizarAsync(
            Cliente cliente,
            CancellationToken cancellationToken = default);

        Task<ResultadoExclusaoCliente> ExcluirAsync(
            int id,
            CancellationToken cancellationToken = default);
    }

    public enum ResultadoExclusaoCliente
    {
        Excluido,
        NaoEncontrado,
        PossuiVinculos
    }
}