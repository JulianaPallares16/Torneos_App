using System.Collections.Generic;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Torneos_App.src.Modules.Notificaciones.Domain.Entities;

namespace Torneos_App.src.Modules.Transferencias.Application.Interfaces
{
    public interface ITransferenciaService
    {
        bool TransferirJugador(int jugadorId, int equipoCompradorId);
        void EnviarSolicitudCompra(int jugadorId, int equipoCompradorId);
        void EnviarSolicitudPrestamo(int jugadorId, int equipoCompradorId);
        void ResponderSolicitudCompra(int notificacionId, int equipoDuenoId, bool aceptar, decimal? precioPropuesto);
        void ResponderSolicitudPrestamo(int notificacionId, int equipoDuenoId, bool aceptar, int? meses);
        void ConfirmarCompra(int notificacionId, int equipoCompradorId, bool aceptar);
        void ConfirmarPrestamo(int notificacionId, int equipoCompradorId, bool aceptar);
        List<Equipo> ObtenerEquipos();
        List<Jugador> ObtenerJugadores();
        List<Notificacion> ObtenerNotificaciones(int equipoId); 
    }
}
