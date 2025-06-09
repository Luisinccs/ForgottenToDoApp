using Foundation;

namespace ForgottenToDoApp.MauiApp;
using Microsoft.Maui.Hosting;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate {
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
