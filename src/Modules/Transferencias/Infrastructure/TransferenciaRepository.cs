using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Torneos_App.src.Modules.Notificaciones.Domain.Entities;
using Torneos_App.src.Modules.Transferencias.Application.Interfaces;
using Torneos_App.src.Shared.Context;

namespace Torneos_App.src.Modules.Transferencias.Infrastructure
{
    public class TransferenciaRepository : ITransferenciaRepository
    {
        private readonly AppDbContext _context;

        public TransferenciaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Jugador? GetJugadorById(int jugadorId)
        {
            return _context.Jugadores.Find(jugadorId);
        }

        public Equipo? GetEquipoById(int equipoId)
        {
            return _context.Equipos.Find(equipoId);
        }

        public List<Equipo> GetEquipos()
        {
            return _context.Equipos.ToList();
        }

        public List<Jugador> GetJugadores()
        {
            return _context.Jugadores.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void AgregarNotificacion(Notificacion notificacion)
        {
            _context.Notificaciones.Add(notificacion);
        }

        public List<Notificacion> GetNotificacionesByEquipo(int equipoId)
        {
            return _context.Notificaciones
                .Include(n => n.Jugador)
                .Include(n => n.EquipoSolicitante)
                .Include(n => n.EquipoDueno)
                .Where(n => (n.EquipoDuenoId == equipoId || n.EquipoSolicitanteId == equipoId) && !n.Atendida)
                .OrderByDescending(n => n.Fecha)
                .ToList();
        }
        public Notificacion? GetNotificacionById(int notificacionId)
        {
            return _context.Notificaciones
                .Include(n => n.Jugador)
                .Include(n => n.EquipoSolicitante)
                .Include(n => n.EquipoDueno)
                .FirstOrDefault(n => n.Id == notificacionId);
        }
        public void UpdateNotificacion(Notificacion notificacion)
        {
            _context.Notificaciones.Update(notificacion);
        }
    }
}