
using ForgottenToDoApp.Views.CommonTestHelpers;
using ForgottenToDoApp.Views.Ios;

[Register("AppDelegate")]
public class AppDelegate : UIApplicationDelegate {

	// Aquí se escoge la prueba que se desea ejecutar
	private static void RunTests() {

		// Crea una instancia de tu MockMapViewModel para probar
        var mockMapViewModel = new MockMapViewModel();

        // Crea una instancia de tu MapViewController, pasando el ViewModel
        var mapViewController = new MapViewController(mockMapViewModel);
		ArgumentNullException.ThrowIfNull(Window);
        // Asigna el MapViewController como el controlador de vista raiz de la ventana
        Window.RootViewController = mapViewController;
	}

	/// <summary>Prueba solo para ver si corre esta aplicación de pruebas</summary>
	private static void FirstTest(string? message) {
		
		// create a UIViewController with a single UILabel
		var vc = new UIViewController();
		vc.View!.AddSubview(new UILabel(Window!.Frame) {
			BackgroundColor = UIColor.SystemBackground,
			TextAlignment = UITextAlignment.Center,
			Text = message?? $"Hello, Ios!",
			AutoresizingMask = UIViewAutoresizing.All,
		});
		Window.RootViewController = vc;
	}

	public static new UIWindow? Window { get; set; }

	public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions) {

		Window = new UIWindow(UIScreen.MainScreen.Bounds);
		RunTests();
		// FirstTest("Hola");
		//try {
		//	RunTests();
		//} catch (Exception ex) {
		//	System.Diagnostics.Debug.WriteLine(ex.Message);
		//	FirstTest(ex.Message);
		//}

		Window.MakeKeyAndVisible();

		return true;
	}

}