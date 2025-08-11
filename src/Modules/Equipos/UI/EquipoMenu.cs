using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Application.Service;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Equipos.Infrastructure.Repositories;
using Torneos_App.src.Modules.Torneos.Application.Services;
using Torneos_App.src.Modules.Torneos.Infrastructure.Repositories;
using Torneos_App.src.Shared.Context;

namespace Torneos_App.src.Modules.Equipos.UI;

public class EquipoMenu
{
    private readonly AppDbContext _context;
    readonly EquipoRepository repo = null!;
    readonly TorneoRepository torneorepo = null!;
    readonly EquipoService service = null!;
    public EquipoMenu(AppDbContext context)
    {
        _context = context;
        repo = new EquipoRepository(context);
        torneorepo = new TorneoRepository(context);
        service = new EquipoService(repo, torneorepo);
    }
    public async Task RenderMenu()
    {
        bool regresar = false;
        while (!regresar)
        {
            Console.Clear();
            Console.WriteLine(new string('*', 26));
            Console.WriteLine(" 🏟️  Gestor De Equipos 🏟️");
            Console.WriteLine(new string('*', 26));
            Console.WriteLine("1. Registrar Equipo");
            Console.WriteLine("2. Registrar Cuerpo Técnico");
            Console.WriteLine("3. Registrar Cuerpo Médico");
            Console.WriteLine("4. Inscripción Torneo");
            Console.WriteLine("5. Notificaciones");
            Console.WriteLine("6. Salir de Torneo");
            Console.WriteLine("7. Regresar");
            Console.WriteLine("Ingrese el número de la acción que desea realizar:");
            string? ope = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ope))
            {
                continue;
            }
            else
            {
                switch (ope)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("== Registrar Equipo ==");
                        Console.WriteLine("Ingrese el nombre del equipo: ");
                        string? nombre = Console.ReadLine();
                        Console.WriteLine("Ingrese el tipo de equipo (Selección/Local):");
                        string? tipo = Console.ReadLine();
                        Console.Write("Ingrese el país de origen del equipo: ");
                        string? pais = Console.ReadLine();
                        await service.RegistrarEquipoAsync(nombre!, tipo!, pais!);
                        Console.WriteLine("✅ Equipo registrado con éxito.");
                        Console.ReadKey();
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("=== Inscribir Equipo a Torneo ===");
                        var equipos = await service.ConsultarEquiposAsync();
                        Console.WriteLine("Equipos disponibles:");
                        foreach (var eq in equipos)
                        {
                            Console.WriteLine($"Id: {eq.Id} - Nombre: {eq.Nombre}");
                        }
                        Console.Write("Ingrese el Id del equipo a inscribir: ");
                        if (!int.TryParse(Console.ReadLine(), out int equipoId))
                        {
                            Console.WriteLine("Id inválido.");
                            Console.ReadKey();
                            break;
                        }
                        var torneoService = new TorneoService(new TorneoRepository(_context));
                        var torneos = await torneoService.ConsultaTorneoAsync();
                        Console.WriteLine("Torneos disponibles:");
                        foreach (var tor in torneos)
                        {
                            Console.WriteLine($"Id: {tor.Id} - Nombre: {tor.Nombre}");
                        }
                        Console.Write("Ingrese el Id del torneo al cual inscribir al equipo: ");
                        if (!int.TryParse(Console.ReadLine(), out int torneoId))
                        {
                            Console.WriteLine("Id inválido.");
                            Console.ReadKey();
                            break;
                        }
                        await service.InscribirATorneoAsync(equipoId, torneoId);
                        Console.WriteLine("✅ Equipo inscrito al torneo con éxito.");
                        break;
                    case "7":
                        Console.Clear();
                        Console.WriteLine("Adios...");
                        regresar = true;
                        break;

                }
            }
        }
    }
}
