using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaOrdensServico.Data;
using SistemaOrdensServico.Models;
using SistemaOrdensServico.Services.Interfaces;

namespace SistemaOrdensServico.Controllers
{
    public class OrdensServicoController(
        IOrdemServicoService ordemServicoService,
        ApplicationDbContext context) : Controller
    {
        private readonly IOrdemServicoService _ordemServicoService = ordemServicoService;
        private readonly ApplicationDbContext _context = context;

        public async Task<IActionResult> Index(
            string? termoBusca,
            string? status)
        {
            var ordens = await _ordemServicoService.ListarAsync(
                termoBusca, status);

            ViewBag.TermoBusca = termoBusca;
            ViewBag.Status = status;

            return View(ordens);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var ordem = await _ordemServicoService.ObterPorIdAsync(id);

            if (ordem == null)
                return NotFound();

            return View(ordem);
        }

        public async Task<IActionResult> Cadastrar()
        {
            await CarregarListas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(OrdemServico ordem)
        {
            if (!ModelState.IsValid)
            {
                await CarregarListas(ordem);
                return View(ordem);
            }

            await _ordemServicoService.CriarAsync(ordem);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int id)
        {
            var ordem = await _ordemServicoService.ObterPorIdAsync(id);

            if (ordem == null)
                return NotFound();

            if (ordem.Status == "Concluída")
                return RedirectToAction(nameof(Detalhes), new { id });

            await CarregarListas(ordem);
            return View(ordem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, OrdemServico ordem)
        {
            if (id != ordem.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await CarregarListas(ordem);
                return View(ordem);
            }

            var atualizado = await _ordemServicoService.AtualizarAsync(ordem);

            if (!atualizado)
                return RedirectToAction(nameof(Detalhes), new { id });

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarStatus(
            int id,
            string novoStatus)
        {
            var atualizado = await _ordemServicoService.AtualizarStatusAsync(
                id, novoStatus);

            if (!atualizado)
                return BadRequest();

            return RedirectToAction(nameof(Detalhes), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reabrir(int id)
        {
            var reaberta = await _ordemServicoService.ReabrirAsync(id);

            if (!reaberta)
                return BadRequest();

            return RedirectToAction(nameof(Detalhes), new { id });
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var ordem = await _ordemServicoService.ObterPorIdAsync(id);

            if (ordem == null)
                return NotFound();

            if (ordem.Status == "Concluída")
                return RedirectToAction(nameof(Detalhes), new { id });

            return View(ordem);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarExclusao(int id)
        {
            var excluida = await _ordemServicoService.ExcluirAsync(id);

            if (!excluida)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarListas(OrdemServico? ordem = null)
        {
            var clientes = await _context.Clientes
                .AsNoTracking()
                .OrderBy(c => c.Nome)
                .ToListAsync();

            var equipamentos = await _context.Equipamentos
                .AsNoTracking()
                .OrderBy(e => e.Nome)
                .ToListAsync();

            var tecnicos = await _context.Tecnicos
                .AsNoTracking()
                .OrderBy(t => t.Nome)
                .ToListAsync();

            var servicos = await _context.Servicos
                .AsNoTracking()
                .OrderBy(s => s.Nome)
                .ToListAsync();

            ViewBag.ClienteId = new SelectList(
                clientes, "Id", "Nome", ordem?.ClienteId);

            ViewBag.EquipamentoId = new SelectList(
                equipamentos, "Id", "Nome", ordem?.EquipamentoId);

            ViewBag.TecnicoId = new SelectList(
                tecnicos, "Id", "Nome", ordem?.TecnicoId);

            ViewBag.ServicoId = new SelectList(
                servicos, "Id", "Nome", ordem?.ServicoId);
        }
    }
}