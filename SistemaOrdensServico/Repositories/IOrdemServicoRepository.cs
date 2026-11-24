
using SistemaOrdensServico.Models;

namespace SistemaOrdensServico.Repositories.Interfaces
{
    public interface IOrdemServicoRepository
    {
        Task<IReadOnlyList<OrdemServico>> ListarAsync(
            string? termoBusca,
            string? status,
            CancellationToken cancellationToken = default);

        Task<OrdemServico?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<OrdemServico?> ObterPorIdParaLeituraAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<bool> ExisteAlgumAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            OrdemServico ordemServico,
            CancellationToken cancellationToken = default);

        void Remover(OrdemServico ordemServico);

        void Atualizar(OrdemServico ordemServico);

        Task SalvarAsync(
            CancellationToken cancellationToken = default);
    }
}
