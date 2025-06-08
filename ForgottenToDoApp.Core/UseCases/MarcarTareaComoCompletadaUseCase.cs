// 2025-06-07

using System;
using System.Threading.Tasks;
using ForgottenToDoApp.Core.Entities;
using ForgottenToDoApp.Core.Enums; // Para TaskStatus y RepetitionFrequency
using ForgottenToDoApp.Core.Interfaces;

namespace ForgottenToDoApp.Core.UseCases;

/// <sum>Caso de uso para marcar una tarea como completada o incompleta y manejar la logica de repeticion.</sum>
public class MarcarTareaComoCompletadaUseCase {
	private readonly IToDoTaskRepository _toDoTaskRepository;

	/// <sum>Inicializa una nueva instancia de MarcarTareaComoCompletadaUseCase.</sum>
	/// <param name="toDoTaskRepository">El repositorio de tareas.</param>
	public MarcarTareaComoCompletadaUseCase(IToDoTaskRepository toDoTaskRepository) {
		_toDoTaskRepository = toDoTaskRepository;
	}

	/// <sum>Ejecuta el proceso de marcar una tarea con un estado especifico.</sum>
	/// <param name="taskId">El identificador de la tarea a actualizar.</param>
	/// <param name="newStatus">El nuevo estado de la tarea (Completed, Pending, etc.).</param>
	public async Task ExecuteAsync(string taskId, Core.Enums.TaskStatus newStatus) // <= ¡Firma del metodo actualizada!
	{
		ToDoTask? task = await _toDoTaskRepository.GetByIdAsync(taskId);

		if (task == null) {
			// Opcional: Lanzar una excepcion o registrar un error si la tarea no se encuentra
			throw new InvalidOperationException($"Tarea con ID {taskId} no encontrada.");
		}

		// Actualizar el estado de la tarea
		task.Estado = newStatus;

		if (newStatus == Core.Enums.TaskStatus.Completed) {
			task.UltimaFechaCompletada = DateTime.UtcNow; // Usar UtcNow para consistencia
			task.FechaCompletado = DateTime.UtcNow; // También se puede actualizar esta si se usa

			// Si es repetitiva, preparar la proxima ocurrencia
			if (task.EsRepetitiva) {
				// Calcular la proxima fecha de repeticion
				DateTime nextOccurrence = task.UltimaFechaCompletada.Value; // Base para el calculo

				switch (task.FrecuenciaRepeticion) {
					case RepetitionFrequency.Daily:
						nextOccurrence = nextOccurrence.AddDays(1);
						break;
					case RepetitionFrequency.Weekly:
						nextOccurrence = nextOccurrence.AddDays(7);
						break;
					case RepetitionFrequency.Monthly:
						nextOccurrence = nextOccurrence.AddMonths(1);
						break;
					case RepetitionFrequency.Annually:
						nextOccurrence = nextOccurrence.AddYears(1);
						break;
					case RepetitionFrequency.Custom:
						// Logica para repeticion personalizada, podria requerir mas datos en ToDoTask
						nextOccurrence = nextOccurrence.AddDays(1); // Ejemplo, ajustar segun el modelo personalizado
						break;
					case RepetitionFrequency.None:
					default:
						// No hacer nada si no es repetitiva o si la frecuencia es None
						break;
				}

				// Crear una nueva instancia de la tarea con la proxima fecha de vencimiento y estado pendiente
				// Este es el mecanismo de "reset" para que la tarea "reaparezca"
				ToDoTask nextTask = new ToDoTask {
					Titulo = task.Titulo,
					Descripcion = task.Descripcion,
					FechaCreacion = DateTime.UtcNow, // La nueva instancia tiene una nueva fecha de creacion
					FechaVencimiento = nextOccurrence,
					Estado = Core.Enums.TaskStatus.Pending, // El nuevo estado es pendiente
					EsRepetitiva = task.EsRepetitiva,
					FrecuenciaRepeticion = task.FrecuenciaRepeticion,
					GrupoId = task.GrupoId,
					Prioridad = task.Prioridad,
					DuracionMinutos = task.DuracionMinutos,
					EsDescartable = task.EsDescartable,
					ProximaFechaRepeticion = nextOccurrence // Se establece para la nueva tarea
				};

				await _toDoTaskRepository.AddAsync(nextTask); // Guardar la nueva instancia de la tarea
			}
		} else // Si el nuevo estado NO es completado (ej. Pending, InProgress, Postponed)
		  {
			task.UltimaFechaCompletada = null; // Limpiar si se marca como incompleta
			task.FechaCompletado = null; // También se puede limpiar esta
										 // Si la tarea se desmarca, y era repetitiva, la "proxima ocurrencia" de la original no se recrea.
										 // Si la logica es mas compleja y una tarea ya creada para la proxima ocurrencia debe ser eliminada
										 // o su estado ajustado, se necesitaria más implementacion.
		}

		// Actualizar la tarea original (marcandola con el nuevo estado)
		await _toDoTaskRepository.UpdateAsync(task);
	}
}