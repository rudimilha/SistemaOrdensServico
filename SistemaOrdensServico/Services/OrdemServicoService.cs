using SistemaOrdensServico.Models;
using SistemaOrdensServico.Repositories.Interfaces;
using SistemaOrdensServico.Services.Interfaces;

namespace SistemaOrdensServico.Services
{
    public class OrdemServicoService(IOrdemServicoRepository repositorio)
        : IOrdemServicoService
    {
        public async Task<IReadOnlyList<OrdemServico>> ListarAsync(
            string? termoBusca,
            string? status,
            CancellationToken cancellationToken = default)
        {
            return await repositorio.ListarAsync(
                termoBusca, status, cancellationToken);
        }

        public async Task<OrdemServico?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await repositorio.ObterPorIdAsync(id, cancellationToken);
        }

        public async Task CriarAsync(
            OrdemServico ordemServico,
            CancellationToken cancellationToken = default)
        {
            ordemServico.DataAbertura = DateTime.Now;
            ordemServico.DataConclusao = null;
            ordemServico.Status = "Aberta";

            await repositorio.AdicionarAsync(ordemServico, cancellationToken);
            await repositorio.SalvarAsync(cancellationToken);
        }

        public async Task<bool> AtualizarAsync(
            OrdemServico dados,
            CancellationToken cancellationToken = default)
        {
            var ordem = await repositorio.ObterPorIdAsync(
                dados.Id, cancellationToken);

            if (ordem == null || ordem.Status == "Concluída")
                return false;

            ordem.Descricao = dados.Descricao;
            ordem.Diagnostico = dados.Diagnostico;
            ordem.Solucao = dados.Solucao;
            ordem.ClienteId = dados.ClienteId;
            ordem.EquipamentoId = dados.EquipamentoId;
            ordem.TecnicoId = dados.TecnicoId;
            ordem.ServicoId = dados.ServicoId;

            await repositorio.SalvarAsync(cancellationToken);

            return true;
        }

        public async Task<bool> AtualizarStatusAsync(
            int id,
            string novoStatus,
            CancellationToken cancellationToken = default)
        {
            var ordem = await repositorio.ObterPorIdAsync(
                id, cancellationToken);

            if (ordem == null || ordem.Status == "Concluída")
                return false;

            if (novoStatus != "Em andamento" &&
                novoStatus != "Concluída")
                return false;

            ordem.Status = novoStatus;

            if (novoStatus == "Concluída")
            {
                ordem.DataConclusao = DateTime.Now;
            }
            else
            {
                ordem.DataConclusao = null;
            }

            await repositorio.SalvarAsync(cancellationToken);

            return true;
        }

        public async Task<bool> ReabrirAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var ordem = await repositorio.ObterPorIdAsync(
                id, cancellationToken);

            if (ordem == null || ordem.Status != "Concluída")
                return false;

            ordem.Status = "Reaberta";
            ordem.DataConclusao = null;

            await repositorio.SalvarAsync(cancellationToken);

            return true;
        }

        public async Task<bool> ExcluirAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var ordem = await repositorio.ObterPorIdAsync(
                id, cancellationToken);

            if (ordem == null || ordem.Status == "Concluída")
                return false;

            repositorio.Remover(ordem);
            await repositorio.SalvarAsync(cancellationToken);

            return true;
        }
    }
}