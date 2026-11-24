using SistemaOrdensServico.Models;

namespace SistemaOrdensServico.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<IReadOnlyList<Cliente>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default);

        Task<Cliente?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<Cliente?> ObterPorIdParaLeituraAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<bool> ExisteAlgumAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<bool> PossuiVinculosAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            Cliente cliente,
            CancellationToken cancellationToken = default);

        void Remover(Cliente cliente);

        void Atualizar(Cliente cliente);

        Task SalvarAsync(
            CancellationToken cancellationToken = default);
    }
}