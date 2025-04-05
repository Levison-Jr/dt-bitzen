using DTBitzen.Models;

namespace DTBitzen.Repositories.Interfaces
{
    public interface IReservaRepository
    {
        Task<IEnumerable<Reserva>> BuscarTodasAsync(DateOnly? filtroData = null,
            string? filtroStatus = null);

        Task<IEnumerable<Reserva>> BuscarPorUsuarioIdAsync(string usuarioId,
            DateOnly? filtroData,
            string? filtroStatus);

        Task<IEnumerable<Reserva>> BuscarPorSalaIdAsync(int salaId,
            DateOnly? filtroData = null,
            string? filtroStatus = null);

        Task<Reserva?> BuscarPorId(Guid id);

        Task CriarAsync(Reserva reserva);

        Task EditarAsync(Reserva reserva);

        Task<bool> ExisteConflitoReservasAtivas(int salaId,
            DateOnly data,
            TimeOnly horaInicio,
            TimeOnly horaFim);
    }
}
