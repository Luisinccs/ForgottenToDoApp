// 2025-06-07

using System.Globalization;
using ForgottenToDoApp.Core.Enums; // Para TaskStatus

namespace ForgottenToDoApp.MauiApp.Converters;

/// <summary>Convierte un <see cref="Core.Enums.TaskStatus"/> a un nombre de archivo de icono.</summary>
public class TaskStatusToIconConverter : IValueConverter {
	/// <summary>Retorna el nombre del archivo de icono correspondiente al estado de la tarea.</summary>
	/// <param name="value">El valor de <see cref="Core.Enums.TaskStatus"/> a convertir.</param>
	/// <param name="targetType">El tipo del destino de la asociación (no utilizado).</param>
	/// <param name="parameter">El parámetro del conversor (no utilizado).</param>
	/// <param name="culture">La cultura a usar en el conversor (no utilizada).</param>
	/// <returns>El nombre del archivo de icono ("check_icon.png" o "uncheck_icon.png") o "uncheck_icon.png" por defecto.</returns>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is Core.Enums.TaskStatus status) {
			return status == Core.Enums.TaskStatus.Completed ? "check_icon.png" : "uncheck_icon.png";
			// Asegurate de tener check_icon.png y uncheck_icon.png en Resources/Images
		}
		return "uncheck_icon.png"; // Icono por defecto
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