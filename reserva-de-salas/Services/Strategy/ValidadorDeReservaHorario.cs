using reserva_de_salas.Interfaces;
using reserva_de_salas.Models;

namespace reserva_de_salas.Services.Strategy
{
    public class ValidadorDeReservaHorario : IValidadorDeReservaStrategy
    {
        private readonly IReservaRepository _repo;
        public ValidadorDeReservaHorario(IReservaRepository repo)
            => _repo = repo;

        public async Task<bool> Validar(Reserva r)
        {
            // não permite datas passadas
            if (r.Data.Date < DateTime.Today ||
               (r.Data.Date == DateTime.Today && r.HoraInicio < DateTime.Now.TimeOfDay))
                return false;

            // carrega reservas existentes para a mesma sala/data
            var existentes = await _repo.FindBySalaIdAndDataAsync(r.SalaId, r.Data);
            // ignora a própria reserva em edição
            existentes = existentes.Where(x => x.Id != r.Id).ToList();

            // verifica sobreposição simples
            bool overlap = existentes.Any(x =>
                r.HoraInicio < x.HoraFim && x.HoraInicio < r.HoraFim);

            return !overlap;
        }
    }
}
