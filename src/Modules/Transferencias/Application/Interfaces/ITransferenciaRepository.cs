using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Torneos_App.src.Modules.Notificaciones.Domain.Entities;
using Torneos_App.src.Modules.Transferencias.Domain.Entities;

namespace Torneos_App.src.Modules.Transferencias.Application.Interfaces
{
    public interface ITransferenciaRepository
    {
        Jugador? GetJugadorById(int jugadorId);
        Equipo? GetEquipoById(int equipoId);
        List<Equipo> GetEquipos();
        List<Jugador> GetJugadores();
        void SaveChanges();
        void AgregarNotificacion(Notificacion notificacion);
        List<Notificacion> GetNotificacionesByEquipo(int equipoId);
        Notificacion? GetNotificacionById(int notificacionId);
        void UpdateNotificacion(Notificacion notificacion);
    }
}