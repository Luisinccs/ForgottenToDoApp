// 2025-06-07

using System.Collections.Generic;
using System.Threading.Tasks;
using ForgottenToDoApp.Core.Entities;
using ForgottenToDoApp.Core.Interfaces;

namespace ForgottenToDoApp.Core.UseCases;

/// <sum>Caso de uso para obtener todas las tareas.</sum>
public class ObtenerTodasLasTareasUseCase {
	private readonly IToDoTaskRepository _toDoTaskRepository;

	/// <sum>Inicializa una nueva instancia de ObtenerTodasLasTareasUseCase.</sum>
	/// <param name="toDoTaskRepository">El repositorio de tareas.</param>
	public ObtenerTodasLasTareasUseCase(IToDoTaskRepository toDoTaskRepository) {
		_toDoTaskRepository = toDoTaskRepository;
	}

	/// <sum>Ejecuta la obtencion de todas las tareas.</sum>
	/// <returns>Una coleccion de todas las tareas.</returns>
	public async Task<IEnumerable<ToDoTask>> ExecuteAsync() {
		return await _toDoTaskRepository.GetAllAsync();
	}
}