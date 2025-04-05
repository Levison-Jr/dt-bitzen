using DTBitzen.Models;

namespace DTBitzen.Services.Interfaces
{
    public interface IReservaService
    {
        Task<IEnumerable<Reserva>> BuscarTodas();

        Task<IEnumerable<Reserva>> BuscarPorUsuarioId(string usuarioId,
            DateOnly? filtroData,
            string? filtroStatus);

        Task<IEnumerable<Reserva>> BuscarPorSalaId(int salaId,
            DateOnly? filtroData,
            string? filtroStatus);

        Task<bool> Criar(Reserva reserva, List<string> usuariosIds);

        Task<bool> CancelarReserva(Guid reservaId);
    }
}
