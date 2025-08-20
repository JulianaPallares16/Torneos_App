using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;

namespace Torneos_App.src.Modules.Notificaciones.Domain.Entities
{
    public class Notificacion
    {
        public int Id { get; set; }
        public int EquipoDuenoId { get; set; }
        public Equipo? EquipoDueno { get; set; }
        public int EquipoSolicitanteId { get; set; }
        public Equipo? EquipoSolicitante { get; set; }
        public int JugadorId { get; set; }
        public Jugador? Jugador { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Estado { get; set; } = "PENDIENTE";
        public string Mensaje { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal? PrecioPropuesto { get; set; }
        public bool Atendida { get; set; } = false;
    }
}