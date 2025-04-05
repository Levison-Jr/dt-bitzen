using System.ComponentModel.DataAnnotations;

namespace DTBitzen.Dtos
{
    public record SalaRequestDto
    {
        [Required]
        public string? Nome { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Insira um valor entre 1 e 20.")]
        public int Capacidade { get; set; }
    }
}
