using Microsoft.AspNetCore.Mvc;
using SistemaOrdensServico.Models;
using SistemaOrdensServico.Services.Interfaces;

namespace SistemaOrdensServico.Controllers
{
    public class TecnicosController(ITecnicoService tecnicoService)
        : Controller
    {
        private readonly ITecnicoService _tecnicoService = tecnicoService;

        public async Task<IActionResult> Index(string? termoBusca)
        {
            var tecnicos = await _tecnicoService.ListarAsync(termoBusca);
            return View(tecnicos);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var tecnico = await _tecnicoService.ObterPorIdAsync(id);

            if (tecnico == null)
                return NotFound();

            return View(tecnico);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Tecnico tecnico)
        {
            if (!ModelState.IsValid)
                return View(tecnico);

            await _tecnicoService.CriarAsync(tecnico);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int id)
        {
            var tecnico = await _tecnicoService.ObterPorIdAsync(id);

            if (tecnico == null)
                return NotFound();

            return View(tecnico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Tecnico tecnico)
        {
            if (id != tecnico.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(tecnico);

            var atualizado = await _tecnicoService.AtualizarAsync(tecnico);

            if (!atualizado)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var tecnico = await _tecnicoService.ObterPorIdAsync(id);

            if (tecnico == null)
                return NotFound();

            return View(tecnico);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarExclusao(int id)
        {
            var excluido = await _tecnicoService.ExcluirAsync(id);

            if (!excluido)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}