using System.Collections.Generic;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Torneos_App.src.Modules.Notificaciones.Domain.Entities;
using Torneos_App.src.Modules.Transferencias.Domain.Entities;

namespace Torneos_App.src.Modules.Transferencias.Application.Interfaces
{
    public interface ITransferenciaRepository
    {
        // Básicos
        Jugador? GetJugadorById(int jugadorId);
        Equipo? GetEquipoById(int equipoId);
        List<Equipo> GetEquipos();
        List<Jugador> GetJugadores();
        Transferencia CrearTransferencia(Transferencia transferencia);
        Transferencia? GetTransferenciaById(int id);
        void UpdateTransferencia(Transferencia transferencia);
        void AgregarNotificacion(Notificacion notificacion);
        List<Notificacion> GetNotificacionesByEquipo(int equipoId); 
        Notificacion? GetNotificacionById(int notificacionId);
        void UpdateNotificacion(Notificacion notificacion);
        void SaveChanges();
    }
}
