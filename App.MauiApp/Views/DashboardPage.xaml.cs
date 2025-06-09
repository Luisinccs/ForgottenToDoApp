// 2025-06-07

namespace ForgottenToDoApp.MauiApp.Views;

/// <sum>Pagina que muestra el dashboard de la aplicacion.</sum>
public partial class DashboardPage : ContentPage {
	/// <sum>Inicializa una nueva instancia de la clase DashboardPage.</sum>
	/// <param name="viewModel">El ViewModel asociado a esta pagina.</param>
	public DashboardPage(ViewModels.DashboardViewModel viewModel) {
		InitializeComponent();
		BindingContext = viewModel;
	}
}