using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.CuerpoM.Application.Services;
using Torneos_App.src.Modules.CuerpoM.Infrastructure.Repositories;
using Torneos_App.src.Modules.CuerpoT.Application.Services;
using Torneos_App.src.Modules.CuerpoT.Infrastructure.Repositories;
using Torneos_App.src.Modules.Equipos.Application.Service;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Equipos.Infrastructure.Repositories;
using Torneos_App.src.Modules.Torneos.Application.Services;
using Torneos_App.src.Modules.Torneos.Infrastructure.Repositories;
using Torneos_App.src.Modules.Transferencias.Application.Services;
using Torneos_App.src.Modules.Transferencias.Infrastructure;
using Torneos_App.src.Shared.Context;

namespace Torneos_App.src.Modules.Equipos.UI;

public class EquipoMenu
{
    private readonly AppDbContext _context;
    readonly EquipoRepository repo = null!;
    readonly TorneoRepository torneorepo = null!;
    readonly CuerpoMedicoRepository cuerpoMrepo = null!;
    readonly CuerpoTRepository cuerpoTrepo = null!;
    readonly TransferenciaRepository transferenciarepo = null!;
    readonly EquipoService service = null!;
    private readonly TorneoService torneoService;
    private readonly CuerpoMedicoService cuerpoMService;
    private readonly CuerpoTecnicoService cuerpoTService;
    private readonly TransferenciaService transferenciaService;

    public EquipoMenu(AppDbContext context)
    {
        _context = context;
        repo = new EquipoRepository(context);
        torneorepo = new TorneoRepository(context);
        cuerpoMrepo = new CuerpoMedicoRepository(context);
        cuerpoTrepo = new CuerpoTRepository(context);
        service = new EquipoService(repo, torneorepo);
        torneoService = new TorneoService(torneorepo);
        cuerpoMService = new CuerpoMedicoService(cuerpoMrepo);
        cuerpoTService = new CuerpoTecnicoService(cuerpoTrepo);
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
                        Console.Clear();
                        Console.WriteLine("== Registrar Cuerpo Técnico ==");
                        var equiposCT = await service.ConsultarEquiposAsync();
                        Console.WriteLine("Equipos disponibles:");
                        foreach (var e in equiposCT)
                            Console.WriteLine($"Id: {e.Id} - Nombre: {e.Nombre}");
                        Console.WriteLine("Ingrese el Id del equipo: ");
                        if (!int.TryParse(Console.ReadLine(), out int equipoIdCT))
                        {
                            Console.WriteLine("Id inválido.");
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("Ingrese el nombre: ");
                        string? nombreCT = Console.ReadLine();
                        Console.WriteLine("Ingrese el apellido: ");
                        string? apellidoCT = Console.ReadLine();
                        int edadCT = LeerEntero("Ingrese la edad:");
                        Console.WriteLine("Ingrese la especialidad: ");
                        string? cargo = Console.ReadLine();
                        await cuerpoTService.RegistrarTecnicoAsync(nombreCT!, apellidoCT!, edadCT, cargo!, equipoIdCT);
                        Console.WriteLine("✅ Cuerpo técnico registrado con éxito.");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("== Registrar Cuerpo Médico ==");
                        var equiposCM = await service.ConsultarEquiposAsync();
                        Console.WriteLine("Equipos disponibles:");
                        foreach (var e in equiposCM)
                            Console.WriteLine($"Id: {e.Id} - Nombre: {e.Nombre}");
                        Console.WriteLine("Ingrese el Id del equipo: ");
                        if (!int.TryParse(Console.ReadLine(), out int equipoIdCM))
                        {
                            Console.WriteLine("Id inválido.");
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("Ingrese el nombre: ");
                        string? nombreCM = Console.ReadLine();
                        Console.WriteLine("Ingrese el apellido: ");
                        string? apellidoCM = Console.ReadLine();
                        int edadCM = LeerEntero("Ingrese la edad:");
                        Console.WriteLine("Ingrese el cargo: ");
                        string? especialidad = Console.ReadLine();
                        await cuerpoMService.RegistrarMedicoAsync(nombreCM!, apellidoCM!, edadCM, especialidad!, equipoIdCM);
                        Console.WriteLine("✅ Cuerpo médico registrado con éxito.");
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("=== Inscribir Equipo a Torneo ===");
                        var equipos = await service.ConsultarEquiposAsync();
                        Console.WriteLine("Equipos disponibles:");
                        foreach (var e in equipos)
                        {
                            Console.WriteLine($"Id: {e.Id} - Nombre: {e.Nombre}");
                        }
                        Console.WriteLine("Ingrese el Id del equipo a inscribir: ");
                        if (!int.TryParse(Console.ReadLine(), out int equipoId))
                        {
                            Console.WriteLine("Id inválido.");
                            Console.ReadKey();
                            break;
                        }
                        var torneos = await torneoService.ConsultaTorneoAsync();
                        Console.WriteLine("Torneos disponibles:");
                        foreach (var t in torneos)
                        {
                            Console.WriteLine($"Id: {t.Id} - Nombre: {t.Nombre}");
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
                        Console.ReadKey();
                        break;
                    case"5":
                        Console.Clear();
                        Console.WriteLine("=== Notificaciones ===");
                        var equiposn = await service.ConsultarEquiposAsync();
                        Console.WriteLine("Equipos disponibles:");
                        foreach (var e in equiposn)
                            Console.WriteLine($"Id: {e.Id} - Nombre: {e.Nombre}");
                        Console.WriteLine("Ingrese el Id del equipo para ver sus notificaciones: ");
                        if (!int.TryParse(Console.ReadLine(), out int equipoIdN))
                        {
                            Console.WriteLine("Id inválido.");
                            Console.ReadKey();
                            break;
                        }
                        var notificaciones = transferenciaService.ObtenerNotificaciones(equipoIdN);
                        if (notificaciones.Any())
                        {
                            Console.WriteLine("📢 Notificaciones:");
                            foreach (var n in notificaciones)
                            {
                                Console.WriteLine($"[{n.Fecha}] {n.Tipo} - {n.Mensaje} | Estado: {n.Estado}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay notificaciones.");
                        }
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("=== Salir de Torneo ===");
                        equipos = await service.ConsultarEquiposAsync();
                        Console.WriteLine("Equipos disponibles:");
                        foreach (var e in equipos)
                        {
                            Console.WriteLine($"Id: {e.Id} - Nombre: {e.Nombre}");
                        }
                        Console.Write("Ingrese el Id del equipo que desea retirar del torneo: ");
                        if (!int.TryParse(Console.ReadLine(), out equipoId))
                        {
                            Console.WriteLine("Id inválido.");
                            Console.ReadKey();
                            break;
                        }
                        torneos = await torneoService.ConsultaTorneoAsync();
                        Console.WriteLine("Torneos disponibles:");
                        foreach (var t in torneos)
                        {
                            Console.WriteLine($"Id: {t.Id} - Nombre: {t.Nombre}");
                        }
                        Console.Write("Ingrese el Id del torneo del cual desea retirar al equipo: ");
                        if (!int.TryParse(Console.ReadLine(), out torneoId))
                        {
                            Console.WriteLine("Id inválido.");
                            Console.ReadKey();
                            break;
                        }
                        await service.SalirDeTorneoAsync(equipoId, torneoId);
                        Console.WriteLine("✅ Equipo retirado del torneo con éxito.");
                        Console.ReadKey();
                        break;
                    case "7":
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
