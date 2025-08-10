using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Torneos_App.src.Shared.Configuration
{
    public class JugadorConfiguration : IEntityTypeConfiguration<Jugador>
    {
         public void Configure(EntityTypeBuilder<Jugador> builder)
        {
            builder.ToTable("jugadores");

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Nombre)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(j => j.Apellido)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(j => j.Edad)
                   .IsRequired();

            builder.Property(j => j.Dorsal)
                   .IsRequired();

            builder.Property(j => j.Posicion)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasOne(j => j.Equipo)
                   .WithMany(e => e.Jugadores)
                   .HasForeignKey(j => j.EquipoId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}