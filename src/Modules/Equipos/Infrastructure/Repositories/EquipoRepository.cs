using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Torneos_App.src.Modules.Equipos.Application.Interfaces;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Shared.Context;

namespace Torneos_App.src.Modules.Equipos.Infrastructure.Repositories
{
    public class EquipoRepository : IEquipoRepository
    {
        private readonly AppDbContext _context;
        public EquipoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Equipo?> GetByIdAsync(int id)
        {
            return await _context.Equipos
                .Include(e => e.Torneos)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<IEnumerable<Equipo>> GetAllAsync()
        {
            return await _context.Equipos.ToListAsync();
        }
        public void Add(Equipo equipo)
        {
            _context.Equipos.Add(equipo);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        
    }
}