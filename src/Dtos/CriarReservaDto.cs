namespace DTBitzen.Dtos
{
    public record CriarReservaDto
    {
        public DateOnly DataInicio { get; set; }

        public string? HoraInicio { get; set; }

        public DateOnly DataFim { get; set; }

        public string? HoraFim { get; set; }

        public int SalaId { get; set; }
    }
}
