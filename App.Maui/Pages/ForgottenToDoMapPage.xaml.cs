// 2025-06-08

using ForgottenToDoApp.ViewModels;
using Microsoft.Maui.Controls; // Directiva para ContentPage y otras clases de UI.

namespace ForgottenToDo.UI.Maui;

/// <summary>Pagina que representa el mapa principal de areas de enfoque de ForgottenToDoApp.</summary>
public partial class ForgottenToDoMapPage : ContentPage {
	#region Variables

	/// <summary>ViewModel asociado a esta vista.</summary>
	private readonly IForgottenToDoMapViewModel _viewModel;

	#endregion

	#region Funciones Externas

	/// <summary>Constructor de la ForgottenToDoMapPage.</summary>
	/// <param name="viewModel">El ViewModel del mapa, inyectado por el sistema de dependencias.</param>
	public ForgottenToDoMapPage(IForgottenToDoMapViewModel viewModel) {
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

	#endregion
}