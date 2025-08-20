using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Torneos_App.src.Modules.Notificaciones.Domain.Entities;
using Torneos_App.src.Modules.Transferencias.Application.Interfaces;
using Torneos_App.src.Modules.Transferencias.Domain.Entities;

namespace Torneos_App.src.Modules.Transferencias.Application.Services
{
    public class TransferenciaService : ITransferenciaService
    {
        private readonly ITransferenciaRepository _repository;

        public TransferenciaService(ITransferenciaRepository repository)
        {
            _repository = repository;
        }

        public bool TransferirJugador(int jugadorId, int equipoCompradorId)
        {
            var jugador = _repository.GetJugadorById(jugadorId);
            var equipoComprador = _repository.GetEquipoById(equipoCompradorId);
            if (jugador == null || equipoComprador == null) return false;

            jugador.EquipoId = equipoComprador.Id;
            _repository.SaveChanges();
            return true;
        }
        public void EnviarSolicitudCompra(int jugadorId, int equipoCompradorId)
        {
            var jugador = _repository.GetJugadorById(jugadorId);
            if (jugador == null || jugador.EquipoId == null) return;

            var notificacion = new Notificacion
            {
                JugadorId = jugadorId,
                EquipoSolicitanteId = equipoCompradorId,
                EquipoDuenoId = jugador.EquipoId.Value,
                Tipo = "COMPRA",
                Mensaje = $"Solicitud de compra \n El equipo {_repository.GetEquipoById(equipoCompradorId)?.Nombre} desea comprar a {jugador.Nombre}",
                Fecha = DateTime.Now,
                Atendida = false,
                Estado = "PENDIENTE"
            };

            _repository.AgregarNotificacion(notificacion);
            _repository.SaveChanges();
        }
        public void EnviarSolicitudPrestamo(int jugadorId, int equipoCompradorId)
        {
            var jugador = _repository.GetJugadorById(jugadorId);
            if (jugador == null || jugador.EquipoId == null) return;

            var notificacion = new Notificacion
            {
                JugadorId = jugadorId,
                EquipoSolicitanteId = equipoCompradorId,
                EquipoDuenoId = jugador.EquipoId.Value,
                Tipo = "PRESTAMO",
                Mensaje = $"Solicitud de prestamo \n El equipo {_repository.GetEquipoById(equipoCompradorId)?.Nombre} desea un prestamo de {jugador.Nombre}",
                Fecha = DateTime.Now,
                Atendida = false,
                Estado = "PENDIENTE"
            };

            _repository.AgregarNotificacion(notificacion);
            _repository.SaveChanges();
        }
        public void ResponderSolicitudCompra(int notificacionId, bool aceptar, decimal? precioPropuesto)
        {
            var notificacion = _repository.GetNotificacionById(notificacionId);
            if (notificacion == null) return;

            if (aceptar)
            {
                notificacion.Estado = "ACEPTADA";
                notificacion.PrecioPropuesto = precioPropuesto;
                var jugador = _repository.GetJugadorById(notificacion.JugadorId);
                if (jugador != null)
                {
                    jugador.EquipoId = notificacion.EquipoSolicitanteId;
                }
            }
            else
            {
                notificacion.Estado = "RECHAZADA";
            }

            notificacion.Atendida = true;
            _repository.UpdateNotificacion(notificacion);
            _repository.SaveChanges();
        }
        public void ResponderSolicitudPrestamo(int notificacionId, bool aceptar)
        {
            var notificacion = _repository.GetNotificacionById(notificacionId);
            if (notificacion == null) return;

            notificacion.Estado = aceptar ? "ACEPTADA" : "RECHAZADA";
            notificacion.Atendida = true;

            if (aceptar)
            {
                var jugador = _repository.GetJugadorById(notificacion.JugadorId);
                if (jugador != null)
                {
                    jugador.EquipoId = notificacion.EquipoSolicitanteId;
                }
            }

            _repository.UpdateNotificacion(notificacion);
            _repository.SaveChanges();
        }
        public List<Notificacion> ObtenerNotificaciones(int equipoId)
        {
            return _repository.GetNotificacionesByEquipo(equipoId);
        }


        public List<Equipo> ObtenerEquipos()
        {
            return _repository.GetEquipos();
        }

        public List<Jugador> ObtenerJugadores()
        {
            return _repository.GetJugadores();
        }
    }
}