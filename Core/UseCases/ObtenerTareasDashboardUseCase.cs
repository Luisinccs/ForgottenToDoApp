// 2025-06-07

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForgottenToDoApp.Core.Entities;
using ForgottenToDoApp.Core.Interfaces;

namespace ForgottenToDoApp.Core.UseCases;

/// <sum>Caso de uso para obtener las tareas del Dashboard (vencidas y proximas a vencer).</sum>
public class ObtenerTareasDashboardUseCase {
	private readonly IToDoTaskRepository _toDoTaskRepository;

	/// <sum>Inicializa una nueva instancia de ObtenerTareasDashboardUseCase.</sum>
	/// <param name="toDoTaskRepository">El repositorio de tareas.</param>
	public ObtenerTareasDashboardUseCase(IToDoTaskRepository toDoTaskRepository) {
		_toDoTaskRepository = toDoTaskRepository;
	}

	/// <sum>Ejecuta la obtencion de tareas vencidas y proximas a vencer.</sum>
	/// <param name="daysAhead">Numero de dias hacia adelante para considerar tareas proximas a vencer.</param>
	/// <returns>Tupla con colecciones de tareas vencidas y proximas a vencer.</returns>
	public async Task<(IEnumerable<ToDoTask> OverdueTasks, IEnumerable<ToDoTask> UpcomingTasks)> ExecuteAsync(int daysAhead = 7) {
		DateTime today = DateTime.Today;
		DateTime upcomingThreshold = today.AddDays(daysAhead);

		var allTasks = await _toDoTaskRepository.GetAllAsync(); // Se podria optimizar con consultas mas especificas si el repositorio lo permite

		var overdueTasks = allTasks.Where(t => t.FechaVencimiento.HasValue &&
												t.FechaVencimiento.Value.Date < today &&
												t.Estado != Enums.TaskStatus.Completed &&
												!t.EsDescartable)
									 .OrderBy(t => t.FechaVencimiento)
									 .ToList();

		var upcomingTasks = allTasks.Where(t => t.FechaVencimiento.HasValue &&
												 t.FechaVencimiento.Value.Date >= today &&
												 t.FechaVencimiento.Value.Date <= upcomingThreshold &&
												 t.Estado != Enums.TaskStatus.Completed &&
												 !t.EsDescartable)
									 .OrderBy(t => t.FechaVencimiento)
									 .ToList();

		return (overdueTasks, upcomingTasks);
	}
}