using DTBitzen.Models;

namespace DTBitzen.Identity
{
    public class RegistrarResposta
    {
        public bool Sucesso { get; set; }
        public Usuario? Usuario { get; set; }

        public string? Mensagem { get; set; }

        public RegistrarResposta(bool sucesso, Usuario? usuario = null, string? mensagem = null)
        {
            Sucesso = sucesso;
            Usuario = usuario;
            Mensagem = mensagem;
        }
    }
}
