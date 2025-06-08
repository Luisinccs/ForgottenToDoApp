// 2025-06-07

using ForgottenToDoApp.Core.Entities;
using ForgottenToDoApp.Core.Interfaces;
using ForgottenToDoApp.Core.Enums; // Para acceder a los Enums

namespace ForgottenToDoApp.Core.UseCases;

/// <sum>Caso de uso para marcar una tarea como completada y manejar la logica de repeticion.</sum>
public class MarcarTareaComoCompletadaUseCase
{
    private readonly IToDoTaskRepository _toDoTaskRepository;

    /// <sum>Inicializa una nueva instancia de MarcarTareaComoCompletadaUseCase.</sum>
    /// <param name="toDoTaskRepository">El repositorio de tareas.</param>
    public MarcarTareaComoCompletadaUseCase(IToDoTaskRepository toDoTaskRepository)
    {
        _toDoTaskRepository = toDoTaskRepository;
    }

    /// <sum>Ejecuta el proceso de marcar una tarea como completada.</sum>
    /// <param name="taskId">El identificador de la tarea a completar.</param>
    public async Task ExecuteAsync(string taskId)
    {
        ToDoTask? task = await _toDoTaskRepository.GetByIdAsync(taskId);

        if (task == null)
        {
            // Opcional: Lanzar una excepcion o registrar un error si la tarea no se encuentra
            throw new InvalidOperationException($"Tarea con ID {taskId} no encontrada.");
        }

        // Marcar la tarea actual como completada
        task.Estado = Enums.TaskStatus.Completed;
        task.UltimaFechaCompletada = DateTime.Now;

        // Si es repetitiva, preparar la proxima ocurrencia
        if (task.EsRepetitiva)
        {
            // Calcular la proxima fecha de repeticion
            DateTime nextOccurrence = task.UltimaFechaCompletada.Value; // Base para el calculo

            switch (task.FrecuenciaRepeticion)
            {
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
                    // Por ahora, asumimos que se manejara externamente o sera mas simple.
                    nextOccurrence = nextOccurrence.AddDays(1); // Ejemplo, ajustar segun el modelo personalizado
                    break;
                case RepetitionFrequency.None:
                default:
                    // No hacer nada si no es repetitiva o si la frecuencia es None
                    break;
            }

            // Crear una nueva instancia de la tarea con la proxima fecha de vencimiento y estado pendiente
            // Este es el mecanismo de "reset" para que la tarea "reaparezca"
            ToDoTask nextTask = new ToDoTask
            {
                Titulo = task.Titulo,
                Descripcion = task.Descripcion,
                FechaCreacion = DateTime.Now, // La nueva instancia tiene una nueva fecha de creacion
                FechaVencimiento = nextOccurrence,
                Estado = Enums.TaskStatus.Pending,
                EsRepetitiva = task.EsRepetitiva,
                FrecuenciaRepeticion = task.FrecuenciaRepeticion,
                GrupoId = task.GrupoId,
                Prioridad = task.Prioridad,
                DuracionMinutos = task.DuracionMinutos,
                EsDescartable = task.EsDescartable,
                // UltimaFechaCompletada y ProximaFechaRepeticion se estableceran cuando esta se complete
            };

            await _toDoTaskRepository.AddAsync(nextTask); // Guardar la nueva instancia de la tarea
        }

        // Actualizar la tarea original (marcandola como completada)
        await _toDoTaskRepository.UpdateAsync(task);
    }
}