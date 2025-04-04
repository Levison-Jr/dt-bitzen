using System.ComponentModel.DataAnnotations;

namespace DTBitzen.Dtos
{
    public record RegistrarDto
    {
        [Required]
        public string? Nome { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Senha { get; set; }
    }
}
