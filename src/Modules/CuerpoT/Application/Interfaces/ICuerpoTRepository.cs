using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.CuerpoT.Domain.Entities;

namespace Torneos_App.src.Modules.CuerpoT.Application.Interfaces
{
    public interface ICuerpoTRepository
    {
        Task<IEnumerable<CuerpoTecnico?>> GetAllAsync();
        Task AddAsync(CuerpoTecnico tecnico);
        Task SaveAsync();
    }
}