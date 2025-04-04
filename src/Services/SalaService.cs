using DTBitzen.Models;
using DTBitzen.Repositories.Interfaces;
using DTBitzen.Services.Interfaces;

namespace DTBitzen.Services
{
    public class SalaService : ISalaService
    {
        private readonly ISalaRepository _salaRepository;

        public SalaService(ISalaRepository salaRepository)
        {
            _salaRepository = salaRepository;
        }

        public async Task<bool> Criar(string nomeSala, int capacidade)
        {
            Sala sala = new()
            {
                Nome = nomeSala,
                Capacidade = capacidade
            };

            try
            {
                await _salaRepository.CriarAsync(sala);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Editar(int salaId, string nomeSala, int capacidade)
        {
            try
            {
                Sala? salaParaEditar = await _salaRepository.BuscarPorId(salaId);

                if (salaParaEditar is null)
                    return false;

                salaParaEditar.Nome = nomeSala;
                salaParaEditar.Capacidade = capacidade;

                await _salaRepository.EditarAsync(salaParaEditar);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Excluir(int salaId)
        {
            try
            {
                Sala? salaParaExcluir = await _salaRepository.BuscarPorId(salaId);

                if (salaParaExcluir is null)
                    return false;

                await _salaRepository.ExcluirAsync(salaParaExcluir);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
