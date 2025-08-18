using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Transferencias.Application.Interfaces;
using Torneos_App.src.Modules.Transferencias.Domain.Entities;

namespace Torneos_App.src.Modules.Transferencias.Application.Services
{
    public class TransferenciaService : ITransferenciaService
    {
        private readonly ITransferenciaRepository _repo;

        public TransferenciaService(ITransferenciaRepository repo)
        {
            _repo = repo;
        }

        public async Task RegistrarTransferencia(int jugadorId, int equipoOrigenId, int equipoDestinoId, decimal monto, DateTime fecha)
        {
            var transferencia = new Transferencia
            {
                JugadorId = jugadorId,
                EquipoOrigenId = equipoOrigenId,
                EquipoDestinoId = equipoDestinoId,
                Monto = monto,
            };

            _repo.Add(transferencia);
            await _repo.SaveAsync();
        }
    }
}