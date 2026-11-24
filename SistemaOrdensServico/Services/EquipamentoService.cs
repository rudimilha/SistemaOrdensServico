using SistemaOrdensServico.Models;
using SistemaOrdensServico.Repositories.Interfaces;
using SistemaOrdensServico.Services.Interfaces;

namespace SistemaOrdensServico.Services
{
    public class EquipamentoService(IEquipamentoRepository repositorio)
        : IEquipamentoService
    {
        public async Task<IReadOnlyList<Equipamento>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default)
        {
            return await repositorio.ListarAsync(termoBusca, cancellationToken);
        }

        public async Task<Equipamento?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await repositorio.ObterPorIdAsync(id, cancellationToken);
        }

        public async Task CriarAsync(
            Equipamento equipamento,
            CancellationToken cancellationToken = default)
        {
            await repositorio.AdicionarAsync(equipamento, cancellationToken);
            await repositorio.SalvarAsync(cancellationToken);
        }

        public async Task<bool> AtualizarAsync(
            Equipamento equipamento,
            CancellationToken cancellationToken = default)
        {
            if (!await repositorio.ExisteAlgumAsync(equipamento.Id, cancellationToken))
                return false;

            repositorio.Atualizar(equipamento);
            await repositorio.SalvarAsync(cancellationToken);

            return true;
        }

        public async Task<bool> ExcluirAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var equipamento = await repositorio.ObterPorIdAsync(id, cancellationToken);

            if (equipamento == null)
                return false;

            repositorio.Remover(equipamento);
            await repositorio.SalvarAsync(cancellationToken);

            return true;
        }
    }
}