namespace DTBitzen.Dtos
{
    public record ReservaDto(
        Guid ReservaId,
        DateOnly Data,
        string HoraInicio,
        string HoraFim,
        string Status,
        int SalaId);
}
