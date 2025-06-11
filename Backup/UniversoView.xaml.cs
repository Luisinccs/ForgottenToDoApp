// 2025-06-09
using ForgottenToDoApp.ViewModels; // Asumo que IUniversoViewModel está aquí
using Microsoft.Maui.Controls;

namespace ForgottenToDoApp.Views;

/// <sum>UniversoView.xaml.cs: Code-behind para la vista principal del mapa mental.</sum>
public partial class UniversoView : ContentPage
{
	/// <sum>_viewModel: Referencia a la interfaz del ViewModel para esta vista.</sum>
	private readonly IUniversoViewModel _viewModel;

	/// <sum>UniversoView: Constructor de la vista UniversoView.</sum>
	/// <param name="viewModel">Implementación de IUniversoViewModel inyectada.</param>
	public UniversoView(IUniversoViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;

		// Aquí podrías añadir lógica adicional si es necesario,
		// por ejemplo, para suscribirse a eventos del ViewModel
		// o para manejar interacciones específicas de la UI que no se resuelvan con XAML puro.
	}

	/// <sum>OnAppearing: Se ejecuta cuando la vista aparece en pantalla.</sum>
	protected override void OnAppearing()
	{
		base.OnAppearing();
		// Si el ViewModel necesita cargar datos al aparecer la vista,
		// podrías llamar a un comando o método aquí.
		// Ejemplo: _viewModel.LoadDataCommand.Execute(null);
	}

	/// <sum>OnDisappearing: Se ejecuta cuando la vista desaparece de pantalla.</sum>
	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		// Limpieza o desuscripción de eventos aquí si es necesario.
	}
}