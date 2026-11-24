using SistemaOrdensServico.Models;
using SistemaOrdensServico.Repositories.Interfaces;
using SistemaOrdensServico.Services.Interfaces;

namespace SistemaOrdensServico.Services
{
    public class ServicoService(IServicoRepository repositorio)
        : IServicoService
    {
        public async Task<IReadOnlyList<Servico>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default)
        {
            return await repositorio.ListarAsync(termoBusca, cancellationToken);
        }

        public async Task<Servico?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await repositorio.ObterPorIdAsync(id, cancellationToken);
        }

        public async Task CriarAsync(
            Servico servico,
            CancellationToken cancellationToken = default)
        {
            await repositorio.AdicionarAsync(servico, cancellationToken);
            await repositorio.SalvarAsync(cancellationToken);
        }

        public async Task<bool> AtualizarAsync(
            Servico servico,
            CancellationToken cancellationToken = default)
        {
            if (!await repositorio.ExisteAlgumAsync(servico.Id, cancellationToken))
                return false;

            repositorio.Atualizar(servico);
            await repositorio.SalvarAsync(cancellationToken);

            return true;
        }

        public async Task<bool> ExcluirAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var servico = await repositorio.ObterPorIdAsync(id, cancellationToken);

            if (servico == null)
                return false;

            repositorio.Remover(servico);
            await repositorio.SalvarAsync(cancellationToken);

            return true;
        }
    }
}