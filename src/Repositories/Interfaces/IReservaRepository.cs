using DTBitzen.Models;

namespace DTBitzen.Repositories.Interfaces
{
    public interface IReservaRepository
    {
        Task<IEnumerable<Reserva>> BuscarTodasAsync(DateOnly? filtroData = null,
            string? filtroHoraiInicio = null,
            string? filtroHoraFim = null,
            string? filtroStatus = null);

        Task<IEnumerable<Reserva>> BuscarPorUsuarioIdAsync(string usuarioId, DateOnly? filtroData, string? filtroStatus);

        Task<IEnumerable<Reserva>> BuscarPorSalaIdAsync(int salaId, DateOnly? filtroData, string? filtroStatus);

        Task<Reserva?> BuscarPorId(Guid id);

        Task CriarAsync(Reserva reserva);

        Task ExcluirAsync(Reserva reserva);
    }
}
