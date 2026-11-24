using Microsoft.AspNetCore.Mvc;
using SistemaOrdensServico.Models;
using SistemaOrdensServico.Services.Interfaces;

namespace SistemaOrdensServico.Controllers
{
    public class ServicosController(IServicoService servicoService)
        : Controller
    {
        private readonly IServicoService _servicoService = servicoService;

        public async Task<IActionResult> Index(string? termoBusca)
        {
            var servicos = await _servicoService.ListarAsync(termoBusca);
            return View(servicos);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var servico = await _servicoService.ObterPorIdAsync(id);

            if (servico == null)
                return NotFound();

            return View(servico);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Servico servico)
        {
            if (!ModelState.IsValid)
                return View(servico);

            await _servicoService.CriarAsync(servico);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int id)
        {
            var servico = await _servicoService.ObterPorIdAsync(id);

            if (servico == null)
                return NotFound();

            return View(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Servico servico)
        {
            if (id != servico.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(servico);

            var atualizado = await _servicoService.AtualizarAsync(servico);

            if (!atualizado)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var servico = await _servicoService.ObterPorIdAsync(id);

            if (servico == null)
                return NotFound();

            return View(servico);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarExclusao(int id)
        {
            var excluido = await _servicoService.ExcluirAsync(id);

            if (!excluido)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}