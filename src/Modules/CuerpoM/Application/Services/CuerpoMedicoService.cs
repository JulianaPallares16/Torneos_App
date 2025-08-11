using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.CuerpoM.Application.Interfaces;
using Torneos_App.src.Modules.CuerpoM.Domain.Entities;
using Torneos_App.src.Modules.CuerpoM.Infrastructure.Repositories;

namespace Torneos_App.src.Modules.CuerpoM.Application.Services
{
    public class CuerpoMedicoService : ICuerpoMService
    {
        private readonly ICuerpoMRepository _repo;

        public CuerpoMedicoService(ICuerpoMRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<CuerpoMedico>> ConsultarMedicoAsync()
        {
            return _repo.GetAllAsync()!;
        }
        public async Task RegistrarMedicoAsync(string nombre, string apellido, int edad, string especialidad, int equipoId)
        {
            var medico = new CuerpoMedico
            {
                Nombre = nombre,
                Apellido = apellido,
                Edad = edad,
                Especialidad = especialidad,
                EquipoId = equipoId
            };

            await _repo.AddAsync(medico);
            await _repo.SaveAsync();
            
        }
    }
}