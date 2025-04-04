namespace DTBitzen.Dtos
{
    public record ReservaDto(
        Guid ReservaId,
        DateOnly DataInicio,
        string HoraInicio,
        DateOnly DataFim,
        string HoraFim,
        string Status,
        int SalaId);
}
