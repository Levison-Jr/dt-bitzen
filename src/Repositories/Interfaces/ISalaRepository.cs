using DTBitzen.Models;

namespace DTBitzen.Repositories.Interfaces
{
    public interface ISalaRepository
    {
        Task<Sala?> BuscarPorId(int id);

        Task CriarAsync(Sala sala);

        Task EditarAsync(Sala sala);

        Task ExcluirAsync(Sala sala);
    }
}
