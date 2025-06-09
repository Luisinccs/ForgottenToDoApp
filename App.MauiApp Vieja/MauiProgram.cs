// 2025-06-07

using ForgottenToDoApp.Core.Interfaces; // Para IToDoTaskRepository, ITaskGroupRepository
using ForgottenToDoApp.Core.UseCases; // Para los Casos de Uso
using ForgottenToDoApp.Infrastructure.Data; // Para AppDbContext, ToDoTaskRepository, TaskGroupRepository, NasPathProvider
using ForgottenToDoApp.MauiApp.Views; // Para DashboardPage
using ForgottenToDoApp.MauiApp.ViewModels; // Para DashboardViewModel
using Microsoft.EntityFrameworkCore; // Para los metodos de extension de EF Core
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration; // <--- AÑADE ESTE USING
using System.Reflection; // <--- AÑADE ESTE USING
using Microsoft.Maui.Devices;
using Microsoft.Maui.Controls.Hosting; // Necesario para DeviceInfo

namespace ForgottenToDoApp.MauiApp;

public static class MauiProgram {
	public static Microsoft.Maui.Hosting.MauiApp CreateMauiApp() {

		var builder = Microsoft.Maui.Hosting.MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts => {
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// --- Configurar la lectura de appsettings.json ---
		var a = Assembly.GetExecutingAssembly();
		// El nombre del recurso incrustado es usualmente <NamespaceDelEnsamblado>.<NombreArchivo>
		using var stream = a.GetManifestResourceStream("ForgottenToDoApp.MauiApp.appsettings.json");
		if (stream != null) {
			builder.Configuration.AddJsonStream(stream);
		} else {
			// Error crítico: appsettings.json no se encontró como recurso incrustado.
			throw new System.IO.FileNotFoundException("El archivo de configuración 'appsettings.json' no se encontró como recurso incrustado. Asegúrate de que el archivo exista en la raíz del proyecto MauiApp y que su 'Acción de compilación' esté establecida en 'EmbeddedResource'. Verifica también la salida de depuración para los nombres de los recursos incrustados.", "appsettings.json");
		}
		// --- Fin de Configuracion de appsettings.json ---


#if DEBUG
		builder.Logging.AddDebug();
		var resourceNames = a.GetManifestResourceNames();
		// foreach (var name in resourceNames) {
		// 	Console.WriteLine($"Recurso incrustado encontrado: {name}");
		// }
#endif
		// --- Registro de Servicios Personalizados ---

		// Obtener la seccion de configuracion del NAS
		var nasSettingsSection = builder.Configuration.GetSection("NasSettings");
		var sharePathWindows = nasSettingsSection["SharePathWindows"];
		var sharePathMac = nasSettingsSection["SharePathMac"];

		if (sharePathMac is null || sharePathWindows is null) {
			throw new Exception("Null path");
		}

		// Console.WriteLine($"mac path: {sharePathMac}");
		Console.WriteLine($"Windows path: {sharePathWindows}");

		// Determinar la ruta del NAS en tiempo de ejecucion de la aplicacion MAUI
		string nasSharePath;
		if (DeviceInfo.Current.Platform == DevicePlatform.WinUI) {
			nasSharePath = sharePathWindows;
		} else if (DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Current.Platform == DevicePlatform.macOS) {
			nasSharePath = sharePathMac;
		} else {
			throw new PlatformNotSupportedException($"La plataforma MAUI '{DeviceInfo.Current.Platform}' no esta soportada para el acceso a NAS.");
		}

		// Registrar el NasPathProvider con la ruta determinada (Singleton)
		builder.Services.AddSingleton(new NasPathProvider(nasSharePath));

		// Registrar el DbContext con la ruta del NAS
		builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) => {
			var nasPathProvider = serviceProvider.GetRequiredService<NasPathProvider>();
			var dbPath = nasPathProvider.GetDatabasePath();
			options.UseSqlite($"Filename={dbPath}");
		});
		
		// Registrar los repositorios
		builder.Services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
		builder.Services.AddScoped<ITaskGroupRepository, TaskGroupRepository>(); // Un solo registro es suficiente

		// Registrar los casos de uso
		builder.Services.AddScoped<CrearNuevaTareaUseCase>();
		builder.Services.AddScoped<ObtenerTareasDashboardUseCase>();
		builder.Services.AddScoped<MarcarTareaComoCompletadaUseCase>();
		builder.Services.AddScoped<ActualizarTareaUseCase>();
		builder.Services.AddScoped<EliminarTareaUseCase>();
		builder.Services.AddScoped<ObtenerTodasLasTareasUseCase>();
		builder.Services.AddTransient<ObtenerTareaPorIdUseCase>();
		builder.Services.AddScoped<CrearNuevoGrupoUseCase>();
		builder.Services.AddScoped<ObtenerTodosLosGruposUseCase>();

		// Registrar ViewModels
        builder.Services.AddTransient<DashboardViewModel>();
        builder.Services.AddTransient<TaskListViewModel>(); 
		builder.Services.AddTransient<TaskDetailViewModel>();

        // Registrar Vistas
		builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddTransient<TaskListPage>(); 
		builder.Services.AddTransient<TaskDetailPage>();
		
		// --- Fin del Registro de Servicios Personalizados ---

		return builder.Build();
	}

}