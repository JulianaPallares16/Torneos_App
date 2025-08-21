using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Torneos_App.src.Modules.Notificaciones.Domain.Entities;

namespace Torneos_App.src.Modules.Transferencias.Domain.Entities
{
    public class Transferencia
    {
         public int Id { get; set; }

        public int JugadorId { get; set; }
        public Jugador? Jugador { get; set; }

        public int EquipoDuenoId { get; set; }
        public Equipo? EquipoDueno { get; set; }

        public int EquipoCompradorId { get; set; }
        public Equipo? EquipoComprador { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Estado { get; set; } = "PENDIENTE_DUENO";

        public decimal? PrecioPropuesto { get; set; }  
        public int? PlazoMeses { get; set; }  

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaCierre { get; set; }

        public ICollection<Notificacion>? Notificaciones { get; set; } = new HashSet<Notificacion>();
    }
}