using System.ComponentModel.DataAnnotations;

namespace SistemaOrdensServico.Models
{
    public class OrdemServico
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(300)]
        public string Descricao { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Diagnostico { get; set; }

        [StringLength(1000)]
        public string? Solucao { get; set; }

        public DateTime DataAbertura { get; set; } = DateTime.Now;

        public DateTime? DataConclusao { get; set; }

        [Required]
        [StringLength(30)]
        public string Status { get; set; } = "Aberta";

        [Required]
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }

        [Required]
        public int EquipamentoId { get; set; }

        public Equipamento? Equipamento { get; set; }

        [Required]
        public int TecnicoId { get; set; }

        public Tecnico? Tecnico { get; set; }

        [Required]
        public int ServicoId { get; set; }

        public Servico? Servico { get; set; }
    }
}
