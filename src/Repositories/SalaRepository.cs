using DTBitzen.Context;
using DTBitzen.Models;
using DTBitzen.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DTBitzen.Repositories
{
    public class SalaRepository : ISalaRepository
    {
        private readonly AppDbContext _appDbContext;
        public SalaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Sala?> BuscarPorId(int id)
        {
            return await _appDbContext.Salas.FirstOrDefaultAsync(s => s.SalaId == id);
        }

        public async Task CriarAsync(Sala sala)
        {
            await _appDbContext.AddAsync(sala);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task EditarAsync(Sala sala)
        {
            _appDbContext.Update(sala);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ExcluirAsync(Sala sala)
        {
            _appDbContext.Remove(sala);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
