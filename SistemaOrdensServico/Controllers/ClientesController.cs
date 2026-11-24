using Microsoft.AspNetCore.Mvc;
using SistemaOrdensServico.Models;
using SistemaOrdensServico.Services.Interfaces;

namespace SistemaOrdensServico.Controllers
{
    public class ClientesController(IClienteService clienteService)
        : Controller
    {
        private readonly IClienteService _clienteService = clienteService;

        // GET: Clientes
        public async Task<IActionResult> Index(string? termoBusca)
        {
            var clientes = await _clienteService.ListarAsync(termoBusca);

            return View(clientes);
        }

        // GET: Clientes/Detalhes/5
        public async Task<IActionResult> Detalhes(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // GET: Clientes/Cadastrar
        public IActionResult Cadastrar()
        {
            return View();
        }

        // POST: Clientes/Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            await _clienteService.CriarAsync(cliente);

            return RedirectToAction(nameof(Index));
        }

        // GET: Clientes/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // POST: Clientes/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Cliente cliente)
        {
            if (id != cliente.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(cliente);

            var atualizado = await _clienteService.AtualizarAsync(cliente);

            if (!atualizado)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: Clientes/Excluir/5
        public async Task<IActionResult> Excluir(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // POST: Clientes/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarExclusao(int id)
        {
            var resultado = await _clienteService.ExcluirAsync(id);

            switch (resultado)
            {
                case ResultadoExclusaoCliente.Excluido:
                    return RedirectToAction(nameof(Index));

                case ResultadoExclusaoCliente.NaoEncontrado:
                    return NotFound();

                case ResultadoExclusaoCliente.PossuiVinculos:
                    TempData["ErroExclusaoCliente"] =
                        "Não é possível excluir este cliente porque existem equipamentos ou ordens de serviço vinculados a ele.";

                    return RedirectToAction(nameof(Excluir), new { id });

                default:
                    return BadRequest();
            }
        }
    }
}