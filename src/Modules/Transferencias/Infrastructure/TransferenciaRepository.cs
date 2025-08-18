using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Torneos_App.src.Modules.Transferencias.Application.Interfaces;
using Torneos_App.src.Modules.Transferencias.Domain.Entities;

namespace Torneos_App.src.Modules.Transferencias.Infrastructure
{
    public class TransferenciaRepository : ITransferenciaRepository
    {
        private readonly DbContext _context;

        public TransferenciaRepository(DbContext context)
        {
            _context = context;
        }

        public void Add(Transferencia transferencia)
        {
            _context.Set<Transferencia>().Add(transferencia);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}