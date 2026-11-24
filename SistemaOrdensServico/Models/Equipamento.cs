
using System.ComponentModel.DataAnnotations;

namespace SistemaOrdensServico.Models
{
    public class Equipamento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do equipamento é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [StringLength(80)]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [StringLength(100)]
        public string Modelo { get; set; } = string.Empty;

        [StringLength(100)]
        public string? NumeroSerie { get; set; }

        [Required(ErrorMessage = "Selecione o cliente.")]
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }
    }
}
