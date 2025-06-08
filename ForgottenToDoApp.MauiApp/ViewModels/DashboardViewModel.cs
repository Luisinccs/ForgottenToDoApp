// 2025-06-07

using CommunityToolkit.Mvvm.ComponentModel; // Para ObservableObject
using CommunityToolkit.Mvvm.Input; // Para AsyncRelayCommand
using ForgottenToDoApp.Core.Entities;
using ForgottenToDoApp.Core.UseCases;
using System.Collections.ObjectModel; // Para ObservableCollection
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForgottenToDoApp.MauiApp.ViewModels;

/// <sum>ViewModel para la pagina del Dashboard.</sum>
public partial class DashboardViewModel : ObservableObject {
	private readonly ObtenerTareasDashboardUseCase _obtenerTareasDashboardUseCase;

	/// <sum>Coleccion de tareas vencidas que se mostraran en el dashboard.</sum>
	[ObservableProperty]
	private ObservableCollection<ToDoTask> _overdueTasks = new();

	/// <sum>Coleccion de tareas proximas a vencer que se mostraran en el dashboard.</sum>
	[ObservableProperty]
	private ObservableCollection<ToDoTask> _upcomingTasks = new();

	/// <sum>Indica si la carga de datos esta en curso.</sum>
	[ObservableProperty]
	private bool _isLoading;

	/// <sum>Inicializa una nueva instancia de la clase DashboardViewModel.</sum>
	/// <param name="obtenerTareasDashboardUseCase">Caso de uso para obtener tareas del dashboard.</param>
	public DashboardViewModel(ObtenerTareasDashboardUseCase obtenerTareasDashboardUseCase) {
		_obtenerTareasDashboardUseCase = obtenerTareasDashboardUseCase;
		_ = LoadDashboardDataAsync(); // Cargar datos al inicializar
	}

	/// <sum>Comando para recargar los datos del dashboard.</sum>
	[RelayCommand]
	private async Task LoadDashboardDataAsync() {
		IsLoading = true;
		(IEnumerable<ToDoTask> overdue, IEnumerable<ToDoTask> upcoming) = await _obtenerTareasDashboardUseCase.ExecuteAsync();

		OverdueTasks.Clear();
		foreach (var task in overdue) {
			OverdueTasks.Add(task);
		}

		UpcomingTasks.Clear();
		foreach (var task in upcoming) {
			UpcomingTasks.Add(task);
		}

		IsLoading = false;
	}
}