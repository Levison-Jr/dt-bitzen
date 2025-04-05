using System.ComponentModel.DataAnnotations;

namespace DTBitzen.Dtos
{
    public record CriarReservaDto
    {
        [Required]
        public DateOnly Data { get; set; }

        [Required]
        [RegularExpression(@"^([01]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "Formato inválido. Usar HH:mm, de 00:00 até 23:59")]
        public string? HoraInicio { get; set; }

        [Required]
        [RegularExpression(@"^([01]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "Formato inválido. Usar HH:mm, de 00:00 até 23:59")]
        public string? HoraFim { get; set; }

        [Required]
        public int SalaId { get; set; }

        [Required]
        public string? Status { get; set; } = "ATIVA";

        public List<string>? UsuariosIds { get; set; } 
    }
}
