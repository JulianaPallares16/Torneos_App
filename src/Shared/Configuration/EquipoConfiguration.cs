using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torneos_App.src.Modules.Equipos.Domain.Entities;

namespace Torneos_App.src.Shared.Configuration
{
    public class EquipoConfiguration : IEntityTypeConfiguration<Equipo>
    {
        public void Configure(EntityTypeBuilder<Equipo> builder)
        {
            builder.ToTable("equipos");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nombre)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Tipo)
                   .HasMaxLength(50);

            builder.Property(e => e.Pais)
                   .HasMaxLength(50);

            builder.HasMany(e => e.Jugadores)
                   .WithOne(j => j.Equipo)
                   .HasForeignKey(j => j.EquipoId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.CuerposTecnicos)
                   .WithOne(ct => ct.Equipo)
                   .HasForeignKey(ct => ct.EquipoId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.CuerposMedicos)
                   .WithOne(cm => cm.Equipo)
                   .HasForeignKey(cm => cm.EquipoId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}