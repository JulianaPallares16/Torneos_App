using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Application.Interfaces;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Torneos.Application.Interfaces;

namespace Torneos_App.src.Modules.Equipos.Application.Service
{
    public class EquipoService : IEquipoService
    {
        private readonly IEquipoRepository _repo;
        private readonly ITorneoRepository _torneoRepo;

        public EquipoService(IEquipoRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<Equipo>> ConsultarEquiposAsync()
        {
            return await _repo.GetAllAsync();
        }
        public async Task RegistrarEquipoAsync(string nombre, string tipo, string pais)
        {
            var existentes = await _repo.GetAllAsync();

            if (existentes.Any(e => e.Nombre == nombre))
                throw new Exception("El equipo ya existe.");

            var equipo = new Equipo
            {
                Nombre = nombre,
                Tipo = tipo,
                Pais = pais
            };

            _repo.Add(equipo);
            await _repo.SaveAsync();
        }
        public async Task InscribirATorneoAsync(int equipoId, int torneoId)
        {
            var equipo = await _repo.GetByIdAsync(equipoId);
            if (equipo == null)
                throw new Exception("El equipo no existe.");

            var torneo = await _torneoRepo.GetByIdAsync(torneoId);
            if (torneo == null)
                throw new Exception("El torneo no existe.");

            equipo.TorneoId = torneoId;
            await _repo.SaveAsync();
        }
        public async Task SalirDeTorneoAsync(int equipoId)
        {
            var equipo = await _repo.GetByIdAsync(equipoId);
            if (equipo == null)
                throw new Exception("El equipo no existe.");

            equipo.TorneoId = null; 
            await _repo.SaveAsync();
        }
        public async Task<Equipo?> ObtenerEquipoPorIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
    }
}