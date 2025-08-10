using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App_Torneos.src.Modules.Torneos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Torneos_App.src.Shared.Configuration
{
    public class TorneoConfiguration : IEntityTypeConfiguration<Torneo>
    {
        public void Configure(EntityTypeBuilder<Torneo> builder)
    {
        builder.ToTable("torneos");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Nombre)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(t => t.Tipo)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(t => t.FechaInicio)
               .IsRequired();
        
        builder.Property(t => t.FechaFin)
               .IsRequired();
               
    }
    }
}