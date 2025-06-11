// 2025-06-11
using ForgottenToDoApp.ViewModels; // Namespace correcto para las interfaces del ViewModel
using Microsoft.Maui.Controls; // Namespace para ContentPage

namespace ForgottenToDoApp.Views.Maui; // Namespace correcto para el code-behind de la vista

/// <sum>MapPage.xaml.cs: Code-behind para la vista del mapa principal.</sum>
public partial class MapPage : ContentPage {
	/// <sum>_viewModel: Referencia a la interfaz del ViewModel para esta vista.</sum>
	private readonly IMapViewModel _viewModel;

	/// <sum>MapPage: Constructor de la vista MapPage.</sum>
	/// <param name="viewModel">Implementacion de IMapViewModel inyectada.</param>
	public MapPage(IMapViewModel viewModel) {
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

	/// <sum>OnAppearing: Se ejecuta cuando la vista aparece en pantalla.</sum>
	protected override void OnAppearing() {
		base.OnAppearing();
		if (_viewModel.CargarMapaCommand?.CanExecute(null) == true) {
			_viewModel.CargarMapaCommand.Execute(null);
		}
	}

	/// <sum>OnDisappearing: Se ejecuta cuando la vista desaparece de pantalla.</sum>
	protected override void OnDisappearing() {
		base.OnDisappearing();
	}
}