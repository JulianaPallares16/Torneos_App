using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;

namespace Torneos_App.src.Modules.Jugadores.Application.Interfaces
{
    public interface IJugadorService
    {
        Task RegistrarJugadorAsync(string nombre, string apellido, int edad, int dorsal, string posicion);
        Task EditarJugadorAsync(int id, string nuevoNombre, string nuevoApellido, int nuevaEdad, int nuevoDorsal, string nuevaPosicion);
        Task EliminarJugadorAsync(int id);
        Task<Jugador?> ObtenerJugadorPorIdAsync(int id);
        Task<IEnumerable<Jugador>> ConsultarJugadoresAsync();
    }
}