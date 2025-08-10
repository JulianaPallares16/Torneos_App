using Torneos_App.src.Shared.Context;
using Torneos_App.src.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Torneos_App.src.Shared.Helpers
{
    public class DbContextFactory
    {
        public static AppDbContext Create()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            string? connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION")
                                ?? config.GetConnectionString("MySqlDB");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("No se encontró una cadena de conexión válida."); 
            var detectedVersion = MySqlVersionResolver.DetectVersion(connectionString);
            var minVersion = new Version(8, 0, 0);
            if (detectedVersion < minVersion)
                throw new NotSupportedException($"Versión de MySQL no soportada: {detectedVersion}. Requiere {minVersion} o superior.");

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, new MySqlServerVersion(detectedVersion))
                .Options;
            return new AppDbContext(options); 
        }
    }
}