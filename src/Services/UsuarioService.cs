using DTBitzen.Models;
using DTBitzen.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DTBitzen.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AspNetUserManager<Usuario> _aspNetUserManager;

        public UsuarioService(AspNetUserManager<Usuario> aspNetUserManager)
        {
            _aspNetUserManager = aspNetUserManager;
        }

        public async Task<Usuario?> BuscarPorId(string id)
        {
            return await _aspNetUserManager.FindByIdAsync(id);
        }

        public async Task<bool> Editar(string id, Usuario usuario)
        {
            Usuario? usuarioParaEditar = await _aspNetUserManager.FindByIdAsync(id);

            if (usuarioParaEditar is null)
                return false;

            usuarioParaEditar.Nome = usuario.Nome;

            var resultado = await _aspNetUserManager.UpdateAsync(usuarioParaEditar);

            return resultado.Succeeded;
        }

        public async Task<bool> Excluir(string id)
        {
            Usuario? usuarioParaExcluir = await _aspNetUserManager.FindByIdAsync(id);

            if (usuarioParaExcluir is null)
                return false;

            var resultado = await _aspNetUserManager.DeleteAsync(usuarioParaExcluir);

            return resultado.Succeeded;
        }
    }
}
