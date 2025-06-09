// 2025-06-07

using System.Collections.Generic;
using System.Threading.Tasks;
using ForgottenToDoApp.Core.Entities;
using ForgottenToDoApp.Core.Interfaces;

namespace ForgottenToDoApp.Core.UseCases;

/// <sum>Caso de uso para obtener todos los grupos de tareas.</sum>
public class ObtenerTodosLosGruposUseCase {
	private readonly ITaskGroupRepository _taskGroupRepository;

	/// <sum>Inicializa una nueva instancia de ObtenerTodosLosGruposUseCase.</sum>
	/// <param name="taskGroupRepository">El repositorio de grupos de tareas.</param>
	public ObtenerTodosLosGruposUseCase(ITaskGroupRepository taskGroupRepository) {
		_taskGroupRepository = taskGroupRepository;
	}

	/// <sum>Ejecuta la obtencion de todos los grupos de tareas.</sum>
	/// <returns>Una coleccion de todos los grupos de tareas.</returns>
	public async Task<IEnumerable<TaskGroup>> ExecuteAsync() {
		return await _taskGroupRepository.GetAllAsync();
	}
}