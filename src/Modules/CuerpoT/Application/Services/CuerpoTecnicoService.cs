using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.CuerpoT.Application.Interfaces;
using Torneos_App.src.Modules.CuerpoT.Domain.Entities;

namespace Torneos_App.src.Modules.CuerpoT.Application.Services
{
    public class CuerpoTecnicoService : ICuerpoTService
    {
        private readonly ICuerpoTRepository _repo;

        public CuerpoTecnicoService(ICuerpoTRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<CuerpoTecnico>> ConsultarTecnicoAsync()
        {
            return _repo.GetAllAsync()!;
        }
        public async Task RegistrarTecnicoAsync(string nombre, string apellido, int edad, string cargo, int equipoId)
        {
            var tecnico = new CuerpoTecnico
            {
                Nombre = nombre,
                Apellido = apellido,
                Edad = edad,
                Cargo = cargo,
                EquipoId = equipoId
            };

            await _repo.AddAsync(tecnico);
            await _repo.SaveAsync();
            
        }
    }
}