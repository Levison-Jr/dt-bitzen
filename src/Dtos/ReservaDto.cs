using DTBitzen.Models;

namespace DTBitzen.Dtos
{
    public record ReservaDto(
        Guid ReservaId,
        DateOnly Data,
        TimeOnly HoraInicio,
        TimeOnly HoraFim,
        string Status,
        int SalaId,
        IList<Agendamento> Agendamentos) : BaseDto;
}
