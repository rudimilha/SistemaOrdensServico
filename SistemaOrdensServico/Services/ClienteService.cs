using SistemaOrdensServico.Models;
using SistemaOrdensServico.Repositories.Interfaces;
using SistemaOrdensServico.Services.Interfaces;

namespace SistemaOrdensServico.Services
{
    public class ClienteService(IClienteRepository repositorio)
        : IClienteService
    {
        public async Task<IReadOnlyList<Cliente>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default)
        {
            return await repositorio.ListarAsync(
                termoBusca,
                cancellationToken);
        }

        public async Task<Cliente?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await repositorio.ObterPorIdAsync(
                id,
                cancellationToken);
        }

        public async Task CriarAsync(
            Cliente cliente,
            CancellationToken cancellationToken = default)
        {
            await repositorio.AdicionarAsync(cliente, cancellationToken);
            await repositorio.SalvarAsync(cancellationToken);
        }

        public async Task<bool> AtualizarAsync(
            Cliente cliente,
            CancellationToken cancellationToken = default)
        {
            if (!await repositorio.ExisteAlgumAsync(
                cliente.Id, cancellationToken))
            {
                return false;
            }

            repositorio.Atualizar(cliente);

            await repositorio.SalvarAsync(cancellationToken);

            return true;
        }

        public async Task<ResultadoExclusaoCliente> ExcluirAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var cliente = await repositorio.ObterPorIdAsync(
                id,
                cancellationToken);

            if (cliente == null)
                return ResultadoExclusaoCliente.NaoEncontrado;

            var possuiVinculos = await repositorio.PossuiVinculosAsync(
                id,
                cancellationToken);

            if (possuiVinculos)
                return ResultadoExclusaoCliente.PossuiVinculos;

            repositorio.Remover(cliente);

            await repositorio.SalvarAsync(cancellationToken);

            return ResultadoExclusaoCliente.Excluido;
        }
    }
}