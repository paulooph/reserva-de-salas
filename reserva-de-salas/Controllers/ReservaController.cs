using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using reserva_de_salas.Models;
using reserva_de_salas.Services;

namespace reserva_de_salas.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ReservasFacade _facade;
        public ReservaController(ReservasFacade facade) => _facade = facade;

        public async Task<IActionResult> Index()
        {
            ViewBag.Indicadores = await _facade.GetIndicadoresAsync();
            return View(await _facade.ListarReservasAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = new SelectList(await _facade.ListarUsuariosAsync(), "Id", "Email");
            ViewBag.Salas = new SelectList(await _facade.ListarSalasAsync(), "Id", "Nome");
            var r = new Reserva
            {
                Data = System.DateTime.Today,
                HoraInicio = System.TimeSpan.FromHours(8),
                HoraFim = System.TimeSpan.FromHours(9)
            };
            return View(r);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserva model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Usuarios = new SelectList(await _facade.ListarUsuariosAsync(), "Id", "Email", model.UsuarioId);
                ViewBag.Salas = new SelectList(await _facade.ListarSalasAsync(), "Id", "Nome", model.SalaId);
                return View(model);
            }

            var msg = await _facade.ReservarAsync(model);
            TempData[msg.Contains("sucesso") ? "SuccessMessage" : "ErrorMessage"] = msg;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var r = await _facade.GetByIdAsync(id);
            ViewBag.Usuarios = new SelectList(await _facade.ListarUsuariosAsync(), "Id", "Email", r.UsuarioId);
            ViewBag.Salas = new SelectList(await _facade.ListarSalasAsync(), "Id", "Nome", r.SalaId);
            return View(r);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Reserva model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Usuarios = new SelectList(await _facade.ListarUsuariosAsync(), "Id", "Email", model.UsuarioId);
                ViewBag.Salas = new SelectList(await _facade.ListarSalasAsync(), "Id", "Nome", model.SalaId);
                return View(model);
            }

            var msg = await _facade.AtualizarAsync(model);
            TempData[msg.Contains("sucesso") ? "SuccessMessage" : "ErrorMessage"] = msg;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            var r = await _facade.GetByIdAsync(id);
            return View(r);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _facade.DeleteAsync(id);
            TempData["SuccessMessage"] = "Reserva excluída com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(long id)
        {
            var r = await _facade.GetByIdAsync(id);
            return View(r);
        }
    }
}
