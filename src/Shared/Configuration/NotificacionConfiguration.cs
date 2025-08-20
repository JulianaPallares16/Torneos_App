using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torneos_App.src.Modules.Notificaciones.Domain.Entities;

namespace Torneos_App.src.Shared.Configuration
{
    public class NotificacionConfiguration
    {
        public void Configure(EntityTypeBuilder<Notificacion> builder)
        {
            builder.ToTable("notificaciones");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Tipo)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(n => n.Mensaje)
                   .IsRequired()
                   .HasMaxLength(300);

            builder.Property(n => n.Fecha)
                   .IsRequired();

            builder.Property(n => n.Atendida)
                   .HasDefaultValue(false);

            builder.Property(n => n.PrecioPropuesto)
                   .HasColumnType("decimal(12,2)")
                   .IsRequired();

            builder.Property(n => n.Estado)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasOne(n => n.EquipoDueno)
                   .WithMany()
                   .HasForeignKey(n => n.EquipoDuenoId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(n => n.EquipoSolicitante)
                   .WithMany()
                   .HasForeignKey(n => n.EquipoSolicitanteId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(n => n.Jugador)
                   .WithMany()
                   .HasForeignKey(n => n.JugadorId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}