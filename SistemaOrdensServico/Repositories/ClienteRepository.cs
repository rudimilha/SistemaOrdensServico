using Microsoft.EntityFrameworkCore;
using SistemaOrdensServico.Data;
using SistemaOrdensServico.Models;
using SistemaOrdensServico.Repositories.Interfaces;

namespace SistemaOrdensServico.Repositories
{
    public class ClienteRepository(ApplicationDbContext context)
        : IClienteRepository
    {
        public async Task<IReadOnlyList<Cliente>> ListarAsync(
            string? termoBusca,
            CancellationToken cancellationToken = default)
        {
            var consulta = context.Clientes.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(termoBusca))
            {
                var termo = termoBusca.Trim();

                consulta = consulta.Where(cliente =>
                    cliente.Nome.Contains(termo) ||
                    cliente.Email.Contains(termo) ||
                    cliente.Telefone.Contains(termo));
            }

            return await consulta
                .OrderBy(cliente => cliente.Nome)
                .ToListAsync(cancellationToken);
        }

        public async Task<Cliente?> ObterPorIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.Clientes
                .FirstOrDefaultAsync(
                    cliente => cliente.Id == id,
                    cancellationToken);
        }

        public async Task<Cliente?> ObterPorIdParaLeituraAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    cliente => cliente.Id == id,
                    cancellationToken);
        }

        public async Task<bool> ExisteAlgumAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await context.Clientes
                .AnyAsync(cliente => cliente.Id == id, cancellationToken);
        }

        public async Task<bool> PossuiVinculosAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var possuiEquipamentos = await context.Equipamentos
                .AnyAsync(equipamento =>
                    equipamento.ClienteId == id,
                    cancellationToken);

            if (possuiEquipamentos)
                return true;

            var possuiOrdensServico = await context.OrdensServico
                .AnyAsync(ordem =>
                    ordem.ClienteId == id,
                    cancellationToken);

            return possuiOrdensServico;
        }

        public async Task AdicionarAsync(
            Cliente cliente,
            CancellationToken cancellationToken = default)
        {
            await context.Clientes.AddAsync(cliente, cancellationToken);
        }

        public void Remover(Cliente cliente)
        {
            context.Clientes.Remove(cliente);
        }

        public void Atualizar(Cliente cliente)
        {
            context.Clientes.Update(cliente);
        }

        public async Task SalvarAsync(
            CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}