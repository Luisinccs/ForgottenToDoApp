// 2025-06-07

namespace ForgottenToDoApp.Core.Enums;

/// <sum>Enumeracion para el estado de una tarea.</sum>
public enum TaskStatus {
	/// <sum>La tarea esta pendiente.</sum>
	Pending,
	/// <sum>La tarea esta en progreso.</sum>
	InProgress,
	/// <sum>La tarea ha sido completada.</sum>
	Completed,
	/// <sum>La tarea ha sido pospuesta.</sum>
	Postponed
}