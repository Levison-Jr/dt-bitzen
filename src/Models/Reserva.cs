namespace DTBitzen.Models
{
    public class Reserva
    {
        public Guid ReservaId { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }


        public int SalaId { get; set; }

        public Sala Sala { get; set; }

        public IList<Agendamento> Agendamentos { get; set; }
    }
}
