namespace DTBitzen.Dtos
{
    public record ReservaDto(
        Guid ReservaId,
        DateOnly Data,
        TimeOnly HoraInicio,
        TimeOnly HoraFim,
        string Status,
        int SalaId);
}
