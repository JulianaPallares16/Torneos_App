using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Torneos_App.src.Modules.Notificaciones.Domain.Entities;

namespace Torneos_App.src.Modules.Transferencias.Application.Interfaces
{
    public interface ITransferenciaService
    {
        bool TransferirJugador(int jugadorId, int equipoCompradorId);
        List<Equipo> ObtenerEquipos();
        List<Jugador> ObtenerJugadores();
        List<Notificacion> ObtenerNotificaciones(int equipoId);
        void EnviarSolicitudCompra(int jugadorId, int equipoCompradorId);
        void EnviarSolicitudPrestamo(int jugadorId, int equipoCompradorId);
        void ResponderSolicitudCompra(int notificacionId, bool aceptar, decimal? precioPropuesto);
        void ResponderSolicitudPrestamo(int notificacionId, bool aceptar);
    }
}