
using SistemaOrdensServico.Models;

namespace SistemaOrdensServico.Repositories.Interfaces
{
    public interface IServicoRepository
    {
        Task<IReadOnlyList<Servico>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default);

        Task<Servico?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<Servico?> ObterPorIdParaLeituraAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<bool> ExisteAlgumAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            Servico servico,
            CancellationToken cancellationToken = default);

        void Remover(Servico servico);

        void Atualizar(Servico servico);

        Task SalvarAsync(
            CancellationToken cancellationToken = default);
    }
}
