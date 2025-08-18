using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Transferencias.Application.Services;
using Torneos_App.src.Modules.Transferencias.Infrastructure;
using Torneos_App.src.Shared.Context;

namespace Torneos_App.src.Modules.Transferencias.UI;

public class TransferenciasMenu
{
    private readonly AppDbContext _context;
    readonly TransferenciaRepository repo = null!;
    readonly TransferenciaService service = null!;
    public TransferenciasMenu(AppDbContext context)
    {
        _context = context;
        repo = new TransferenciaRepository(context);
        service = new TransferenciaService(repo);
    }
    public async Task RenderMenu()
    {
        bool regresar = false;
        while (!regresar)
        {
            Console.Clear();
            Console.WriteLine(new string('*', 30));
            Console.WriteLine(" 🔁 Gestor De Transferencias 🔁");
            Console.WriteLine(new string('*', 30));
            Console.WriteLine("1. Adquirir Jugador");
            Console.WriteLine("2. Regresar");
            Console.WriteLine("Ingrese el número de la acción que desea realizar:");
            string? opt = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(opt))
            {
                continue;
            }
            else
            {
                switch (opt)
                {
                    case "1":
                        Console.Clear();
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Opción no valida");
                        break;
                }
            }
        }  
    }
}

