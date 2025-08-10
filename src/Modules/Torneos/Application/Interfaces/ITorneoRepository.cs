using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App_Torneos.src.Modules.Torneos.Domain.Entities;

namespace Torneos_App.src.Modules.Torneos.Application.Interfaces
{
    public interface ITorneoRepository
    {
        Task<Torneo?> GetByIdAsync(int id);
        Task<IEnumerable<Torneo?>> GetAllAsync();
        void Add(Torneo torneo);
        void Remove(Torneo torneo);
        void Update(Torneo torneo);
        Task SaveAsync();
    }
}