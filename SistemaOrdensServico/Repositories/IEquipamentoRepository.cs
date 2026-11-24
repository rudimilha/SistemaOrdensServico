
using SistemaOrdensServico.Models;

namespace SistemaOrdensServico.Repositories.Interfaces
{
    public interface IEquipamentoRepository
    {
        Task<IReadOnlyList<Equipamento>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default);

        Task<Equipamento?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<Equipamento?> ObterPorIdParaLeituraAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<bool> ExisteAlgumAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            Equipamento equipamento,
            CancellationToken cancellationToken = default);

        void Remover(Equipamento equipamento);

        void Atualizar(Equipamento equipamento);

        Task SalvarAsync(
            CancellationToken cancellationToken = default);
    }
}
