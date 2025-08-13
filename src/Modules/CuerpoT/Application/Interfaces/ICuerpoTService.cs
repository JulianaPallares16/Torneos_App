using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.CuerpoT.Domain.Entities;

namespace Torneos_App.src.Modules.CuerpoT.Application.Interfaces
{
    public interface ICuerpoTService
    {
        Task<IEnumerable<CuerpoTecnico>> ConsultarTecnicoAsync();
        Task RegistrarTecnicoAsync(string nombre, string apellido, int edad, string cargo, int equipoId);
    }
}