using Microsoft.AspNetCore.Mvc;
using reserva_de_salas.Interfaces;
using reserva_de_salas.Models;

namespace reserva_de_salas.Controllers
{
    public class SalaController : Controller
    {
        private readonly ISalaService _salaService;

        public SalaController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        // GET: /Salas
        public async Task<IActionResult> Index()
        {
            var salas = await _salaService.GetAllSalasAsync();
            return View(salas);
        }

        // GET: /Salas/Details/{id}
        public async Task<IActionResult> Details(long id)
        {
            var sala = await _salaService.GetSalaByIdAsync(id);
            if (sala == null)
            {
                TempData["ErrorMessage"] = "Sala não encontrada.";
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        // GET: /Salas/Create
        public IActionResult Create()
        {
            return View(new Sala()); // Passa uma nova instância de Sala para o formulário
        }

        // POST: /Salas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Capacidade,Recursos")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _salaService.SaveSalaAsync(sala);
                    TempData["SuccessMessage"] = "Sala criada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao criar sala: " + ex.Message);
                }
            }
            return View(sala);
        }

        // GET: /Salas/Edit/{id}
        public async Task<IActionResult> Edit(long id)
        {
            var sala = await _salaService.GetSalaByIdAsync(id);
            if (sala == null)
            {
                TempData["ErrorMessage"] = "Sala não encontrada para edição.";
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        // POST: /Salas/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nome,Capacidade,Recursos")] Sala sala)
        {
            if (id != sala.Id)
            {
                TempData["ErrorMessage"] = "ID da sala inconsistente.";
                return RedirectToAction(nameof(Index)); // Ou Bad Request, dependendo da sua preferência
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _salaService.SaveSalaAsync(sala);
                    TempData["SuccessMessage"] = "Sala atualizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao atualizar sala: " + ex.Message);
                }
            }
            return View(sala);
        }

        // GET: /Salas/Delete/{id}
        public async Task<IActionResult> Delete(long id)
        {
            var sala = await _salaService.GetSalaByIdAsync(id);
            if (sala == null)
            {
                TempData["ErrorMessage"] = "Sala não encontrada para exclusão.";
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        // POST: /Salas/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                await _salaService.DeleteSalaAsync(id);
                TempData["SuccessMessage"] = "Sala excluída com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao excluir sala: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

