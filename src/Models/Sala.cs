using System.Text.Json.Serialization;

namespace DTBitzen.Models
{
    public class Sala
    {
        public int SalaId { get; set; }

        public string? Nome { get; set; }

        public int Capacidade { get; set; }

        [JsonIgnore]
        public IList<Reserva> Reservas { get; set; }
    }
}
