
using SistemaOrdensServico.Models;

namespace SistemaOrdensServico.Repositories.Interfaces
{
    public interface ITecnicoRepository
    {
        Task<IReadOnlyList<Tecnico>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default);

        Task<Tecnico?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<Tecnico?> ObterPorIdParaLeituraAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<bool> ExisteAlgumAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            Tecnico tecnico,
            CancellationToken cancellationToken = default);

        void Remover(Tecnico tecnico);

        void Atualizar(Tecnico tecnico);

        Task SalvarAsync(
            CancellationToken cancellationToken = default);
    }
}
