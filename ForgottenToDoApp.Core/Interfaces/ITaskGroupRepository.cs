// 2025-06-07

using System.Collections.Generic;
using System.Threading.Tasks;
using ForgottenToDoApp.Core.Entities; // Necesario para usar TaskGroup

namespace ForgottenToDoApp.Core.Interfaces;

/// <sum>Define el contrato para el repositorio de grupos de tareas.</sum>
public interface ITaskGroupRepository {

	/// <sum>Obtiene un grupo de tareas por su identificador unico.</sum>
	/// <param name="id">El identificador unico del grupo.</param>
	/// <returns>El grupo de tareas encontrado, o null si no existe.</returns>
	Task<TaskGroup?> GetByIdAsync(string id);

	/// <sum>Obtiene todos los grupos de tareas.</sum>
	/// <returns>Una coleccion de todos los grupos de tareas.</returns>
	Task<IEnumerable<TaskGroup>> GetAllAsync();

	/// <sum>Agrega un nuevo grupo de tareas.</sum>
	/// <param name="group">El grupo de tareas a agregar.</param>
	Task AddAsync(TaskGroup group);

	/// <sum>Actualiza un grupo de tareas existente.</sum>
	/// <param name="group">El grupo de tareas a actualizar.</param>
	Task UpdateAsync(TaskGroup group);

	/// <sum>Elimina un grupo de tareas por su identificador.</sum>
	/// <param name="id">El identificador unico del grupo a eliminar.</param>
	Task DeleteAsync(string id);

}