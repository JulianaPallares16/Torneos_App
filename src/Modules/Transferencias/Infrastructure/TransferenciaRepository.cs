using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Torneos_App.src.Modules.Notificaciones.Domain.Entities;
using Torneos_App.src.Modules.Transferencias.Application.Interfaces;
using Torneos_App.src.Modules.Transferencias.Domain.Entities;
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
        public Jugador? GetJugadorById(int jugadorId) => _context.Jugadores.Find(jugadorId);

        public Equipo? GetEquipoById(int equipoId) => _context.Equipos.Find(equipoId);

        public List<Equipo> GetEquipos() => _context.Equipos.ToList();

        public List<Jugador> GetJugadores() => _context.Jugadores.ToList();
        public Transferencia CrearTransferencia(Transferencia transferencia)
        {
            _context.Transferencias.Add(transferencia);
            return transferencia;
        }

        public Transferencia? GetTransferenciaById(int id)
        {
            return _context.Transferencias
                .Include(t => t.Jugador)
                .Include(t => t.EquipoDueno)
                .Include(t => t.EquipoComprador)
                .FirstOrDefault(t => t.Id == id);
        }

        public void UpdateTransferencia(Transferencia transferencia)
        {
            _context.Transferencias.Update(transferencia);
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
                .Include(n => n.Transferencia)
                .Where(n => (n.EquipoDuenoId == equipoId || n.EquipoSolicitanteId == equipoId) && !n.Atendida && n.Estado == "PENDIENTE")
                .OrderByDescending(n => n.Fecha)
                .ToList();
        }

        public Notificacion? GetNotificacionById(int notificacionId)
        {
            return _context.Notificaciones
                .Include(n => n.Jugador)
                .Include(n => n.EquipoSolicitante)
                .Include(n => n.EquipoDueno)
                .Include(n => n.Transferencia)
                .FirstOrDefault(n => n.Id == notificacionId);
        }

        public void UpdateNotificacion(Notificacion notificacion)
        {
            _context.Notificaciones.Update(notificacion);
        }
        public void SaveChanges() => _context.SaveChanges();
    }
}
