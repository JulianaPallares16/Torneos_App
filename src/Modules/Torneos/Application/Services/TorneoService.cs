using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App_Torneos.src.Modules.Torneos.Domain.Entities;
using Torneos_App.src.Modules.Torneos.Application.Interfaces;

namespace Torneos_App.src.Modules.Torneos.Application.Services
{
    public class TorneoService : ITorneoService
    {
        private readonly ITorneoRepository _repo;
        public TorneoService(ITorneoRepository repo)
        {
            _repo = repo;
        }
        public Task<IEnumerable<Torneo>> ConsultaTorneoAsync()
        {
            return _repo.GetAllAsync()!;
        }
        public async Task RegistrarTorneo(string nombre, string tipo, DateTime fechaInicio, DateTime fechaFin)
        {
            var existentes = await _repo.GetAllAsync();
            if (existentes.Any(t => t.Nombre == nombre))
                throw new Exception("El torneo ya existe.");

            var torneo = new Torneo
            {
                Nombre = nombre,
                Tipo = tipo,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
            };
            _repo.Add(torneo);
            await _repo.SaveAsync(); 
        }
        public async Task ActualizarTorneo(int id, string nuevoNombre, string nuevoTipo, DateTime nuevoInicio, DateTime nuevoFin)
        {
            var torneo = await _repo.GetByIdAsync(id);

            if (torneo == null)
                throw new Exception($"⚠️ Id no encontrado.");

            torneo.Nombre = nuevoNombre;
            torneo.Tipo = nuevoTipo;
            torneo.FechaInicio = nuevoInicio;
            torneo.FechaFin = nuevoFin;

            _repo.Update(torneo);
            await _repo.SaveAsync();
        }
        public async Task EliminarTorneo(int id)
        {
            var torneo = await _repo.GetByIdAsync(id);
            if (torneo == null)
                throw new Exception($"⚠️ Id no encontrado.");
            _repo.Remove(torneo);
            await _repo.SaveAsync();
        }
        public async Task<Torneo?> ObtenerTorneoPorIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
    }
}