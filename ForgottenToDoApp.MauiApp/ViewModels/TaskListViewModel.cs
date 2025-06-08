// 2025-06-07

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ForgottenToDoApp.Core.Entities;
using ForgottenToDoApp.Core.UseCases; // Usamos las clases concretas de casos de uso
using ForgottenToDoApp.Core.Enums; // Para TaskStatus y RepetitionFrequency
using System.Collections.ObjectModel;
using ForgottenToDoApp.MauiApp.Views;	
using System.Threading.Tasks;
using System;
using System.Linq; // Para LINQ en el filtro

namespace ForgottenToDoApp.MauiApp.ViewModels;

/// <sum>ViewModel para la pagina de Lista de Tareas.</sum>
public partial class TaskListViewModel : ObservableRecipient {
	private readonly ObtenerTodasLasTareasUseCase _obtenerTodasLasTareasUseCase;
	private readonly MarcarTareaComoCompletadaUseCase _marcarTareaComoCompletadaUseCase;

	/// <sum>Coleccion de todas las tareas cargadas.</sum>
	private ObservableCollection<ToDoTask> _allTasks = new();

	/// <sum>Coleccion de tareas visibles en la lista, basada en el filtro.</sum>
	[ObservableProperty]
	private ObservableCollection<ToDoTask> _visibleTasks = new();

	/// <sum>Indica si se estan mostrando tareas completadas.</sum>
	[ObservableProperty]
	[NotifyPropertyChangedRecipients] // Notifica a los suscriptores cuando cambia
	private bool _showCompletedTasks = false;

	/// <sum>Indica si la carga de datos esta en curso.</sum>
	[ObservableProperty]
	private bool _isLoading;

	/// <sum>Inicializa una nueva instancia de la clase TaskListViewModel.</sum>
	/// <param name="obtenerTodasLasTareasUseCase">Caso de uso para obtener todas las tareas.</param>
	/// <param name="marcarTareaComoCompletadaUseCase">Caso de uso para marcar tareas como completadas.</param>
	public TaskListViewModel(
		ObtenerTodasLasTareasUseCase obtenerTodasLasTareasUseCase,
		MarcarTareaComoCompletadaUseCase marcarTareaComoCompletadaUseCase) {
		_obtenerTodasLasTareasUseCase = obtenerTodasLasTareasUseCase;
		_marcarTareaComoCompletadaUseCase = marcarTareaComoCompletadaUseCase;
		// Suscribirse a los cambios de ShowCompletedTasks para actualizar la lista visible
		PropertyChanged += (s, e) => {
			if (e.PropertyName == nameof(ShowCompletedTasks)) {
				ApplyFilter();
			}
		};
		_ = LoadTasksAsync(); // Cargar tareas al inicializar
	}

	/// <sum>Comando para cargar todas las tareas.</sum>
	[RelayCommand]
	private async Task LoadTasksAsync() {
		IsLoading = true;
		var tasks = await _obtenerTodasLasTareasUseCase.ExecuteAsync();

		_allTasks.Clear();
		foreach (var task in tasks.OrderByDescending(t => t.FechaCreacion)) // Opcional: ordenar por fecha de creacion
		{
			_allTasks.Add(task);
		}
		ApplyFilter(); // Aplicar filtro despues de cargar
		IsLoading = false;
	}

	/// <sum>Comando para marcar una tarea como completada o incompleta.</sum>
	/// <param name="task">La tarea a marcar.</param>
	[RelayCommand]
	private async Task ToggleTaskCompletionAsync(ToDoTask task) {
		if (task == null) return;

		// Invertir el estado de completado
		var newStatus = (task.Estado == Core.Enums.TaskStatus.Completed) ? Core.Enums.TaskStatus.Pending : Core.Enums.TaskStatus.Completed;

		// Llamar al caso de uso. El caso de uso maneja la logica de repeticion y UltimaFechaCompletada.
		await _marcarTareaComoCompletadaUseCase.ExecuteAsync(task.Id, newStatus);

		// Recargar la lista para reflejar los cambios
		await LoadTasksAsync();
	}

	/// <sum>Comando para navegar a la pagina de detalles/edicion de una tarea.</sum>
	/// <param name="task">La tarea seleccionada.</param>
	[RelayCommand]
	private async Task GoToTaskDetailAsync(ToDoTask task) {
		if (task == null)
        {
            // Navegar para crear una nueva tarea.
            // Pasamos un parametro especial (ej. "new") para indicar que es una tarea nueva.
            await Shell.Current.GoToAsync($"{nameof(TaskDetailPage)}?id=new");
        }
        else
        {
            // Navegar para editar una tarea existente.
            // Pasamos el ID de la tarea como parametro.
            await Shell.Current.GoToAsync($"{nameof(TaskDetailPage)}?id={task.Id}");
        }
	}

	/// <sum>Aplica el filtro de tareas (completadas/pendientes) a la lista visible.</sum>
	private void ApplyFilter() {
		VisibleTasks.Clear();
		var filteredTasks = ShowCompletedTasks
			? _allTasks.Where(t => t.Estado == Core.Enums.TaskStatus.Completed)
			: _allTasks.Where(t => t.Estado == Core.Enums.TaskStatus.Pending || t.Estado == Core.Enums.TaskStatus.InProgress); // Mostrar pendientes y en progreso

		foreach (var task in filteredTasks.OrderBy(t => t.FechaVencimiento).ThenBy(t => t.Prioridad)) // Ordenar por fecha y prioridad
		{
			VisibleTasks.Add(task);
		}
	}

}