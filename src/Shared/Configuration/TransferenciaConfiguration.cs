using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torneos_App.src.Modules.Transferencias.Domain.Entities;

namespace Torneos_App.src.Shared.Configuration
{
    public class TransferenciaConfiguration : IEntityTypeConfiguration<Transferencia>
    {
        public void Configure(EntityTypeBuilder<Transferencia> builder)
        {
            builder.ToTable("transferencias");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Tipo)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(t => t.Estado)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(t => t.PrecioPropuesto)
                   .HasColumnType("decimal(12,2)")
                   .IsRequired();

            builder.Property(t => t.PlazoMeses)
                   .IsRequired();

            builder.Property(t => t.FechaCreacion)
                   .IsRequired();

            builder.HasOne(t => t.Jugador)
                   .WithMany()
                   .HasForeignKey(t => t.JugadorId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.EquipoDueno)
                   .WithMany()
                   .HasForeignKey(t => t.EquipoDuenoId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.EquipoComprador)
                   .WithMany()
                   .HasForeignKey(t => t.EquipoCompradorId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
