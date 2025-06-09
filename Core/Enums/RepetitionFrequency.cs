// 2025-06-07

namespace ForgottenToDoApp.Core.Enums;

/// <sum>Enumeracion para la frecuencia de repeticion de una tarea.</sum>
public enum RepetitionFrequency {
	/// <sum>La tarea no se repite.</sum>
	None,
	/// <sum>La tarea se repite diariamente.</sum>
	Daily,
	/// <sum>La tarea se repite semanalmente.</sum>
	Weekly,
	/// <sum>La tarea se repite mensualmente.</sum>
	Monthly,
	/// <sum>La tarea se repite anualmente.</sum>
	Annually,
	/// <sum>La tarea tiene una frecuencia de repeticion personalizada.</sum>
	Custom
}