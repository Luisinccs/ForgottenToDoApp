// 2025-06-07

namespace ForgottenToDoApp.MauiApp.Views;

/// <sum>Pagina para ver o editar los detalles de una tarea.</sum>
public partial class TaskDetailPage : ContentPage {
	/// <sum>Inicializa una nueva instancia de la clase TaskDetailPage.</sum>
	/// <param name="viewModel">El ViewModel asociado a esta pagina.</param>
	public TaskDetailPage(ViewModels.TaskDetailViewModel viewModel) {
		InitializeComponent();
		BindingContext = viewModel;
	}
}