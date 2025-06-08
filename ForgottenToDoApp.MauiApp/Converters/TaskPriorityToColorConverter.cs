// 2025-06-07

using System.Globalization;
using ForgottenToDoApp.Core.Enums; // Para TaskPriority
using Microsoft.Maui.Graphics; // Necesario para Color

namespace ForgottenToDoApp.MauiApp.Converters;

/// <summary>Convierte una <see cref="TaskPriority"/> a un <see cref="Color"/> para mostrar en la UI.</summary>
public class TaskPriorityToColorConverter : IValueConverter {
	/// <summary>Retorna el color correspondiente a la prioridad de la tarea.</summary>
	/// <param name="value">El valor de <see cref="TaskPriority"/> a convertir.</param>
	/// <param name="targetType">El tipo del destino de la asociación (no utilizado).</param>
	/// <param name="parameter">El parámetro del conversor (no utilizado).</param>
	/// <param name="culture">La cultura a usar en el conversor (no utilizada).</param>
	/// <returns>Un <see cref="Color"/> correspondiente a la prioridad, o un color por defecto si la conversión falla.</returns>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is TaskPriority priority) {
			return priority switch {
				TaskPriority.Low => Color.FromArgb("#8BC34A"), // Verde claro
				TaskPriority.Medium => Color.FromArgb("#FFC107"), // Ambar
				TaskPriority.High => Color.FromArgb("#F44336"), // Rojo
				_ => Color.FromArgb("#9E9E9E") // Gris por defecto
			};
		}
		return Colors.Gray; // Color por defecto
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