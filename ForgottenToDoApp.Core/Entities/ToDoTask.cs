// 2025-06-07

using ForgottenToDoApp.Core.Enums;

namespace ForgottenToDoApp.Core.Entities;

/// <sum>Representa la entidad principal de una tarea dentro de la aplicacion.</sum>
public class ToDoTask {

	/// <sum>Identificador unico de la tarea.</sum>
	public string Id { get; set; } = Guid.NewGuid().ToString();

	/// <sum>Titulo descriptivo de la tarea.</sum>
	public string Titulo { get; set; } = string.Empty;

	/// <sum>Descripcion detallada de la tarea.</sum>
	public string Descripcion { get; set; } = string.Empty;

	/// <sum>Fecha y hora de creacion de la tarea.</sum>
	public DateTime FechaCreacion { get; set; } = DateTime.Now;

	/// <sum>Fecha de vencimiento de la tarea.</sum>
	public DateTime? FechaVencimiento { get; set; }

	/// <sum>Fecha en que la tarea fue completada.</sum>
	public DateTime? FechaCompletado { get; set; } // Añade esta linea

	/// <sum>Estado actual de la tarea (Pendiente, Completada, etc.).</sum>
	public Enums.TaskStatus Estado { get; set; } = Enums.TaskStatus.Pending;

	/// <sum>Indica si la tarea es repetitiva.</sum>
	public bool EsRepetitiva { get; set; }

	/// <sum>Define la frecuencia de repeticion de la tarea (Diario, Semanal, etc.).</sum>
	public RepetitionFrequency FrecuenciaRepeticion { get; set; } = Enums.RepetitionFrequency.None;

	/// <sum>Fecha de la ultima vez que la tarea repetitiva fue completada.</sum>
	public DateTime? UltimaFechaCompletada { get; set; }

	/// <sum>Fecha de la proxima ocurrencia para tareas repetitivas.</sum>
	public DateTime? ProximaFechaRepeticion { get; set; }

	/// <sum>Identificador del grupo al que pertenece la tarea.</sum>
	public string? GrupoId { get; set; }

	/// <sum>Nivel de prioridad de la tarea.</sum>
	public TaskPriority Prioridad { get; set; } = TaskPriority.Medium;

	/// <sum>Duracion estimada de la tarea en minutos.</sum>
	public int? DuracionMinutos { get; set; }

	/// <sum>Indica si la tarea es opcional y su incumplimiento no es acumulativo.</sum>
	public bool EsDescartable { get; set; }
}