using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Torneos_App.src.Modules.Transferencias.Application.Interfaces;
using Torneos_App.src.Modules.Transferencias.Domain.Entities;

namespace Torneos_App.src.Modules.Transferencias.Application.Services
{
    public class TransferenciaService : ITransferenciaService
    {
        private readonly ITransferenciaRepository _repository;

        public TransferenciaService(ITransferenciaRepository repository)
        {
            _repository = repository;
        }

        public bool TransferirJugador(int jugadorId, int equipoDestinoId)
        {
            var jugador = _repository.GetJugadorById(jugadorId);
            var equipoDestino = _repository.GetEquipoById(equipoDestinoId);

            if (jugador == null || equipoDestino == null)
                return false;

            // ✅ Caso 1: jugador libre
            if (jugador.EquipoId == null)
            {
                jugador.EquipoId = equipoDestino.Id;
                _repository.SaveChanges();
                return true;
            }

            // ✅ Caso 2: jugador tiene equipo → aquí va la otra lógica de compra/venta
            // TODO: implementar lógica de negociación
            return false;
        }

        public List<Equipo> ObtenerEquipos()
        {
            return _repository.GetEquipos();
        }

        public List<Jugador> ObtenerJugadores()
        {
            return _repository.GetJugadores();
        }
    }
}