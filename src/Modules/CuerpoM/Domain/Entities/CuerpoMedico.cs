using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Domain.Entities;

namespace Torneos_App.src.Modules.CuerpoM.Domain.Entities
{
    public class CuerpoMedico
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public string? Apellido { get; set; } = string.Empty;
        public string? Especialidad { get; set; } = string.Empty; 
        public int EquipoId { get; set; }
        public Equipo? Equipo { get; set; }
    }
}