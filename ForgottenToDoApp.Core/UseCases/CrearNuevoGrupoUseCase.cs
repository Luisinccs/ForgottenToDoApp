// 2025-06-07

using System.Threading.Tasks;
using ForgottenToDoApp.Core.Entities;
using ForgottenToDoApp.Core.Interfaces;

namespace ForgottenToDoApp.Core.UseCases;

/// <sum>Caso de uso para crear un nuevo grupo de tareas.</sum>
public class CrearNuevoGrupoUseCase {
    private readonly ITaskGroupRepository _taskGroupRepository;

    /// <sum>Inicializa una nueva instancia de CrearNuevoGrupoUseCase.</sum>
    /// <param name="taskGroupRepository">El repositorio de grupos de tareas.</param>
    public CrearNuevoGrupoUseCase(ITaskGroupRepository taskGroupRepository)
    {
        _taskGroupRepository = taskGroupRepository;
    }

    /// <sum>Ejecuta la creacion de un nuevo grupo de tareas.</sum>
    /// <param name="newGroup">El grupo a crear.</param>
    public async Task ExecuteAsync(TaskGroup newGroup)
    {
        await _taskGroupRepository.AddAsync(newGroup);
    }
}