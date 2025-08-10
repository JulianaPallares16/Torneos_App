using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App_Torneos.src.Modules.Torneos.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;

namespace Torneos_App.src.Modules.Jugadores.Application.Interfaces
{
    public interface IJugadorRepository
    {
        Task<Jugador?> GetByIdAsync(int id);
        Task<IEnumerable<Jugador?>> GetAllAsync();
        void Add(Jugador jugador);
        void Remove(Jugador jugador);
        void Update(Jugador jugador);
        Task SaveAsync();
    }
}