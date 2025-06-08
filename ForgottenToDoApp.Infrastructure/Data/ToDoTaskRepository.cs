// 2025-06-07

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ForgottenToDoApp.Core.Entities;
using ForgottenToDoApp.Core.Interfaces; // Asegurarse de usar el namespace correcto

namespace ForgottenToDoApp.Infrastructure.Data;

/// <sum>Implementacion del repositorio de tareas utilizando Entity Framework Core con SQLite.</sum>
public class ToDoTaskRepository : IToDoTaskRepository {
	private readonly AppDbContext _context;

	/// <sum>Inicializa una nueva instancia de ToDoTaskRepository.</sum>
	/// <param name="context">El contexto de base de datos de la aplicacion.</param>
	public ToDoTaskRepository(AppDbContext context) {
		_context = context;
	}

	/// <sum>Agrega una nueva tarea de forma asincrona.</sum>
	/// <param name="task">La tarea a agregar.</param>
	public async Task AddAsync(ToDoTask task) {
		_context.ToDoTasks.Add(task);
		await _context.SaveChangesAsync();
	}

	/// <sum>Elimina una tarea por su identificador de forma asincrona.</sum>
	/// <param name="id">El identificador de la tarea a eliminar.</param>
	public async Task DeleteAsync(string id) {
		var task = await _context.ToDoTasks.FindAsync(id);
		if (task != null) {
			_context.ToDoTasks.Remove(task);
			await _context.SaveChangesAsync();
		}
	}

	/// <sum>Obtiene todas las tareas de forma asincrona.</sum>
	/// <returns>Una coleccion de todas las tareas.</returns>
	public async Task<IEnumerable<ToDoTask>> GetAllAsync() {
		return await _context.ToDoTasks.ToListAsync();
	}

	/// <sum>Obtiene una tarea por su identificador unico de forma asincrona.</sum>
	/// <param name="id">El identificador unico de la tarea.</param>
	/// <returns>La tarea encontrada, o null si no existe.</returns>
	public async Task<ToDoTask?> GetByIdAsync(string id) {
		return await _context.ToDoTasks.FindAsync(id);
	}

	/// <sum>Obtiene todas las tareas que vencen en una fecha especifica o antes de forma asincrona.</sum>
	/// <param name="date">La fecha limite para las tareas a obtener.</param>
	/// <returns>Una coleccion de tareas que vencen en o antes de la fecha especificada.</returns>
	public async Task<IEnumerable<ToDoTask>> GetDueOrOverdueTasksAsync(DateTime date) {
		return await _context.ToDoTasks
							 .Where(t => t.FechaVencimiento.HasValue && t.FechaVencimiento.Value.Date <= date.Date)
							 .ToListAsync();
	}

	/// <sum>Obtiene todas las tareas proximas a vencer en un rango de dias de forma asincrona.</sum>
	/// <param name="startDate">La fecha de inicio del rango.</param>
	/// <param name="endDate">La fecha de fin del rango.</param>
	/// <returns>Una coleccion de tareas que vencen dentro del rango especificado.</returns>
	public async Task<IEnumerable<ToDoTask>> GetUpcomingTasksAsync(DateTime startDate, DateTime endDate) {
		return await _context.ToDoTasks
							 .Where(t => t.FechaVencimiento.HasValue &&
										 t.FechaVencimiento.Value.Date >= startDate.Date &&
										 t.FechaVencimiento.Value.Date <= endDate.Date)
							 .ToListAsync();
	}

	/// <sum>Obtiene todas las tareas para un grupo especifico de forma asincrona.</sum>
	/// <param name="groupId">El identificador del grupo.</param>
	/// <returns>Una coleccion de tareas pertenecientes al grupo especificado.</returns>
	public async Task<IEnumerable<ToDoTask>> GetTasksByGroupIdAsync(string groupId) {
		return await _context.ToDoTasks
							 .Where(t => t.GrupoId == groupId)
							 .ToListAsync();
	}

	/// <sum>Actualiza una tarea existente de forma asincrona.</sum>
	/// <param name="task">La tarea a actualizar.</param>
	public async Task UpdateAsync(ToDoTask task) {
		_context.ToDoTasks.Update(task);
		await _context.SaveChangesAsync();
	}
}