using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Torneos_App.src.Modules.CuerpoM.Application.Interfaces;
using Torneos_App.src.Modules.CuerpoM.Domain.Entities;
using Torneos_App.src.Shared.Context;

namespace Torneos_App.src.Modules.CuerpoM.Infrastructure.Repositories
{
    public class CuerpoMedicoRepository : ICuerpoMRepository
    {
        private readonly AppDbContext _context;

        public CuerpoMedicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CuerpoMedico?>> GetAllAsync() =>
            await _context.CuerposMedicos.ToListAsync();

        public async Task AddAsync(CuerpoMedico medico)
        {
            await _context.CuerposMedicos.AddAsync(medico);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}