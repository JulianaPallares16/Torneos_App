using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;

namespace Torneos_App.src.Modules.Transferencias.Application.Interfaces
{
    public interface ITransferenciaService
    {
       bool TransferirJugador(int jugadorId, int equipoDestinoId);
        List<Equipo> ObtenerEquipos();
        List<Jugador> ObtenerJugadores();
    }
}