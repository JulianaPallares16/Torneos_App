using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App_Torneos.src.Modules.Torneos.Domain.Entities;

namespace Torneos_App.src.Modules.Torneos.Application.Interfaces
{
    public interface ITorneoService
    {
        Task RegistrarTorneo(string nombre, string tipo, DateTime fechaInicio, DateTime fechaFin);
        Task ActualizarTorneo(int id, string nuevoNombre, string nuevoTipo, DateTime nuevoInicio, DateTime nuevoFin);
        Task EliminarTorneo(int id);
        Task<Torneo?> ObtenerTorneoPorIdAsync(int id);
        Task<IEnumerable<Torneo>> ConsultaTorneoAsync();
    }
}