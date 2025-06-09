// 2025-06-07

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ForgottenToDoApp.Core.Entities;
using ForgottenToDoApp.Core.Enums; // Para las enumeraciones si las usaremos en el ViewModel
using ForgottenToDoApp.Core.UseCases; // Necesario para los casos de uso
using System.Threading.Tasks;
using System.Web; // Necesario para HttpUtility.UrlDecode

namespace ForgottenToDoApp.MauiApp.ViewModels;

/// <sum>ViewModel para la pagina de detalle de una tarea.</sum>
// Este atributo permite que Shell.Current.GoToAsync pase parametros directamente a las propiedades
[QueryProperty(nameof(TaskId), "id")]
public partial class TaskDetailViewModel : ObservableObject, IQueryAttributable {
	// Casos de Uso que seran inyectados a traves del constructor
	private readonly ObtenerTareaPorIdUseCase _obtenerTareaPorIdUseCase;
	private readonly CrearNuevaTareaUseCase _crearTareaUseCase;
	private readonly ActualizarTareaUseCase _actualizarTareaUseCase;

	/// <sum>El ID de la tarea que se esta editando, o "new" si es una nueva tarea.</sum>
	[ObservableProperty]
	private string _taskId = string.Empty;

	/// <sum>La tarea actual que se esta mostrando o editando.</sum>
	[ObservableProperty]
	private ToDoTask _currentTask = new();

	/// <sum>Indica si el ViewModel esta ocupado realizando una operacion (ej. guardando o cargando).</sum>
	[ObservableProperty]
	private bool _isBusy;

	/// <sum>Indica si se esta editando una tarea existente (true) o creando una nueva (false).</sum>
	[ObservableProperty]
	private bool _isEditingExistingTask;

	/// <sum>Constructor para inyeccion de dependencias.</sum>
	public TaskDetailViewModel(
		ObtenerTareaPorIdUseCase obtenerTareaPorIdUseCase,
		CrearNuevaTareaUseCase crearTareaUseCase,
		ActualizarTareaUseCase actualizarTareaUseCase) {
		_obtenerTareaPorIdUseCase = obtenerTareaPorIdUseCase;
		_crearTareaUseCase = crearTareaUseCase;
		_actualizarTareaUseCase = actualizarTareaUseCase;

		// Suscribirse a los cambios de la propiedad TaskId para cargar la tarea
		PropertyChanged += async (s, e) => {
			if (e.PropertyName == nameof(TaskId) && !string.IsNullOrEmpty(TaskId)) {
				await LoadTaskDataAsync();
			}
		};
	}
	/*	
	/// <sum>Metodo que MAUI invoca automaticamente para pasar los parametros de navegacion.</sum>
	/// <param name="query">Diccionario de parametros de la query string.</param>
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		// Al usar [QueryProperty], el valor ya se asigna a TaskId antes de llamar a este metodo.
		// Solo necesitamos asegurarnos de que el ID no sea null y el LoadTaskDataAsync se encargara.
		// Esta implementacion de ApplyQueryAttributes puede ser vacía o simplemente
		// llamar a LoadTaskDataAsync si TaskId ya está establecido.
		// La suscripción en el constructor se encargará de esto de forma reactiva.
	}
	*/
	
	/// <sum>Metodo que MAUI invoca automaticamente para pasar los parametros de navegacion.</sum>
	/// <param name="query">Diccionario de parametros de la query string.</param>
	public async void ApplyQueryAttributes(IDictionary<string, object> query) {
		IsBusy = true;
		try {
			// El parametro 'id' viene de la navegacion.
			// Lo decodificamos por si contiene caracteres especiales.
			var decodedTaskId = HttpUtility.UrlDecode(query["id"].ToString());

			if (decodedTaskId == "new") {
				// Es una nueva tarea, inicializamos con valores por defecto.
				CurrentTask = new ToDoTask {
					FechaCreacion = DateTime.Now,
					Estado = Core.Enums.TaskStatus.Pending,
					Prioridad = TaskPriority.Medium,
					EsRepetitiva = false,
					// Configurar duración por defecto para nuevas tareas
					DuracionEstimada = TimeSpan.FromHours(1) // Por defecto 1 hora
				};
				IsEditingExistingTask = false;
			} else {
				// Es una tarea existente, la cargamos por su ID
				var task = await _obtenerTareaPorIdUseCase.ExecuteAsync(decodedTaskId);
				if (task != null) {
					CurrentTask = task;
					IsEditingExistingTask = true;
				} else {
					// Manejar el caso donde la tarea no se encuentra.
					// Podriamos mostrar un mensaje o navegar de vuelta.
					Console.WriteLine($"Tarea con ID {decodedTaskId} no encontrada. Creando nueva tarea por defecto.");
					CurrentTask = new ToDoTask {
						FechaCreacion = DateTime.Now,
						Estado = Core.Enums.TaskStatus.Pending,
						Prioridad = TaskPriority.Medium,
						EsRepetitiva = false,
						DuracionEstimada = TimeSpan.FromHours(1) // Fallback a 1 hora si la tarea no se encuentra
					};
					IsEditingExistingTask = false;
				}
			}
		} finally {
			IsBusy = false;
		}
	}
	

