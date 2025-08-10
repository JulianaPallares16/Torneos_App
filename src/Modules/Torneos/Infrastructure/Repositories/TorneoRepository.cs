using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App_Torneos.src.Modules.Torneos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Torneos_App.src.Modules.Torneos.Application.Interfaces;
using Torneos_App.src.Shared.Context;

namespace Torneos_App.src.Modules.Torneos.Infrastructure.Repositories
{
    public class TorneoRepository : ITorneoRepository
    {
        private readonly AppDbContext _context;
        public TorneoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Torneo?> GetByIdAsync(int id)
        {
            return await _context.Torneos
                .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<IEnumerable<Torneo?>> GetAllAsync() =>
            await _context.Torneos.ToListAsync();
        public void Add(Torneo torneo) =>
            _context.Torneos.Add(torneo);
        public void Remove(Torneo torneo) =>
            _context.Torneos.Remove(torneo);
        public void Update(Torneo torneo) =>
            _context.SaveChanges();
        public async Task SaveAsync() =>
        await _context.SaveChangesAsync();
    }
}