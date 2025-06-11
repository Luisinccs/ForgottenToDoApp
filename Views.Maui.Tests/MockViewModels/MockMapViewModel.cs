// 2025-06-11

using System.Collections.ObjectModel;
using System.Windows.Input; // Para ICommand
using CommunityToolkit.Mvvm.Input; // Para RelayCommand y AsyncRelayCommand
using ForgottenToDoApp.ViewModels; // Usando el namespace de tus interfaces de ViewModel

namespace ForgottenToDoApp.Mocks; // Namespace para los mocks

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

	/// <sum>AddAreaCommand: Comando para añadir una nueva area.</sum>
	public ICommand AddAreaCommand { get; }

	/// <sum>NavigateToListsCommand: Comando para navegar a la vista de listas.</sum>
	public ICommand NavigateToListsCommand { get; }

	/// <sum>NavigateToCalendarCommand: Comando para navegar a la vista de calendario.</sum>
	public ICommand NavigateToCalendarCommand { get; }

	/// <sum>NavigateToHabitsCommand: Comando para navegar a la vista de habitos.</sum>
	public ICommand NavigateToHabitsCommand { get; }

	/// <sum>NavigateToSettingsCommand: Comando para navegar a la vista de configuracion.</sum>
	public ICommand NavigateToSettingsCommand { get; }

	/// <sum>MockMapViewModel: Constructor del MockMapViewModel.</sum>
	public MockMapViewModel() {
		AreasDeEnfoque = new ObservableCollection<IAreaOfFocusViewModel>
		{
			new MockAreaOfFocusViewModel("Proyectos", "#A0E6FF", 600, 600),
			new MockAreaOfFocusViewModel("Aprendizaje", "#D1FFB3", 1200, 650),
			new MockAreaOfFocusViewModel("Personal", "#FFC8A2", 600, 1200),
			new MockAreaOfFocusViewModel("Empresa", "#FFE6A0", 1200, 1200)
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
		AddAreaCommand = new RelayCommand(OnAddArea);
		NavigateToListsCommand = new RelayCommand(OnNavigateToLists);
		NavigateToCalendarCommand = new RelayCommand(OnNavigateToCalendar);
		NavigateToHabitsCommand = new RelayCommand(OnNavigateToHabits);
		NavigateToSettingsCommand = new RelayCommand(OnNavigateToSettings);
	}

	/// <sum>CargarMapa: Logica para cargar los datos del mapa (mock).</sum>
	private void CargarMapa() {
		Console.WriteLine("MockMapViewModel: Datos del mapa cargados.");
	}

	/// <sum>OnAreaSelected: Maneja la seleccion de un area de enfoque (mock).</sum>
	/// <param name="area">El IAreaOfFocusViewModel seleccionado.</param>
	private void OnAreaSelected(IAreaOfFocusViewModel? area) {
		if (area != null) {
			Console.WriteLine($"MockMapViewModel: Área seleccionada: {area.Nombre}");
		}
	}

	/// <sum>OnAddArea: Maneja la accion de añadir un area (mock).</sum>
	private void OnAddArea() {
		Console.WriteLine("MockMapViewModel: Añadir nueva área (acción mock).");
	}

	/// <sum>OnNavigateToLists: Maneja la navegacion a la vista de listas (mock).</sum>
	private void OnNavigateToLists() {
		Console.WriteLine("MockMapViewModel: Navegar a la vista de Listas.");
	}

	/// <sum>OnNavigateToCalendar: Maneja la navegacion a la vista de calendario (mock).</sum>
	private void OnNavigateToCalendar() {
		Console.WriteLine("MockMapViewModel: Navegar a la vista de Calendario.");
	}

	/// <sum>OnNavigateToHabits: Maneja la navegacion a la vista de habitos (mock).</sum>
	private void OnNavigateToHabits() {
		Console.WriteLine("MockMapViewModel: Navegar a la vista de Hábitos.");
	}

	/// <sum>OnNavigateToSettings: Maneja la navegacion a la vista de configuracion (mock).</sum>
	private void OnNavigateToSettings() {
		Console.WriteLine("MockMapViewModel: Navegar a la vista de Configuración.");
	}
}