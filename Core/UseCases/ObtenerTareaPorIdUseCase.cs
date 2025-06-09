// 2025-06-07

using ForgottenToDoApp.Core.Entities;
using ForgottenToDoApp.Core.Interfaces;
using System.Threading.Tasks;

namespace ForgottenToDoApp.Core.UseCases;

/// <sum>Caso de uso para obtener una tarea por su identificador unico.</sum>
public class ObtenerTareaPorIdUseCase {
	// Este caso de uso dependera de una interfaz de repositorio para acceder a los datos.
	// Asumimos que 'IToDoTaskRepository' sera definida en otra parte (probablemente en Core.Interfaces o Infrastructure).
	private readonly IToDoTaskRepository _toDoTaskRepository;

	/// <sum>Inicializa una nueva instancia de la clase ObtenerTareaPorIdUseCase.</sum>
	/// <param name="toDoTaskRepository">El repositorio de tareas a utilizar para acceder a los datos.</param>
	public ObtenerTareaPorIdUseCase(IToDoTaskRepository toDoTaskRepository) {
		_toDoTaskRepository = toDoTaskRepository;
	}

	/// <sum>Ejecuta el caso de uso para obtener una tarea especifica.</sum>
	/// <param name="id">El identificador unico de la tarea a obtener.</param>
	/// <returns>La tarea encontrada, o null si no se encuentra ninguna tarea con el ID dado.</returns>
	public async Task<ToDoTask?> ExecuteAsync(string id) {
		if (string.IsNullOrWhiteSpace(id)) {
			// Puedes agregar logica para manejar IDs invalidos, como lanzar una excepcion
			// o simplemente devolver null.
			return null;
		}
		return await _toDoTaskRepository.GetByIdAsync(id);
	}
}

// Nota: El compilador se quejara de la ausencia de 'IToDoTaskRepository'.
// Esto es esperado y es nuestro proximo paso.