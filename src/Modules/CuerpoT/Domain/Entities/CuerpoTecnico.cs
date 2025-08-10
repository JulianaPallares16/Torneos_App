using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Domain.Entities;

namespace Torneos_App.src.Modules.CuerpoT.Domain.Entities
{
    public class CuerpoTecnico
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public string? Apellido { get; set; } = string.Empty;
        public string? Cargo { get; set; } = string.Empty;  
        public int EquipoId { get; set; }
        public Equipo? Equipo { get; set; }
    }
}