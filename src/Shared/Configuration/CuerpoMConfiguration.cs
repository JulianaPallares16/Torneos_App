using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torneos_App.src.Modules.CuerpoM.Domain.Entities;

namespace Torneos_App.src.Shared.Configuration
{
    public class CuerpoMConfiguration
    {
        public void Configure(EntityTypeBuilder<CuerpoMedico> builder)
        {
            builder.ToTable("CuerposMedicos");

            builder.HasKey(cm => cm.Id);

            builder.Property(cm => cm.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cm => cm.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cm => cm.Edad)
            .IsRequired();

            builder.Property(cm => cm.Especialidad)
                .HasMaxLength(100);

            builder.HasOne(cm => cm.Equipo)
                .WithMany(e => e.CuerposMedicos) 
                .HasForeignKey(cm => cm.EquipoId)
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}