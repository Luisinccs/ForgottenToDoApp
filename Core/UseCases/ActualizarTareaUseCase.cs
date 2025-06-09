// 2025-06-07

using System.Threading.Tasks;
using ForgottenToDoApp.Core.Entities;
using ForgottenToDoApp.Core.Interfaces;

namespace ForgottenToDoApp.Core.UseCases;

/// <sum>Caso de uso para actualizar una tarea existente.</sum>
public class ActualizarTareaUseCase {
	private readonly IToDoTaskRepository _toDoTaskRepository;

	/// <sum>Inicializa una nueva instancia de ActualizarTareaUseCase.</sum>
	/// <param name="toDoTaskRepository">El repositorio de tareas.</param>
	public ActualizarTareaUseCase(IToDoTaskRepository toDoTaskRepository) {
		_toDoTaskRepository = toDoTaskRepository;
	}

	/// <sum>Ejecuta la actualizacion de una tarea.</sum>
	/// <param name="updatedTask">La tarea con los datos actualizados.</param>
	public async Task ExecuteAsync(ToDoTask updatedTask) {
		// Puedes agregar logica de negocio o validaciones aqui
		await _toDoTaskRepository.UpdateAsync(updatedTask);
	}
}