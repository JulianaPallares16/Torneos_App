using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Torneos.Application.Services;
using Torneos_App.src.Modules.Torneos.Infrastructure.Repositories;
using App_Torneos.src.Modules.Torneos.Domain.Entities;
using Torneos_App.src.Shared.Context;

namespace Torneos_App.src.Modules.Torneos.UI;

public class TorneoMenu
{
    private readonly AppDbContext _context;
    readonly TorneoRepository repo = null!;
    readonly TorneoService service = null!;
    public TorneoMenu(AppDbContext context)
    {
        _context = context;
        repo = new TorneoRepository(context);
        service = new TorneoService(repo);
    }
    public async Task RenderMenu()
    {
        bool regresar = false;
        while (!regresar)
        {
            Console.Clear();
            Console.WriteLine(new string('*', 25));
            Console.WriteLine(" 🏆 Gestor De Torneos 🏆");
            Console.WriteLine(new string('*', 25));
            Console.WriteLine("1. Crear Torneo");
            Console.WriteLine("2. Buscar Torneo");
            Console.WriteLine("3. Eliminar Torneo");
            Console.WriteLine("4. Actualizar Torneo");
            Console.WriteLine("5. Regresar");
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
                        Console.WriteLine("== Crear Torneo ==");
                        Console.WriteLine("Ingrese el nombre del torneo:");
                        string? nombre = Console.ReadLine();
                        Console.WriteLine("¿Qué tipo de torneo es? (Liga/Torneo Internacional)");
                        string? tipo = Console.ReadLine();
                        DateTime fechaInicio = LeerFecha("Ingrese la fecha de inicio (dd/MM/yyyy):");
                        DateTime fechaFin = LeerFecha("Ingrese la fecha de fin (dd/MM/yyyy):");
                        await service.RegistrarTorneo(nombre!, tipo!, fechaInicio, fechaFin);
                        Console.WriteLine("✅ Torneo creado con exito.");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("== Buscar Torneo ==");
                        int id = LeerEntero("Ingrese el Id del torneo que desea buscar: ");
                        Torneo? torneo = await service.ObtenerTorneoPorIdAsync(id);
                        if (torneo != null)
                        {
                            Console.WriteLine("⭐ Torneo encontrado:");
                            Console.WriteLine($"Torneo: {torneo.Nombre}, Tipo: {torneo.Tipo}, Fecha de inicio: {torneo.FechaInicio}, Fecha de fin: {torneo.FechaFin} ");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("⚠️ Id no encontrado.");
                        }
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("== Eliminar Torneo ==");
                        int eliminarId = LeerEntero("Ingrese el Id del torneo que desea eliminar: ");
                        while (true)
                        {
                            Console.WriteLine("¿Está seguro de eliminarlo? (si/no)");
                            string? eliminar = Console.ReadLine();
                            if (eliminar?.ToLower() != "si")
                            {
                                break;
                            }
                            else
                            {
                                await service.EliminarTorneo(eliminarId);
                                Console.WriteLine("🗑️ Torneo eliminado exitosamente.");
                                Console.ReadKey();
                                break;
                            }
                        }
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("== Actualizar Torneo ==");
                        int idActualizar = LeerEntero("Ingrese el Id del torneo que desea actualizar: ");
                        Console.WriteLine("Ingrese el nuevo nombre del torneo");
                        string? nuevoNombre = Console.ReadLine();
                        Console.WriteLine("Ingrese el nuevo tipo del torneo");
                        string? nuevoTipo = Console.ReadLine();
                        DateTime nuevoInicio = LeerFecha("Nueva fecha de inicio (dd/MM/yyyy): ");
                        DateTime nuevoFin = LeerFecha("Nueva fecha de fin (dd/MM/yyyy): ");
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
                            await service.ActualizarTorneo(idActualizar, nuevoNombre!, nuevoTipo!, nuevoInicio, nuevoFin);
                            Console.WriteLine("✅ Torneo actualizado con éxito. ");
                            Console.ReadKey();
                        }
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
    private DateTime LeerFecha(string mensaje)
    {
        DateTime fecha;
        while (true)
        {
            Console.WriteLine(mensaje);
            string? entrada = Console.ReadLine();
            if (DateTime.TryParseExact(entrada, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out fecha))
                return fecha;

            Console.WriteLine("⚠️ Formato inválido. Use dd/MM/yyyy.");
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
