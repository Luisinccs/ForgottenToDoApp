// 2025-06-10
using ForgottenToDo.UI.Maui;
using ForgottenToDoApp.App.Maui.Tests;
namespace ForgottenToDoApp.Views.Maui.Tests;

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