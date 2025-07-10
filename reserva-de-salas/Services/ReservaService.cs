using reserva_de_salas.Interfaces;
using reserva_de_salas.Models;
using reserva_de_salas.Services.Strategy; 

namespace reserva_de_salas.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _repo;
        private IValidadorDeReservaStrategy _validator;

        public ReservaService(IReservaRepository repo)
            => _repo = repo;

        public async Task<IEnumerable<Reserva>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<Reserva> GetByIdAsync(long id)
            => await _repo.GetByIdAsync(id)
               ?? throw new InvalidOperationException("Reserva não existe");

        public async Task<Reserva> SaveAsync(Reserva r)
        {
            if (r.Id == 0) await _repo.AddAsync(r);
            else _repo.Update(r);

            await _repo.SaveChangesAsync();
            return r;
        }

        public async Task DeleteAsync(long id)
        {
            var r = await _repo.GetByIdAsync(id)
                ?? throw new InvalidOperationException("Reserva não existe");
            _repo.Delete(r);
            await _repo.SaveChangesAsync();
        }

        public void SetValidator(IValidadorDeReservaStrategy v)
            => _validator = v;

        public async Task<bool> ValidateAsync(Reserva r)
            => _validator != null
               ? await _validator.Validar(r)
               : throw new InvalidOperationException("Validador não definido");
    }
}