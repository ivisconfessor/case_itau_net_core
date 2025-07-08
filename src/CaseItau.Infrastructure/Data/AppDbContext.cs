using CaseItau.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseItau.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {   
        }

        public DbSet<Fund> Funds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da entidade Fund
            modelBuilder.Entity<Fund>(entity =>
            {
                entity.ToTable("FUNDO");
                entity.HasKey(f => f.Code);
                entity.Property(f => f.Code).HasColumnName("CODIGO");
                entity.Property(f => f.Name).HasColumnName("NOME");
                entity.Property(f => f.Cnpj).HasColumnName("CNPJ");
                entity.Property(f => f.FundTypeId).HasColumnName("CODIGO_TIPO");
                entity.Property(f => f.NetWorth).HasColumnName("PATRIMONIO");
                entity.Property(f => f.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(f => f.UpdatedAt).HasColumnName("UPDATED_AT");

                // Relacionamento com FundType
                entity.HasOne(f => f.FundType)
                      .WithMany()
                      .HasForeignKey(f => f.FundTypeId);
            });

            // Configuração da entidade FundType
            modelBuilder.Entity<FundType>(entity =>
            {
                entity.ToTable("TIPO_FUNDO");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id).HasColumnName("CODIGO");
                entity.Property(t => t.Name).HasColumnName("NOME");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
