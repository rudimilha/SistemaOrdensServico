using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaOrdensServico.Data;
using SistemaOrdensServico.Models;
using SistemaOrdensServico.Services.Interfaces;

namespace SistemaOrdensServico.Controllers
{
    public class EquipamentosController(
        IEquipamentoService equipamentoService,
        ApplicationDbContext context) : Controller
    {
        private readonly IEquipamentoService _equipamentoService = equipamentoService;
        private readonly ApplicationDbContext _context = context;

        public async Task<IActionResult> Index(string? termoBusca)
        {
            var equipamentos = await _equipamentoService.ListarAsync(termoBusca);
            return View(equipamentos);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var equipamento = await _equipamentoService.ObterPorIdAsync(id);

            if (equipamento == null)
                return NotFound();

            return View(equipamento);
        }

        public async Task<IActionResult> Cadastrar()
        {
            await CarregarClientes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Equipamento equipamento)
        {
            if (!ModelState.IsValid)
            {
                await CarregarClientes(equipamento.ClienteId);
                return View(equipamento);
            }

            await _equipamentoService.CriarAsync(equipamento);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int id)
        {
            var equipamento = await _equipamentoService.ObterPorIdAsync(id);

            if (equipamento == null)
                return NotFound();

            await CarregarClientes(equipamento.ClienteId);
            return View(equipamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Equipamento equipamento)
        {
            if (id != equipamento.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await CarregarClientes(equipamento.ClienteId);
                return View(equipamento);
            }

            var atualizado = await _equipamentoService.AtualizarAsync(equipamento);

            if (!atualizado)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var equipamento = await _equipamentoService.ObterPorIdAsync(id);

            if (equipamento == null)
                return NotFound();

            return View(equipamento);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarExclusao(int id)
        {
            var excluido = await _equipamentoService.ExcluirAsync(id);

            if (!excluido)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarClientes(int? clienteSelecionado = null)
        {
            var clientes = await _context.Clientes
                .AsNoTracking()
                .OrderBy(c => c.Nome)
                .ToListAsync();

            ViewBag.ClienteId = new SelectList(
                clientes, "Id", "Nome", clienteSelecionado);
        }
    }
}