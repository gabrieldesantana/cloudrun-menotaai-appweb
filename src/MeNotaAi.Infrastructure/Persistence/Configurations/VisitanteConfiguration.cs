using MeNotaAi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeNotaAi.Infrastructure.Persistence.Configurations
{
    public class VisitanteConfiguration : IEntityTypeConfiguration<Visitante>
    {
        public void Configure(EntityTypeBuilder<Visitante> builder)
        {
            builder.ToTable("Visitantes");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).HasColumnType("varchar(100)").IsRequired();

            builder.Property(p => p.DataVisita)
            .HasColumnName("DataVisita")
            .HasColumnType("timestamp");

            builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamp");
        }
    }
}
