namespace DTBitzen.Dtos
{
    public record UsuarioDto(
        string Id,
        string Nome,
        string Email) : BaseDto;
}
