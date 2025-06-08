// 2025-06-07

using ForgottenToDoApp.MauiApp.ViewModels; // Para TaskListViewModel
using ForgottenToDoApp.Core.Entities; 

namespace ForgottenToDoApp.MauiApp.Views;

/// <sum>Pagina que muestra la lista completa de tareas.</sum>
public partial class TaskListPage : ContentPage {
	/// <sum>Inicializa una nueva instancia de la clase TaskListPage.</sum>
	/// <param name="viewModel">El ViewModel asociado a esta pagina.</param>
	public TaskListPage(TaskListViewModel viewModel) {
		InitializeComponent();
		BindingContext = viewModel;
	}

	/// <sum>Se ejecuta cuando la pagina aparece en pantalla.</sum>
	protected override void OnAppearing() {
		base.OnAppearing();
		// Cargar tareas cada vez que la pagina se hace visible
		if (BindingContext is TaskListViewModel viewModel) {
			viewModel.LoadTasksCommand.Execute(null);
		}
	}
}