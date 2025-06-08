// 2025-06-07

using System.Globalization;
using ForgottenToDoApp.Core.Enums;

namespace ForgottenToDoApp.MauiApp.Converters;

/// <summary>Convierte <see cref="Core.Enums.TaskStatus"/> a una cadena de texto amigable.</summary>
public class TaskStatusToTextConverter : IValueConverter {
	/// <summary>Retorna la cadena de texto correspondiente al estado de la tarea.</summary>
	/// <param name="value">El valor de <see cref="Core.Enums.TaskStatus"/> a convertir.</param>
	/// <param name="targetType">El tipo del destino de la asociación (no utilizado).</param>
	/// <param name="parameter">El parámetro del conversor (no utilizado).</param>
	/// <param name="culture">La cultura a usar en el conversor (no utilizada).</param>
	/// <returns>Una cadena ("Completada", "Pendiente" o "Desconocido") representando el estado.</returns>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is Core.Enums.TaskStatus status) {
			return status == Core.Enums.TaskStatus.Completed ? "Completada" : "Pendiente";
		}
		return "Desconocido";
	}

	/// <summary>No implementado. Lanza una <see cref="NotImplementedException"/>.</summary>
	/// <param name="value">El valor que se va a convertir de nuevo (no utilizado).</param>
	/// <param name="targetType">El tipo al que se va a convertir de nuevo (no utilizado).</param>
	/// <param name="parameter">El parámetro del conversor (no utilizado).</param>
	/// <param name="culture">La cultura a usar en el conversor (no utilizada).</param>
	/// <returns>No devuelve ningún valor; siempre lanza una excepción.</returns>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		throw new NotImplementedException();
	}
}