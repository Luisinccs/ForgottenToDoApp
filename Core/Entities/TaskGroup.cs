// 2025-06-07

using System;

namespace ForgottenToDoApp.Core.Entities;

/// <sum>Representa una agrupacion de tareas.</sum>
public class TaskGroup {
	/// <sum>Identificador unico del grupo.</sum>
	public string Id { get; set; } = Guid.NewGuid().ToString();

	/// <sum>Nombre del grupo de tareas.</sum>
	public string Nombre { get; set; } = string.Empty;

	/// <sum>Descripcion del grupo.</sum>
	public string? Descripcion { get; set; }
}