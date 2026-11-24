
using System.ComponentModel.DataAnnotations;

namespace SistemaOrdensServico.Models
{
    public class Tecnico
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

        [StringLength(100)]
        public string? Especialidade { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
