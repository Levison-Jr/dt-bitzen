using DTBitzen.Models;
using DTBitzen.Repositories.Interfaces;
using DTBitzen.Services.Interfaces;

namespace DTBitzen.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly ISalaRepository _salaRepository;
        public ReservaService(IReservaRepository reservaRepository, ISalaRepository salaRepository)
        {
            _reservaRepository = reservaRepository;
            _salaRepository = salaRepository;
        }

        public async Task<IEnumerable<Reserva>> BuscarTodas(DateOnly? filtroData, string? filtroStatus)
        {
            return await _reservaRepository.BuscarTodasAsync(filtroData, filtroStatus);
        }

        public async Task<IEnumerable<Reserva>> BuscarPorUsuarioId(string usuarioId,
            DateOnly? filtroData,
            string? filtroStatus)
        {
            return await _reservaRepository.BuscarPorUsuarioIdAsync(usuarioId, filtroData, filtroStatus);
        }

        public async Task<IEnumerable<Reserva>> BuscarPorSalaId(int salaId,
            DateOnly? filtroData,
            string? filtroStatus)
        {
            return await _reservaRepository.BuscarPorSalaIdAsync(salaId, filtroData, filtroStatus);
        }

        public async Task<bool> Criar(Reserva reserva, List<string> usuariosIds)
        {
            try
            {
                Sala? salaDaReserva = await _salaRepository.BuscarPorId(reserva.SalaId);

                if (salaDaReserva is null || salaDaReserva.Capacidade < usuariosIds.Count)
                    return false;

                bool existeConflito = await _reservaRepository.ExisteConflitoReservasAtivas(
                    reserva.SalaId,
                    reserva.Data,
                    reserva.HoraInicio,
                    reserva.HoraFim);

                if (existeConflito)
                    return false;

                reserva.Agendamentos = [];

                foreach (var usuarioId in usuariosIds)
                {
                    var agendamento = new Agendamento()
                    {
                        UsuarioId = usuarioId,
                        ReservaId = reserva.ReservaId
                    };

                    reserva.Agendamentos.Add(agendamento);
                }                

                await _reservaRepository.CriarAsync(reserva);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CancelarReserva(Guid reservaId)
        {
            try
            {
                Reserva? reservaParaEditar = await _reservaRepository.BuscarPorId(reservaId);

                if (reservaParaEditar is null)
                    return false;

                reservaParaEditar.Status = "CANCELADA";

                await _reservaRepository.EditarAsync(reservaParaEditar);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
