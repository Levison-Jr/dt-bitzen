using DTBitzen.Context;
using DTBitzen.Models;
using DTBitzen.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DTBitzen.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly AppDbContext _appDbContext;

        public ReservaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Reserva>> BuscarTodasAsync(DateOnly? filtroData = null,
            string? filtroHoraiInicio = null,
            string? filtroHoraFim = null,
            string? filtroStatus = null)
        {
            var query = _appDbContext.Reservas.AsQueryable();

            if (filtroData is not null)
                query = query.Where(r => r.Data == filtroData);

            if (!string.IsNullOrEmpty(filtroHoraiInicio))
                query = query.Where(r => r.HoraInicio.CompareTo(filtroHoraiInicio) >= 0);

            if (!string.IsNullOrEmpty(filtroHoraFim))
                query = query.Where(r => r.HoraInicio.CompareTo(filtroHoraFim) <= 0);

            if (!string.IsNullOrEmpty(filtroStatus))
                query = query.Where(r => r.Status == filtroStatus);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> BuscarPorUsuarioIdAsync(string usuarioId,
            DateOnly? filtroData,
            string? filtroStatus)
        {
            var query = _appDbContext.Reservas
                .Where(r => r.Agendamentos.Any(a => a.UsuarioId == usuarioId));

            if (filtroData is not null)
                query = query.Where(r => r.Data == filtroData);

            if (!string.IsNullOrEmpty(filtroStatus))
                query = query.Where(r => r.Status == filtroStatus);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> BuscarPorSalaIdAsync(int salaId,
            DateOnly? filtroData,
            string? filtroStatus)
        {
            var query = _appDbContext.Reservas
                .Where(r => r.SalaId == salaId);

            if (filtroData is not null)
                query = query.Where(r => r.Data == filtroData);

            if (!string.IsNullOrEmpty(filtroStatus))
                query = query.Where(r => r.Status == filtroStatus);

            return await query.ToListAsync();
        }

        public async Task<Reserva?> BuscarPorId(Guid id)
        {
            return await _appDbContext.Reservas.FirstOrDefaultAsync(r => r.ReservaId == id);
        }

        public async Task CriarAsync(Reserva reserva)
        {
            await _appDbContext.AddAsync(reserva);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ExcluirAsync(Reserva reserva)
        {
            _appDbContext.Reservas.Remove(reserva);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
