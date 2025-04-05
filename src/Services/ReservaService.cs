using DTBitzen.Context;
using DTBitzen.Models;
using DTBitzen.Repositories;
using DTBitzen.Repositories.Interfaces;
using DTBitzen.Services.Interfaces;

namespace DTBitzen.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        public ReservaService(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public async Task<IEnumerable<Reserva>> BuscarTodas()
        {
            return await _reservaRepository.BuscarTodasAsync();
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

        public async Task<bool> Excluir(Guid reservaId)
        {
            try
            {
                Reserva? reservaParaExcluir = await _reservaRepository.BuscarPorId(reservaId);

                if (reservaParaExcluir is null)
                    return false;

                await _reservaRepository.ExcluirAsync(reservaParaExcluir);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
