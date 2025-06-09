// 2025-06-07

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration; // Necesario para ConfigurationBuilder

namespace ForgottenToDoApp.Infrastructure.Data;

/// <sum>Fabrica para la creacion de AppDbContext en tiempo de diseno.</sum>
/// <remarks>
/// Lee la configuracion de rutas del NAS desde appsettings.json para fines de diseno.
/// </remarks>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext> {
	/// <sum>Crea una nueva instancia de DbContext para tiempo de diseno.</sum>
	/// <param name="args">Argumentos de la linea de comandos.</param>
	/// <returns>Una nueva instancia de AppDbContext.</returns>
	public AppDbContext CreateDbContext(string[] args) {
		var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

		// --- DEPURACION: VERIFICA EL DIRECTORIO ACTUAL ---
        Console.WriteLine($"Directorio actual (Directory.GetCurrentDirectory()): {Directory.GetCurrentDirectory()}");
        Console.WriteLine($"Directorio base de la aplicacion (AppContext.BaseDirectory): {AppContext.BaseDirectory}");
        // --------------------------------------------------

		// Configurar la lectura de appsettings.json para tiempo de diseno
		var configuration = new ConfigurationBuilder()
			.SetBasePath(AppContext.BaseDirectory)
			//.SetBasePath(Directory.GetCurrentDirectory()) // Establece la base donde buscar appsettings.json
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

		var nasSettingsSection = configuration.GetSection("NasSettings");
		var sharePathWindows = nasSettingsSection["SharePathWindows"];
		var sharePathMac = nasSettingsSection["SharePathMac"];

		string nasPathForDesignTime;

		if (sharePathMac is null || sharePathWindows is null) {
			throw new Exception("Null path");
		}

		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
			nasPathForDesignTime = sharePathWindows;
		} else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
			nasPathForDesignTime = sharePathMac;
		} else {
			throw new PlatformNotSupportedException("La plataforma no es soportada para la operacion de herramientas de EF Core en tiempo de diseno.");
		}

		var nasPathProvider = new NasPathProvider(nasPathForDesignTime);
		var dbPath = nasPathProvider.GetDatabasePath();

		optionsBuilder.UseSqlite($"Filename={dbPath}");

		return new AppDbContext(optionsBuilder.Options);
	}
}