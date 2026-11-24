
using Microsoft.EntityFrameworkCore;
using SistemaOrdensServico.Models;

namespace SistemaOrdensServico.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Tecnico> Tecnicos { get; set; }

        public DbSet<Equipamento> Equipamentos { get; set; }

        public DbSet<Servico> Servicos { get; set; }

        public DbSet<OrdemServico> OrdensServico { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cliente possui vários equipamentos
            modelBuilder.Entity<Equipamento>()
                .HasOne(e => e.Cliente)
                .WithMany()
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cliente possui várias ordens de serviço
            modelBuilder.Entity<OrdemServico>()
                .HasOne(o => o.Cliente)
                .WithMany()
                .HasForeignKey(o => o.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Equipamento possui várias ordens de serviço
            modelBuilder.Entity<OrdemServico>()
                .HasOne(o => o.Equipamento)
                .WithMany()
                .HasForeignKey(o => o.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Técnico pode atender várias ordens
            modelBuilder.Entity<OrdemServico>()
                .HasOne(o => o.Tecnico)
                .WithMany()
                .HasForeignKey(o => o.TecnicoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Precisão dos valores dos serviços
            modelBuilder.Entity<Servico>()
                .Property(s => s.Valor)
                .HasPrecision(10, 2);

            modelBuilder.Entity<OrdemServico>()
                .HasOne(o => o.Servico)
                .WithMany()
                .HasForeignKey(o => o.ServicoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
