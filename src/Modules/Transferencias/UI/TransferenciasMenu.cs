using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Torneos_App.src.Modules.Equipos.Application.Service;
using Torneos_App.src.Modules.Equipos.Domain.Entities;
using Torneos_App.src.Modules.Equipos.Infrastructure.Repositories;
using Torneos_App.src.Modules.Jugadores.Domain.Entities;
using Torneos_App.src.Modules.Transferencias.Application.Interfaces;
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
                        Console.WriteLine("==Adquirir Jugador==");
                        Console.WriteLine("Equipos disponibles:");
                        var equipos = service.ObtenerEquipos();
                        foreach (var e in equipos)
                        {
                            Console.WriteLine($"Id: {e.Id} - Nombre: {e.Nombre}");
                        }
                        Console.WriteLine("Ingrese el Id del equipo comprador");
                        if (!int.TryParse(Console.ReadLine(), out int equipoId))
                        {
                            Console.WriteLine("Id inválido.");
                            return;
                        }
                        var equipoComprador = equipos.FirstOrDefault(e => e.Id == equipoId);
                        if (equipoComprador == null)
                        {
                            Console.WriteLine("Equipo no encontrado.");
                            return;
                        }
                        Console.WriteLine("Jugadores disponibles:");
                        var jugadores = service.ObtenerJugadores();
                        foreach (var jugador in jugadores)
                        {
                            string estado = jugador.EquipoId == null ? "Libre" : $"En {jugador.Equipo?.Nombre}";
                            Console.WriteLine($"Id: {jugador.Id} - Nombre: {jugador.Nombre} ({estado})");
                        }

                        Console.WriteLine("Ingrese el Id del jugador a comprar: ");
                        if (!int.TryParse(Console.ReadLine(), out int jugadorId))
                        {
                            Console.WriteLine("Id inválido.");
                            return;
                        }
                        var jugadorSeleccionado = jugadores.FirstOrDefault(j => j.Id == jugadorId);
                        if (jugadorSeleccionado == null)
                        {
                            Console.WriteLine("Jugador no encontrado.");
                            return;
                        }
                        if (jugadorSeleccionado.EquipoId == null)
                        {
                            service.TransferirJugador(jugadorSeleccionado.Id, equipoComprador.Id);
                            Console.WriteLine($"{jugadorSeleccionado.Nombre} fue adquirido por {equipoComprador.Nombre}.");
                        }
                        else
                        {
                            Console.WriteLine($"{jugadorSeleccionado.Nombre} no está libre.");
                            Negociacion(jugadorSeleccionado, equipoComprador);

                        }
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Adios...");
                        regresar = true;
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Opción no valida");
                        break;
                }
            }
        }
    }
    private void Negociacion(Jugador jugadorSeleccionado, Equipo equipoComprador)
    {
        Console.WriteLine("¿Qué desea hacer?");
        Console.WriteLine("1. Solicitar compra");
        Console.WriteLine("2. Solicitar préstamo");
        Console.WriteLine("3. Cancelar");
        string? opn = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(opn))
        {
            return;
        }
        else
        {
            switch (opn)
            {
                case "1":
                    service.EnviarSolicitudCompra(jugadorSeleccionado.Id, equipoComprador.Id);
                    Console.WriteLine("✅ Solicitud de compra enviada al equipo dueño.");
                    Console.ReadKey();
                    break;

                case "2":
                    service.EnviarSolicitudPrestamo(jugadorSeleccionado.Id, equipoComprador.Id);
                    Console.WriteLine("✅ Solicitud de préstamo enviada al equipo dueño.");
                    break;

                case "3":
                    Console.WriteLine("Operación cancelada.");
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }
}

