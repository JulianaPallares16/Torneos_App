using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Torneos_App.src.Modules.Transferencias.Domain.Entities;

namespace Torneos_App.src.Shared.Configuration
{
    public class TransferenciaConfiguration : IEntityTypeConfiguration<Transferencia>
    {
        public void Configure(EntityTypeBuilder<Transferencia> builder)
        {
            builder.ToTable("Transferencias");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Fecha)
                   .IsRequired();

            builder.Property(t => t.Monto)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne<Jugador>() 
                   .WithMany()
                   .HasForeignKey(t => t.JugadorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Equipo>() 
                   .WithMany()
                   .HasForeignKey(t => t.EquipoDestinoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Equipo>() 
                   .WithMany()
                   .HasForeignKey(t => t.EquipoOrigenId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}