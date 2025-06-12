// 2025-06-11

using System.Collections.ObjectModel;
using System.Windows.Input; // Para ICommand
using CommunityToolkit.Mvvm.Input; // Para RelayCommand y AsyncRelayCommand
using ForgottenToDoApp.ViewModels; // Usando el namespace de tus interfaces de ViewModel

namespace ForgottenToDoApp.Views.CommonTestHelpers; // Namespace para los mocks

/// <sum>MockMapViewModel: Implementacion de prueba para IMapViewModel.</sum>
public class MockMapViewModel : IMapViewModel {
	/// <sum>Title: Titulo del mapa.</sum>
	public string Title => "Mi Universo (Mock)";

	/// <sum>AreasDeEnfoque: Coleccion de areas de enfoque de prueba.</sum>
	public ObservableCollection<IAreaOfFocusViewModel> AreasDeEnfoque { get; }

	/// <sum>SeleccionarAreaDeEnfoqueCommand: Comando para seleccionar un area de enfoque.</sum>
	public ICommand SeleccionarAreaDeEnfoqueCommand { get; }

	/// <sum>CargarMapaCommand: Comando para cargar los datos del mapa.</sum>
	public ICommand CargarMapaCommand { get; }

	
	/// <sum>MockMapViewModel: Constructor del MockMapViewModel.</sum>
	public MockMapViewModel() {
		AreasDeEnfoque = new ObservableCollection<IAreaOfFocusViewModel>
		{
			new MockAreaOfFocusViewModel("Proyectos", "#A0E6FF", 50, 50),     // Cerca de la esquina superior izquierda
            new MockAreaOfFocusViewModel("Aprendizaje", "#D1FFB3", 250, 100),  // Más a la derecha, un poco más abajo
            new MockAreaOfFocusViewModel("Personal", "#FFC8A2", 100, 350),     // Más abajo, un poco a la derecha
            new MockAreaOfFocusViewModel("Empresa", "#FFE6A0", 350, 450)
		};

		// Simula sub-areas para Proyectos
		var proyectosArea = AreasDeEnfoque.FirstOrDefault(a => a.Nombre == "Proyectos");
		if (proyectosArea is MockAreaOfFocusViewModel mockProyectos) {
			// NOTA: Aquí se usan los métodos Add(), no el operador de asignación '='
			mockProyectos.SubAreas.Add(new MockAreaOfFocusViewModel("Proyecto X", "#C2F0FF", 400, 450));
			mockProyectos.SubAreas.Add(new MockAreaOfFocusViewModel("Proyecto Y", "#C2F0FF", 350, 700));
		}

		SeleccionarAreaDeEnfoqueCommand = new RelayCommand<IAreaOfFocusViewModel>(OnAreaSelected);
		CargarMapaCommand = new RelayCommand(CargarMapa);
	
	}

	/// <sum>CargarMapa: Logica para cargar los datos del mapa (mock).</sum>
	private void CargarMapa() {
		Console.WriteLine("MockMapViewModel: Datos del mapa cargados.");
		System.Diagnostics.Debug.WriteLine("MockMapViewModel: Datos del mapa cargados.");
	}

	/// <sum>OnAreaSelected: Maneja la seleccion de un area de enfoque (mock).</sum>
	/// <param name="area">El IAreaOfFocusViewModel seleccionado.</param>
	private void OnAreaSelected(IAreaOfFocusViewModel? area) {
		if (area != null) {
			Console.WriteLine($"MockMapViewModel: Área seleccionada: {area.Nombre}");
			System.Diagnostics.Debug.WriteLine($"MockMapViewModel: Área seleccionada: {area.Nombre}");
		}
	}

	/// <sum>OnAddArea: Maneja la accion de añadir un area (mock).</sum>
	private void OnAddArea() {
		Console.WriteLine("MockMapViewModel: Añadir nueva área (acción mock).");
	}

	
}