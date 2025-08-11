using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Domain.Entities;

namespace Torneos_App.src.Modules.Equipos.Application.Interfaces
{
    public interface IEquipoService
    {
        Task RegistrarEquipoAsync(string nombre, string tipo, string pais);
        Task<IEnumerable<Equipo>> ConsultarEquiposAsync();
        Task<Equipo?> ObtenerEquipoPorIdAsync(int id);
        Task InscribirATorneoAsync(int equipoId, int torneoId);
        Task SalirDeTorneoAsync(int equipoId);
    }
}