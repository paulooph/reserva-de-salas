using reserva_de_salas.Interfaces;
using reserva_de_salas.Models;

namespace reserva_de_salas.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository repo)
            => _usuarioRepository = repo;

        public async Task<IEnumerable<Usuario>> GetAllAsync()
            => await _usuarioRepository.GetAllAsync();

        public async Task<Usuario> GetByIdAsync(long id)
            => await _usuarioRepository.GetByIdAsync(id);

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            var existente = await _usuarioRepository.GetByEmailAsync(usuario.Email);
            if (existente != null)
                throw new InvalidOperationException("E‑mail já cadastrado.");

            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();
            return usuario;
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _usuarioRepository.Update(usuario);
            await _usuarioRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            _usuarioRepository.Delete(usuario);
            await _usuarioRepository.SaveChangesAsync();
        }
    }
}

