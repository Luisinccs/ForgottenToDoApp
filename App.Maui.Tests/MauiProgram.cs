// 2025-06-09

using ForgottenToDo.UI.Maui;
namespace ForgottenToDoApp.App.Maui.Tests;

public class App : Application {

	public App() {
		// Prueba actual
		MainPage = new ForgottenToDoMapPage(new MockForgottenToDoMapViewModel());
		// Otras pruebas
		//MainPage = new ...;
	}

}

public static class MauiProgram {
	public static MauiApp CreateMauiApp() {
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts => {
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}