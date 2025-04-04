namespace DTBitzen.Identity
{
    public class LoginResposta
    {
        public bool Sucesso { get; set; }
        public string? Token { get; set; }

        public LoginResposta(bool sucesso, string? token = null)
        {
            Sucesso = sucesso;
            Token = token;
        }
    }
}
