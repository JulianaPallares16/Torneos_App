using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Torneos_App.src.Modules.Transferencias.Application.Interfaces
{
    public interface ITransferenciaService
    {
        Task RegistrarTransferencia(int jugadorId, int equipoOrigenId, int equipoDestinoId, decimal monto, DateTime fecha);
    }
}