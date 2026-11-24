
using System.ComponentModel.DataAnnotations;

namespace SistemaOrdensServico.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [StringLength(20)]
        public string Telefone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(200)]
        public string? Endereco { get; set; }
    }
}
