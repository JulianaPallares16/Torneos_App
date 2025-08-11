using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.CuerpoM.Domain.Entities;

namespace Torneos_App.src.Modules.CuerpoM.Application.Interfaces
{
    public interface ICuerpoMRepository
    {
        Task<IEnumerable<CuerpoMedico?>> GetAllAsync();
        Task AddAsync(CuerpoMedico medico);
        Task SaveAsync();
    }
}