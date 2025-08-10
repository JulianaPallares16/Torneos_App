using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Torneos_App.src.Modules.Jugadores.Application.Interfaces;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Torneos_App.src.Shared.Context;

namespace Torneos_App.src.Modules.Jugadores.Infrastructure.Repositories
{
    public class JugadorRepository : IJugadorRepository
    {
        private readonly AppDbContext _context;
        public JugadorRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Jugador?> GetByIdAsync(int id)
        {
            return await _context.Jugadores
                .FirstOrDefaultAsync(j => j.Id == id);
        }
        public async Task<IEnumerable<Jugador?>> GetAllAsync() =>
            await _context.Jugadores.ToListAsync();
        public void Add(Jugador jugador) =>
            _context.Jugadores.Add(jugador);
        public void Remove(Jugador jugador) =>
            _context.Jugadores.Remove(jugador);
        public void Update(Jugador jugador) =>
            _context.SaveChanges();
        public async Task SaveAsync() =>
        await _context.SaveChangesAsync();
    }
}