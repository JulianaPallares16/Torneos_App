using System;
using System.Collections.Generic;
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

            if (jugador.EquipoId != null) return false; 

            jugador.EquipoId = equipoComprador.Id;
            _repository.SaveChanges();
            return true;
        }
        public void EnviarSolicitudCompra(int jugadorId, int equipoCompradorId)
        {
            var jugador = _repository.GetJugadorById(jugadorId);
            var equipoComprador = _repository.GetEquipoById(equipoCompradorId);
            if (jugador == null || equipoComprador == null) return;
            if (jugador.EquipoId == null) return; 

            var transferencia = new Transferencia
            {
                JugadorId = jugadorId,
                EquipoDuenoId = jugador.EquipoId.Value,
                EquipoCompradorId = equipoCompradorId,
                Tipo = "COMPRA",
                Estado = "PENDIENTE_DUENO",
                FechaCreacion = DateTime.Now
            };
            _repository.CrearTransferencia(transferencia);

            var notif = new Notificacion
            {
                TransferenciaId = transferencia.Id,
                EquipoDuenoId = transferencia.EquipoDuenoId,
                EquipoSolicitanteId = transferencia.EquipoCompradorId,
                JugadorId = jugadorId,
                Tipo = "SOLICITUD_COMPRA",
                Mensaje = $"El equipo {equipoComprador.Nombre} desea COMPRAR a {jugador.Nombre}.",
                Fecha = DateTime.Now,
                Estado = "PENDIENTE",
                Atendida = false
            };
            _repository.AgregarNotificacion(notif);

            _repository.SaveChanges();
        }

        public void EnviarSolicitudPrestamo(int jugadorId, int equipoCompradorId)
        {
            var jugador = _repository.GetJugadorById(jugadorId);
            var equipoComprador = _repository.GetEquipoById(equipoCompradorId);
            if (jugador == null || equipoComprador == null) return;
            if (jugador.EquipoId == null) return; 

            var transferencia = new Transferencia
            {
                JugadorId = jugadorId,
                EquipoDuenoId = jugador.EquipoId.Value,
                EquipoCompradorId = equipoCompradorId,
                Tipo = "PRESTAMO",
                Estado = "PENDIENTE_DUENO",
                FechaCreacion = DateTime.Now
            };
            _repository.CrearTransferencia(transferencia);

            var notif = new Notificacion
            {
                TransferenciaId = transferencia.Id,
                EquipoDuenoId = transferencia.EquipoDuenoId,
                EquipoSolicitanteId = transferencia.EquipoCompradorId,
                JugadorId = jugadorId,
                Tipo = "SOLICITUD_PRESTAMO",
                Mensaje = $"El equipo {equipoComprador.Nombre} solicita PRÉSTAMO de {jugador.Nombre}.",
                Fecha = DateTime.Now,
                Estado = "PENDIENTE",
                Atendida = false
            };
            _repository.AgregarNotificacion(notif);

            _repository.SaveChanges();
        }
        public void ResponderSolicitudCompra(int notificacionId, int equipoDuenoId, bool aceptar, decimal? precioPropuesto)
        {
            var notif = _repository.GetNotificacionById(notificacionId);
            if (notif == null || notif.Tipo != "SOLICITUD_COMPRA") return;
            if (notif.EquipoDuenoId != equipoDuenoId) return; 
            var t = _repository.GetTransferenciaById(notif.TransferenciaId);
            if (t == null) return;

            notif.Atendida = true;
            notif.Estado = aceptar ? "ACEPTADA" : "RECHAZADA";

            if (aceptar)
            {
                t.Estado = "PROPUESTA_ENVIADA";
                t.PrecioPropuesto = precioPropuesto;

                var notifComprador = new Notificacion
                {
                    TransferenciaId = t.Id,
                    EquipoDuenoId = t.EquipoDuenoId,
                    EquipoSolicitanteId = t.EquipoCompradorId,
                    JugadorId = t.JugadorId,
                    Tipo = "PROPUESTA_COMPRA",
                    Mensaje = $"El dueño acepta vender. Precio propuesto: {precioPropuesto:C}. ¿Aceptan?",
                    Fecha = DateTime.Now,
                    Estado = "PENDIENTE",
                    Atendida = false,
                    PrecioPropuesto = precioPropuesto
                };
                _repository.AgregarNotificacion(notifComprador);
            }
            else
            {
                t.Estado = "RECHAZADA";

                var notifComprador = new Notificacion
                {
                    TransferenciaId = t.Id,
                    EquipoDuenoId = t.EquipoDuenoId,
                    EquipoSolicitanteId = t.EquipoCompradorId,
                    JugadorId = t.JugadorId,
                    Tipo = "PROPUESTA_COMPRA",
                    Mensaje = "El dueño ha RECHAZADO la compra.",
                    Fecha = DateTime.Now,
                    Estado = "RECHAZADA",
                    Atendida = false
                };
                _repository.AgregarNotificacion(notifComprador);
            }

            _repository.UpdateNotificacion(notif);
            _repository.UpdateTransferencia(t);
            _repository.SaveChanges();
        }

        public void ResponderSolicitudPrestamo(int notificacionId, int equipoDuenoId, bool aceptar, int? meses)
        {
            var notif = _repository.GetNotificacionById(notificacionId);
            if (notif == null || notif.Tipo != "SOLICITUD_PRESTAMO") return;
            if (notif.EquipoDuenoId != equipoDuenoId) return;
            var t = _repository.GetTransferenciaById(notif.TransferenciaId);
            if (t == null) return;

            notif.Atendida = true;
            notif.Estado = aceptar ? "ACEPTADA" : "RECHAZADA";

            if (aceptar)
            {
                t.Estado = "PROPUESTA_ENVIADA";
                t.PlazoMeses = meses;

                var notifComprador = new Notificacion
                {
                    TransferenciaId = t.Id,
                    EquipoDuenoId = t.EquipoDuenoId,
                    EquipoSolicitanteId = t.EquipoCompradorId,
                    JugadorId = t.JugadorId,
                    Tipo = "PROPUESTA_PRESTAMO",
                    Mensaje = $"El dueño acepta préstamo. Plazo: {meses} meses. ¿Aceptan?",
                    Fecha = DateTime.Now,
                    Estado = "PENDIENTE",
                    Atendida = false
                };
                _repository.AgregarNotificacion(notifComprador);
            }
            else
            {
                t.Estado = "RECHAZADA";

                var notifComprador = new Notificacion
                {
                    TransferenciaId = t.Id,
                    EquipoDuenoId = t.EquipoDuenoId,
                    EquipoSolicitanteId = t.EquipoCompradorId,
                    JugadorId = t.JugadorId,
                    Tipo = "PROPUESTA_PRESTAMO",
                    Mensaje = "El dueño ha RECHAZADO el préstamo.",
                    Fecha = DateTime.Now,
                    Estado = "RECHAZADA",
                    Atendida = false
                };
                _repository.AgregarNotificacion(notifComprador);
            }

            _repository.UpdateNotificacion(notif);
            _repository.UpdateTransferencia(t);
            _repository.SaveChanges();
        }
        public void ConfirmarCompra(int notificacionId, int equipoCompradorId, bool aceptar)
        {
            var notif = _repository.GetNotificacionById(notificacionId);
            if (notif == null || notif.Tipo != "PROPUESTA_COMPRA") return;
            if (notif.EquipoSolicitanteId != equipoCompradorId) return;
            var t = _repository.GetTransferenciaById(notif.TransferenciaId);
            if (t == null) return;

            notif.Atendida = true;
            notif.Estado = aceptar ? "ACEPTADA" : "RECHAZADA";

            if (aceptar)
            {
                t.Estado = "ACEPTADA";
                t.FechaCierre = DateTime.Now;

                var jugador = _repository.GetJugadorById(t.JugadorId);
                if (jugador != null)
                {
                    jugador.EquipoId = t.EquipoCompradorId;
                }

                var notifDueno = new Notificacion
                {
                    TransferenciaId = t.Id,
                    EquipoDuenoId = t.EquipoDuenoId,
                    EquipoSolicitanteId = t.EquipoCompradorId,
                    JugadorId = t.JugadorId,
                    Tipo = "PROPUESTA_COMPRA",
                    Mensaje = "El comprador ACEPTÓ el precio. Transferencia completada.",
                    Fecha = DateTime.Now,
                    Estado = "ACEPTADA",
                    Atendida = false
                };
                _repository.AgregarNotificacion(notifDueno);
            }
            else
            {
                t.Estado = "RECHAZADA";
                var notifDueno = new Notificacion
                {
                    TransferenciaId = t.Id,
                    EquipoDuenoId = t.EquipoDuenoId,
                    EquipoSolicitanteId = t.EquipoCompradorId,
                    JugadorId = t.JugadorId,
                    Tipo = "PROPUESTA_COMPRA",
                    Mensaje = "El comprador RECHAZÓ el precio propuesto.",
                    Fecha = DateTime.Now,
                    Estado = "RECHAZADA",
                    Atendida = false
                };
                _repository.AgregarNotificacion(notifDueno);
            }

            _repository.UpdateNotificacion(notif);
            _repository.UpdateTransferencia(t);
            _repository.SaveChanges();
        }

        public void ConfirmarPrestamo(int notificacionId, int equipoCompradorId, bool aceptar)
        {
            var notif = _repository.GetNotificacionById(notificacionId);
            if (notif == null || notif.Tipo != "PROPUESTA_PRESTAMO") return;
            if (notif.EquipoSolicitanteId != equipoCompradorId) return;
            var t = _repository.GetTransferenciaById(notif.TransferenciaId);
            if (t == null) return;

            notif.Atendida = true;
            notif.Estado = aceptar ? "ACEPTADA" : "RECHAZADA";

            if (aceptar)
            {
                t.Estado = "ACEPTADA";
                t.FechaCierre = DateTime.Now;

                var jugador = _repository.GetJugadorById(t.JugadorId);
                if (jugador != null)
                {
                    jugador.EquipoId = t.EquipoCompradorId;
                }

                var notifDueno = new Notificacion
                {
                    TransferenciaId = t.Id,
                    EquipoDuenoId = t.EquipoDuenoId,
                    EquipoSolicitanteId = t.EquipoCompradorId,
                    JugadorId = t.JugadorId,
                    Tipo = "PROPUESTA_PRESTAMO",
                    Mensaje = "El comprador ACEPTÓ el préstamo. Transferencia (préstamo) completada.",
                    Fecha = DateTime.Now,
                    Estado = "ACEPTADA",
                    Atendida = false
                };
                _repository.AgregarNotificacion(notifDueno);
            }
            else
            {
                t.Estado = "RECHAZADA";

                var notifDueno = new Notificacion
                {
                    TransferenciaId = t.Id,
                    EquipoDuenoId = t.EquipoDuenoId,
                    EquipoSolicitanteId = t.EquipoCompradorId,
                    JugadorId = t.JugadorId,
                    Tipo = "PROPUESTA_PRESTAMO",
                    Mensaje = "El comprador RECHAZÓ el préstamo.",
                    Fecha = DateTime.Now,
                    Estado = "RECHAZADA",
                    Atendida = false
                };
                _repository.AgregarNotificacion(notifDueno);
            }

            _repository.UpdateNotificacion(notif);
            _repository.UpdateTransferencia(t);
            _repository.SaveChanges();
        }
        public List<Notificacion> ObtenerNotificaciones(int equipoId)
        {
            return _repository.GetNotificacionesByEquipo(equipoId);
        }

        public List<Equipo> ObtenerEquipos() => _repository.GetEquipos();

        public List<Jugador> ObtenerJugadores() => _repository.GetJugadores();
    }
}
