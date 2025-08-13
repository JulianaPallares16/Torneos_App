using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Torneos_App.src.Modules.CuerpoM.Domain.Entities;
using Torneos_App.src.Modules.CuerpoT.Application.Interfaces;
using Torneos_App.src.Modules.CuerpoT.Domain.Entities;
using Torneos_App.src.Shared.Context;

namespace Torneos_App.src.Modules.CuerpoT.Infrastructure.Repositories
{
    public class CuerpoTRepository : ICuerpoTRepository
    {
        private readonly AppDbContext _context;

        public CuerpoTRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CuerpoTecnico?>> GetAllAsync() =>
            await _context.CuerposTecnicos.ToListAsync();

        public async Task AddAsync(CuerpoTecnico tecnico)
        {
            await _context.CuerposTecnicos.AddAsync(tecnico);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}