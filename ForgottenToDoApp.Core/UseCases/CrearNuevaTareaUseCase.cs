// 2025-06-07

using System.Threading.Tasks;
using ForgottenToDoApp.Core.Entities;
using ForgottenToDoApp.Core.Interfaces;

namespace ForgottenToDoApp.Core.UseCases;

/// <sum>Caso de uso para crear una nueva tarea.</sum>
public class CrearNuevaTareaUseCase {
	private readonly IToDoTaskRepository _toDoTaskRepository;

	/// <sum>Inicializa una nueva instancia de CrearNuevaTareaUseCase.</sum>
	/// <param name="toDoTaskRepository">El repositorio de tareas.</param>
	public CrearNuevaTareaUseCase(IToDoTaskRepository toDoTaskRepository) {
		_toDoTaskRepository = toDoTaskRepository;
	}

	/// <sum>Ejecuta la creacion de una nueva tarea.</sum>
	/// <param name="newTask">La tarea a crear.</param>
	public async Task ExecuteAsync(ToDoTask newTask) {
		// Puedes agregar logica de negocio aqui antes de guardar la tarea,
		// por ejemplo, validaciones adicionales.
		await _toDoTaskRepository.AddAsync(newTask);
	}
}