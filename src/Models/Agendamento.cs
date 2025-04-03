using System.Text.Json.Serialization;

namespace DTBitzen.Models
{
    public class Agendamento
    {
        public string UsuarioId { get; set; }

        public Guid ReservaId { get; set; }


        [JsonIgnore]
        public Usuario Usuario { get; set; }

        [JsonIgnore]
        public Reserva Reserva { get; set; }
    }
}
