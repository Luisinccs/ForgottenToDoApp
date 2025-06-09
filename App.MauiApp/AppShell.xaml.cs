// 2025-06-07

using ForgottenToDoApp.MauiApp.Views; // Asegurate de que este using este presente

namespace ForgottenToDoApp.MauiApp;

public partial class AppShell : Shell {
	public AppShell() {
		InitializeComponent();

		// Registrar las rutas de las paginas aqui
		Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
		Routing.RegisterRoute(nameof(TaskListPage), typeof(TaskListPage)); 
		Routing.RegisterRoute(nameof(Views.TaskDetailPage), typeof(Views.TaskDetailPage));
	}
}