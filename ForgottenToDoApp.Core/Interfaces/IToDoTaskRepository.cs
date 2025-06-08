// 2025-06-07

using System.Collections.Generic;
using System.Threading.Tasks;
using ForgottenToDoApp.Core.Entities; // Necesario para usar ToDoTask

namespace ForgottenToDoApp.Core.Interfaces;

/// <sum>Define el contrato para el repositorio de tareas pendientes.</sum>
public interface IToDoTaskRepository {
	
	/// <sum>Obtiene una tarea por su identificador unico.</sum>
	/// <param name="id">El identificador unico de la tarea.</param>
	/// <returns>La tarea encontrada, o null si no existe.</returns>
	Task<ToDoTask?> GetByIdAsync(string id);

	/// <sum>Obtiene todas las tareas.</sum>
	/// <returns>Una coleccion de todas las tareas.</returns>
	Task<IEnumerable<ToDoTask>> GetAllAsync();

	/// <sum>Obtiene todas las tareas que vencen en una fecha especifica o antes.</sum>
	/// <param name="date">La fecha limite para las tareas a obtener.</param>
	/// <returns>Una coleccion de tareas que vencen en o antes de la fecha especificada.</returns>
	Task<IEnumerable<ToDoTask>> GetDueOrOverdueTasksAsync(DateTime date);

	/// <sum>Obtiene todas las tareas proximas a vencer en un rango de dias.</sum>
	/// <param name="startDate">La fecha de inicio del rango.</param>
	/// <param name="endDate">La fecha de fin del rango.</param>
	/// <returns>Una coleccion de tareas que vencen dentro del rango especificado.</returns>
	Task<IEnumerable<ToDoTask>> GetUpcomingTasksAsync(DateTime startDate, DateTime endDate);

	/// <sum>Obtiene todas las tareas para un grupo especifico.</sum>
	/// <param name="groupId">El identificador del grupo.</param>
	/// <returns>Una coleccion de tareas pertenecientes al grupo especificado.</returns>
	Task<IEnumerable<ToDoTask>> GetTasksByGroupIdAsync(string groupId);

	/// <sum>Agrega una nueva tarea.</sum>
	/// <param name="task">La tarea a agregar.</param>
	Task AddAsync(ToDoTask task);

	/// <sum>Actualiza una tarea existente.</sum>
	/// <param name="task">La tarea a actualizar.</param>
	Task UpdateAsync(ToDoTask task);

	/// <sum>Elimina una tarea por su identificador.</sum>
	/// <param name="id">El identificador unico de la tarea a eliminar.</param>
	Task DeleteAsync(string id);
}