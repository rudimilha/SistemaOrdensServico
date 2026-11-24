using SistemaOrdensServico.Models;

namespace SistemaOrdensServico.Services.Interfaces
{
    public interface IEquipamentoService
    {
        Task<IReadOnlyList<Equipamento>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default);

        Task<Equipamento?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task CriarAsync(
            Equipamento equipamento,
            CancellationToken cancellationToken = default);

        Task<bool> AtualizarAsync(
            Equipamento equipamento,
            CancellationToken cancellationToken = default);

        Task<bool> ExcluirAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}