	/// <sum>Comando para eliminar la tarea actual.</sum>
	/// <returns>Tarea asincrona.</returns>
	[RelayCommand]
	private async Task DeleteTaskAsync() {
		// Implementar la logica de eliminacion aqui
		// Por ejemplo, mostrar una alerta de confirmacion antes de eliminar
		bool confirm = await Shell.Current.DisplayAlert("Confirmar Eliminación", "¿Está seguro de que desea eliminar esta tarea?", "Sí", "No");
		if (confirm) {
			try {
				if (IsEditingExistingTask && !string.IsNullOrEmpty(CurrentTask.Id)) {
					// Asegurarse de tener un caso de uso de eliminación o inyectar un repositorio directamente
					// Si no lo tienes, deberás crear un caso de uso EliminarTareaUseCase y registrarlo.
					await Shell.Current.DisplayAlert("Error", "La lógica de eliminación no está implementada completamente.", "OK");
					// await _eliminarTareaUseCase.ExecuteAsync(CurrentTask.Id); // Esto es lo que harías
				}
				await Shell.Current.GoToAsync(".."); // Volver a la página anterior después de eliminar
			} catch (Exception ex) {
				await Shell.Current.DisplayAlert("Error", $"Error al eliminar tarea: {ex.Message}", "OK");
			}
		}
	}

	/// <sum>Carga los datos de la tarea basandose en el TaskId recibido.</sum>
	[RelayCommand] // Este comando se invocara internamente, no desde la UI
	private async Task LoadTaskDataAsync() {
		if (IsBusy) return; // Evitar multiples cargas simultaneas
		IsBusy = true;

		try {
			// Es buena practica decodificar los parametros de URL si contienen caracteres especiales
			var decodedTaskId = HttpUtility.UrlDecode(TaskId);

			if (decodedTaskId == "new") {
				// Es una nueva tarea, inicializamos una vacia con valores por defecto
				CurrentTask = new ToDoTask {
					// Puedes establecer valores por defecto aqui, por ejemplo:
					FechaCreacion = DateTime.Now,
					Estado = Core.Enums.TaskStatus.Pending,
					Prioridad = TaskPriority.Medium, // Asumiendo que TaskPriority existe y es un enum
					EsRepetitiva = false,
					DuracionMinutos = 60
				};
				IsEditingExistingTask = false;
			} else {
				// Es una tarea existente, la cargamos por su ID
				var task = await _obtenerTareaPorIdUseCase.ExecuteAsync(decodedTaskId);
				if (task != null) {
					CurrentTask = task;
					IsEditingExistingTask = true;
				} else {
					Console.WriteLine($"Tarea con ID {decodedTaskId} no encontrada. Creando nueva tarea por defecto.");
					CurrentTask = new ToDoTask {
						FechaCreacion = DateTime.Now,
						Estado = Core.Enums.TaskStatus.Pending,
						Prioridad = TaskPriority.Medium,
						EsRepetitiva = false,
						DuracionMinutos = 60 // Fallback a 60 minutos si la tarea no se encuentra
					};
					IsEditingExistingTask = false;
				}
			}
		} finally {
			IsBusy = false;
		}
	}


	/// <sum>Comando para guardar la tarea (nueva o existente).</sum>
	[RelayCommand]
	private async Task SaveTaskAsync() {
		if (IsBusy) return; // Evitar ejecuciones simultaneas
		IsBusy = true;

		try {
			if (IsEditingExistingTask) {
				// Actualizar tarea existente
				await _actualizarTareaUseCase.ExecuteAsync(CurrentTask);
			} else {
				// Crear nueva tarea
				await _crearTareaUseCase.ExecuteAsync(CurrentTask);
			}
			await Shell.Current.GoToAsync(".."); // Navegar de vuelta a la pagina anterior
		} finally {
			IsBusy = false;
		}
	}

	/// <sum>Comando para cancelar la edicion y volver a la pagina anterior sin guardar.</sum>
	[RelayCommand]
	private async Task CancelTaskAsync() {
		await Shell.Current.GoToAsync(".."); // Navegar de vuelta a la pagina anterior
	}

	/// <sum>Comando para cambiar el estado de completado de una tarea.</sum>
	/// <param name="task">La tarea a actualizar.</param>
	[RelayCommand]
	private void ToggleCompletion(ToDoTask task) {
		// Esta logica de toggle puede ser manejada en el TaskListViewModel
		// o a traves de un caso de uso especifico si se necesita mas control.
		// En el contexto de TaskDetailViewModel, si se completa una tarea,
		// se actualizaria su estado y se guardaria.
		if (CurrentTask != null) {
			if (CurrentTask.Estado == Core.Enums.TaskStatus.Completed) {
				CurrentTask.Estado = Core.Enums.TaskStatus.Pending;
				CurrentTask.FechaCompletado = null;
			} else {
				CurrentTask.Estado = Core.Enums.TaskStatus.Completed;
				CurrentTask.FechaCompletado = DateTime.Now;
			}
			// Esto activará la notificación de cambio para la UI si hay un binding
			// directamente a CurrentTask.Estado o se llama a OnPropertyChanged("CurrentTask")
		}
	}
}