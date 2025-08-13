using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torneos_App.src.Modules.CuerpoT.Domain.Entities;

namespace Torneos_App.src.Shared.Configuration
{
    public class CuerpoTConfiguration
    {
        public void Configure(EntityTypeBuilder<CuerpoTecnico> builder)
        {
            builder.ToTable("CuerposMedicos");

            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ct => ct.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ct => ct.Edad)
            .IsRequired();

            builder.Property(ct => ct.Cargo)
                .HasMaxLength(100);

            builder.HasOne(ct => ct.Equipo)
                .WithMany(e => e.CuerposTecnicos) 
                .HasForeignKey(cm => cm.EquipoId)
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}