using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Domain.Entities;

namespace Torneos_App.src.Modules.Jugadores.Domain.Entities
{
    public class Jugador
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public string? Apellido { get; set; } = string.Empty;
        public int Edad { get; set; }
        public int Dorsal { get; set; }
        public string? Posicion { get; set; } = string.Empty;
        public int? EquipoId { get; set; }
        public Equipo? Equipo { get; set; }
    }
}