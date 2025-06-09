// 2025-06-07

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ForgottenToDoApp.Core.Entities;
using ForgottenToDoApp.Core.Interfaces; // Asegurarse de usar el namespace correcto

namespace ForgottenToDoApp.Infrastructure.Data;

/// <sum>Implementacion del repositorio de grupos de tareas utilizando Entity Framework Core con SQLite.</sum>
public class TaskGroupRepository : ITaskGroupRepository {
	private readonly AppDbContext _context;

	/// <sum>Inicializa una nueva instancia de TaskGroupRepository.</sum>
	/// <param name="context">El contexto de base de datos de la aplicacion.</param>
	public TaskGroupRepository(AppDbContext context) {
		_context = context;
	}

	/// <sum>Agrega un nuevo grupo de tareas de forma asincrona.</sum>
	/// <param name="group">El grupo a agregar.</param>
	public async Task AddAsync(TaskGroup group) {
		_context.TaskGroups.Add(group);
		await _context.SaveChangesAsync();
	}

	/// <sum>Elimina un grupo de tareas por su identificador de forma asincrona.</sum>
	/// <param name="id">El identificador unico del grupo a eliminar.</param>
	public async Task DeleteAsync(string id) {
		var group = await _context.TaskGroups.FindAsync(id);
		if (group != null) {
			_context.TaskGroups.Remove(group);
			await _context.SaveChangesAsync();
		}
	}

	/// <sum>Obtiene todos los grupos de tareas de forma asincrona.</sum>
	/// <returns>Una coleccion de todos los grupos de tareas.</returns>
	public async Task<IEnumerable<TaskGroup>> GetAllAsync() {
		return await _context.TaskGroups.ToListAsync();
	}

	/// <sum>Obtiene un grupo de tareas por su identificador unico de forma asincrona.</sum>
	/// <param name="id">El identificador unico del grupo.</param>
	/// <returns>El grupo de tareas encontrado, o null si no existe.</returns>
	public async Task<TaskGroup?> GetByIdAsync(string id) {
		return await _context.TaskGroups.FindAsync(id);
	}

	/// <sum>Actualiza un grupo de tareas existente de forma asincrona.</sum>
	/// <param name="group">El grupo de tareas a actualizar.</param>
	public async Task UpdateAsync(TaskGroup group) {
		_context.TaskGroups.Update(group);
		await _context.SaveChangesAsync();
	}
}