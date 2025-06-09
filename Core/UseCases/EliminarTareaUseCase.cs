// 2025-06-07

using System.Threading.Tasks;
using ForgottenToDoApp.Core.Interfaces;

namespace ForgottenToDoApp.Core.UseCases;

/// <sum>Caso de uso para eliminar una tarea.</sum>
public class EliminarTareaUseCase {
	private readonly IToDoTaskRepository _toDoTaskRepository;

	/// <sum>Inicializa una nueva instancia de EliminarTareaUseCase.</sum>
	/// <param name="toDoTaskRepository">El repositorio de tareas.</param>
	public EliminarTareaUseCase(IToDoTaskRepository toDoTaskRepository) {
		_toDoTaskRepository = toDoTaskRepository;
	}

	/// <sum>Ejecuta la eliminacion de una tarea por su ID.</sum>
	/// <param name="taskId">El identificador de la tarea a eliminar.</param>
	public async Task ExecuteAsync(string taskId) {
		await _toDoTaskRepository.DeleteAsync(taskId);
	}
}