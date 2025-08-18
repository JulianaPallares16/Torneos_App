using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Torneos_App.src.Modules.Transferencias.Domain.Entities
{
    public class Transferencia
    {
         public int Id { get; set; }
        public int JugadorId { get; set; }
        public int EquipoDestinoId { get; set; }
        public int? EquipoOrigenId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal? Monto { get; set; }
    }
}