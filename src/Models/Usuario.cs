using Microsoft.AspNetCore.Identity;

namespace DTBitzen.Models
{
    public class Usuario : IdentityUser
    {
        public string? Nome { get; set; }

        public IList<Agendamento> Agendamentos { get; set; }
    }
}
