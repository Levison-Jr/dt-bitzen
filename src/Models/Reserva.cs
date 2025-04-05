namespace DTBitzen.Models
{
    public class Reserva
    {
        public Guid ReservaId { get; set; }

        public DateOnly Data { get; set; }

        public TimeOnly HoraInicio { get; set; }

        public TimeOnly HoraFim { get; set; }

        public string Status { get; set; }


        public int SalaId { get; set; }

        public Sala Sala { get; set; }

        public IList<Agendamento> Agendamentos { get; set; }
    }
}
