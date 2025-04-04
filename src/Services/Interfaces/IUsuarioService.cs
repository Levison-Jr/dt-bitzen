using DTBitzen.Models;

namespace DTBitzen.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario?> BuscarPorId(string id);

        Task<bool> Editar(string id, Usuario usuario);

        Task<bool> Excluir(string id);
    }
}
