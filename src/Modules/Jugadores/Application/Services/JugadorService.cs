using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Jugadores.Application.Interfaces;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;

namespace Torneos_App.src.Modules.Jugadores.Application.Services
{
    public class JugadorService : IJugadorService
    {
    private readonly IJugadorRepository _repo;
    public JugadorService(IJugadorRepository repo)
    {
        _repo = repo;
    }
    public Task<IEnumerable<Jugador>> ConsultarJugadoresAsync()
    {
        return _repo.GetAllAsync()!;
    }
    public async Task RegistrarJugadorAsync(string nombre, string apellido, int edad, int dorsal, string posicion)
    {
        var existentes = await _repo.GetAllAsync();
        if (existentes.Any(j => j.Nombre == nombre && j.Apellido == apellido))
            throw new Exception("El jugador ya existe.");

        var jugador = new Jugador
        {
            Nombre = nombre,
            Apellido = apellido,
            Edad = edad,
            Dorsal = dorsal,
            Posicion = posicion,
        };
        _repo.Add(jugador);
        await _repo.SaveAsync();
    }
    public async Task EditarJugadorAsync(int id, string nuevoNombre, string nuevoApellido, int nuevaEdad, int nuevoDorsal, string nuevaPosicion)
    {
        var jugador = await _repo.GetByIdAsync(id);

        if (jugador == null)
            throw new Exception($"⚠️ Id no encontrado.");

        jugador.Nombre = nuevoNombre;
        jugador.Apellido = nuevoApellido;
        jugador.Edad = nuevaEdad;
        jugador.Dorsal = nuevoDorsal;
        jugador.Posicion = nuevaPosicion;

        _repo.Update(jugador);
        await _repo.SaveAsync();
    }
    public async Task EliminarJugadorAsync(int id)
    {
        var jugador = await _repo.GetByIdAsync(id);
        if (jugador == null)
            throw new Exception($"⚠️ Id no encontrado.");
        _repo.Remove(jugador);
        await _repo.SaveAsync();
    }
    public async Task<Jugador?> ObtenerJugadorPorIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }
    }
}