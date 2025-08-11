using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App_Torneos.src.Modules.Torneos.Domain.Entities;
using Torneos_App.src.Modules.CuerpoM.Domain.Entities;
using Torneos_App.src.Modules.CuerpoT.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;

namespace Torneos_App.src.Modules.Equipos.Domain.Entities
{
    public class Equipo
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public string? Tipo { get; set; } = string.Empty;
        public string? Pais { get; set; } = string.Empty;
        public ICollection<Jugador>? Jugadores { get; set; } = new HashSet<Jugador>();
        public ICollection<CuerpoTecnico>? CuerposTecnicos { get; set; } = new HashSet<CuerpoTecnico>();
        public ICollection<CuerpoMedico>? CuerposMedicos { get; set; } = new HashSet<CuerpoMedico>();
        public ICollection<Torneo>? Torneos { get; set; } = new HashSet<Torneo>();
    }
}