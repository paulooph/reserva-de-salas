using reserva_de_salas.Interfaces;
using reserva_de_salas.Models;
using reserva_de_salas.Services.Strategy;

namespace reserva_de_salas.Services
{
    public class ReservasFacade
    {
        private readonly IReservaService _reservaService;
        private readonly IUsuarioService _usuarioService;
        private readonly ISalaService _salaService;
        private readonly IValidadorDeReservaStrategy _validadorHorario;
        private readonly IValidadorDeReservaStrategy _validadorCapacidade;

        public ReservasFacade(
            IReservaService reservaService,
            IUsuarioService usuarioService,
            ISalaService salaService,
            ValidadorDeReservaHorario validadorHorario,
            ValidadorDeReservaCapacidade validadorCapacidade)
        {
            _reservaService = reservaService;
            _usuarioService = usuarioService;
            _salaService = salaService;
            _validadorHorario = validadorHorario;
            _validadorCapacidade = validadorCapacidade;
        }

        public async Task<List<Reserva>> ListarReservasAsync()
            => (await _reservaService.GetAllAsync()).ToList();

        public async Task<List<Usuario>> ListarUsuariosAsync()
            => (await _usuarioService.GetAllAsync()).ToList();

        public async Task<List<Sala>> ListarSalasAsync()
            => (await _salaService.GetAllSalasAsync()).ToList();

        public async Task<Dictionary<string, long>> GetIndicadoresAsync()
            => new()
            {
                ["totalSalas"] = (await _salaService.GetAllSalasAsync()).Count(),
                ["totalUsuarios"] = (await _usuarioService.GetAllAsync()).Count(),
                ["totalReservas"] = (await _reservaService.GetAllAsync()).Count()
            };

        public async Task<string> ReservarAsync(Reserva reserva)
        {
            // 0) Validação de duração mínima
            if (reserva.HoraFim <= reserva.HoraInicio)
            {
                return "A hora de fim deve ser posterior à hora de início.";
            }


            // 1) Data no passado
            if (reserva.Data.Date < DateTime.Today)
            {
                return "A data da reserva não pode ser anterior à data de hoje.";
            }

            // 2) Usuário e sala existem
            if (await _usuarioService.GetByIdAsync(reserva.UsuarioId) is null)
            {
                return "Usuário não encontrado.";
            }

            if (await _salaService.GetSalaByIdAsync(reserva.SalaId) is null)
            {
                return "Sala não encontrada.";
            }

            // 3) Conflito de horário
            _reservaService.SetValidator(_validadorHorario);
            if (!await _reservaService.ValidateAsync(reserva))
            {
                return "Este horário está indisponível: já existe outra reserva nesse período.";
            }

            // 4) Capacidade
            _reservaService.SetValidator(_validadorCapacidade);
            if (!await _reservaService.ValidateAsync(reserva))
            {
                return "O número de pessoas excede a capacidade da sala.";
            }

            // 5) Salvar
            await _reservaService.SaveAsync(reserva);
            return "Reserva realizada com sucesso!";
        }


        public async Task<string> AtualizarAsync(Reserva reserva)
        {
            try
            {
                _ = await _reservaService.GetByIdAsync(reserva.Id);
                return await ReservarAsync(reserva);
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }

        public async Task DeleteAsync(long id)
        {
            await _reservaService.DeleteAsync(id);
        }

        public async Task<Reserva> GetByIdAsync(long id)
            => await _reservaService.GetByIdAsync(id);
    }
}
