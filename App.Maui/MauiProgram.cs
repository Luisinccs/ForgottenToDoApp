// 2025-06-09
namespace ForgottenToDo.App.Maui;

public class App : Application {
	[Obsolete]
	public App() {
		ContentPage contentPage = new() {
			Content = new Label {
				Text = "Hello, Minimal MAUI",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			},
		};
		MainPage = contentPage;
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