using Microsoft.AspNetCore.Mvc;
using reserva_de_salas.Interfaces;
using reserva_de_salas.Models;

namespace reserva_de_salas.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: /Usuario
        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return View(usuarios);
        }

        // GET: /Usuario/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: /Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);
            try
            {
                await _usuarioService.CreateAsync(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                // captura o "E-mail já cadastrado."
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(usuario);
            }
        }

        // GET: /Usuario/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: /Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            await _usuarioService.UpdateAsync(usuario);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Usuario/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: /Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _usuarioService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
