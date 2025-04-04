using System.ComponentModel.DataAnnotations;

namespace DTBitzen.Dtos
{
    public record LoginDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Senha { get; set; }
    }
}
