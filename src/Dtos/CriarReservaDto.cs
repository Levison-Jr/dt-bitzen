using System.ComponentModel.DataAnnotations;

namespace DTBitzen.Dtos
{
    public record CriarReservaDto
    {
        [Required]
        public DateOnly Data { get; set; }

        [Required]
        public TimeOnly HoraInicio { get; set; }

        [Required]
        public TimeOnly HoraFim { get; set; }

        [Required]
        public int SalaId { get; set; }

        [Required]
        [RegularExpression("^ATIVA|CANCELADA$", ErrorMessage = "Status deve ser ATIVA ou CANCELADA")]
        public string? Status { get; set; } = "ATIVA";

        public List<string>? UsuariosIds { get; set; } 
    }
}
