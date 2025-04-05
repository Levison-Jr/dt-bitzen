namespace DTBitzen.Identity
{
    public interface IIdentityHelper
    {
        Task<LoginResposta> Login(string email, string password);
        Task<RegistrarResposta> Registrar(string nome, string email, string password);
    }
}
