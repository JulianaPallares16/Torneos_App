using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App_Torneos.src.Modules.Torneos.Domain.Entities;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Torneos_App.src.Modules.CuerpoT.Domain.Entities;
using Torneos_App.src.Modules.CuerpoM.Domain.Entities;
using Torneos_App.src.Modules.Notificaciones.Domain.Entities;


namespace Torneos_App.src.Shared.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Torneo> Torneos => Set<Torneo>();
        public DbSet<Equipo> Equipos => Set<Equipo>();
        public DbSet<Jugador> Jugadores => Set<Jugador>();
        public DbSet<CuerpoTecnico> CuerposTecnicos => Set<CuerpoTecnico>();
        public DbSet<CuerpoMedico> CuerposMedicos => Set<CuerpoMedico>();
        public DbSet<Notificacion> Notificaciones => Set<Notificacion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}