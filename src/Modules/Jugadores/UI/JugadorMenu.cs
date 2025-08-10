using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Jugadores.Application.Services;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Torneos_App.src.Modules.Jugadores.Infrastructure.Repositories;
using Torneos_App.src.Modules.Torneos.Application.Services;
using Torneos_App.src.Modules.Torneos.Infrastructure.Repositories;
using Torneos_App.src.Shared.Context;

namespace Torneos_App.src.Modules.Jugadores.UI;

public class JugadorMenu
{
    private readonly AppDbContext _context;
    readonly JugadorRepository repo = null!;
    readonly JugadorService service = null!;
    public JugadorMenu(AppDbContext context)
    {
        _context = context;
        repo = new JugadorRepository(context);
        service = new JugadorService(repo);
    }
    public async Task RenderMenu()
    {
        bool regresar = false;
        while (!regresar)
        {
            Console.Clear();
            Console.WriteLine(new string('*', 28));
            Console.WriteLine(" ⚽ Gestor De Jugadores ⚽");
            Console.WriteLine(new string('*', 28));
            Console.WriteLine("1. Registrar Jugador");
            Console.WriteLine("2. Buscar Jugador");
            Console.WriteLine("3. Editar Jugador");
            Console.WriteLine("4. Eliminar Jugador");
            Console.WriteLine("5. Regresar");
            Console.WriteLine("Ingrese el número de la acción que desea realizar:");
            string? opj = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(opj))
            {
                continue;
            }
            else
            {
                switch (opj)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("== Registrar Jugador ==");
                        Console.WriteLine("Ingrese el nombre del jugador:");
                        string? nombre = Console.ReadLine();
                        Console.WriteLine("Ingrese el apellido del jugador:");
                        string? apellido = Console.ReadLine();
                        int edad = LeerEntero("Ingrese la edad del juagor:");
                        int dorsal = LeerEntero("Ingrese la dorsal del jugador:");
                        Console.WriteLine("Ingrese la posicion en la que juega::");
                        string? posicion = Console.ReadLine();
                        await service.RegistrarJugadorAsync(nombre!, apellido!, edad, dorsal, posicion!);
                        Console.WriteLine("✅ Jugador registrado con exito.");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("== Buscar Jugador ==");
                        int id = LeerEntero("Ingrese el Id del jugador que desea buscar: ");
                        Jugador? jugador = await service.ObtenerJugadorPorIdAsync(id);
                        if (jugador != null)
                        {
                            Console.WriteLine("⭐ Jugador encontrado:");
                            Console.WriteLine($"Jugador: {jugador.Nombre}{jugador.Apellido}, Edad: {jugador.Edad}, Dorsal: {jugador.Dorsal}, Posición: {jugador.Posicion} ");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("⚠️ Id no encontrado.");
                        }
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("== Editar Jugador ==");
                        int idEditar = LeerEntero("Ingrese el id del jugador que desea editar:");
                        Console.WriteLine("Ingrese el nuevo nombre del Jugador");
                        string? nuevoNombre = Console.ReadLine();
                        Console.WriteLine("Ingrese el nuevo Apellido del jugador");
                        string? nuevoApellido = Console.ReadLine();
                        int nuevaEdad = LeerEntero("Ingrese la nueva edad del jugador");
                        int nuevoDorsal = LeerEntero("Ingrese la nueva dorsal del jugador");
                        Console.WriteLine("Ingrese la nueva posicion del jugador");
                        string? nuevaPosicion = Console.ReadLine();
                        Console.WriteLine("¿Está seguro de realizar la actualización? si/no");
                        string? answer = Console.ReadLine();
                        if (answer?.ToLower() != "si")
                        {
                            Console.WriteLine("Regresa al menú anterior.");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            await service.EditarJugadorAsync(idEditar, nuevoNombre!, nuevoApellido!, nuevaEdad, nuevoDorsal, nuevaPosicion!);
                            Console.WriteLine("✅ Jugador editado con éxito. ");
                            Console.ReadKey();
                        }
                        break;
                    case "4":
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("Adios...");
                        regresar = true;
                        break;
                    default:
                        Console.WriteLine("Opción no valida");
                        break;
                }
            }

        }
    }
    private int LeerEntero(string mensaje)
    {
        int valor;
        while (true)
        {
            Console.WriteLine(mensaje);
            if (int.TryParse(Console.ReadLine(), out valor))
                return valor;

            Console.WriteLine("⚠️ Ingrese un número válido.");
        }
    }
}

