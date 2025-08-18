using Torneos_App.src.Shared.Helpers;
using Torneos_App.src.Modules.Torneos.UI;
using Torneos_App.src.Modules.Jugadores.UI;
using Torneos_App.src.Modules.Equipos.UI;
using System;
using System.Drawing.Printing;
using System.Drawing;

var context = DbContextFactory.Create();

bool salir = false;
while (!salir)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(new string('*', 53));
    Console.WriteLine("*** Bienvenido Al Gestor De Campeonatos De Fútbol ***");
    Console.WriteLine(new string('*', 53));
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("1. Torneo");
    Console.WriteLine("2. Equipos");
    Console.WriteLine("3. Jugadores");
    Console.WriteLine("4. Transferencias (Compra, Prestamo)");
    Console.WriteLine("5. Estadisticas");
    Console.WriteLine("6. Salir");
    Console.WriteLine("Ingrese el número de la sección ha acceder:");
    string? opcion = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(opcion))
    {
        continue;
    }
    else
    {
        switch (opcion)
        {
            case "1":
                await new TorneoMenu(context).RenderMenu();
                break;
            case "2":
                await new EquipoMenu(context).RenderMenu();
                break;
            case "3":
                await new JugadorMenu(context).RenderMenu();
                break;
            case "4":
                break;
            case "5":
                break;
            case "6":
                Console.Clear();
                Console.WriteLine("Vuleve pronto 👋");
                salir = true;
                break;
            default:
                Console.Clear();
                Console.WriteLine("Opción no valida");
                break;

        }

    }
}
