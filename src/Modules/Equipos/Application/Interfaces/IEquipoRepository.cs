using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Domain.Entities;

namespace Torneos_App.src.Modules.Equipos.Application.Interfaces
{
    public interface IEquipoRepository
    {
        Task<IEnumerable<Equipo>> GetAllAsync();
        Task<Equipo?> GetByIdAsync(int id);
        void Add(Equipo equipo);
        Task SaveAsync();
    }
}