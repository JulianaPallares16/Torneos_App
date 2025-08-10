using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Domain.Entities;

namespace App_Torneos.src.Modules.Torneos.Domain.Entities
{
    public class Torneo
    {
         public int Id { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public string? Tipo { get; set; } = string.Empty; 
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public ICollection<Equipo>? Equipos { get; set; } = new HashSet<Equipo>();
    }
}