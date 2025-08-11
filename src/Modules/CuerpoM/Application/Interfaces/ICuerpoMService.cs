using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.CuerpoM.Domain.Entities;

namespace Torneos_App.src.Modules.CuerpoM.Application.Interfaces
{
    public interface ICuerpoMService
    {
        Task<IEnumerable<CuerpoMedico>> ConsultarMedicoAsync();
        Task RegistrarMedicoAsync(string nombre, string apellido, int edad, string especialidad, int equipoId);

    }
}