using DTBitzen.Models;
using DTBitzen.Services.Interfaces;

namespace DTBitzen.Services
{
    public class ReservaService : IReservaService
    {
        public Task<IEnumerable<Reserva>> BuscarPorSalaId(int salaId, DateOnly filtroData, string filtroStatus)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Reserva>> BuscarPorUsuarioId(string usuarioId, DateOnly filtroData, string filtroStatus)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Reserva>> BuscarTodas()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Criar(Reserva reserva)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(Guid reservaId, Reserva reserva)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Excluir(Guid reservaId)
        {
            throw new NotImplementedException();
        }
    }
}
