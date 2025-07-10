using reserva_de_salas.Interfaces;
using reserva_de_salas.Models;

namespace reserva_de_salas.Services.Strategy
{
    public class ValidadorDeReservaCapacidade : IValidadorDeReservaStrategy
    {
        private readonly ISalaRepository _salaRepo;
        public ValidadorDeReservaCapacidade(ISalaRepository salaRepo)
            => _salaRepo = salaRepo;

        public async Task<bool> Validar(Reserva r)
        {
            var sala = await _salaRepo.GetByIdAsync(r.SalaId);
            return r.NumeroDePessoas <= sala.Capacidade;
        }
    }
}
