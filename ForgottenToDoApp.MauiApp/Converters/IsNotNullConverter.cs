// 2025-06-07

using System.Globalization;

namespace ForgottenToDoApp.MauiApp.Converters;

/// <summary>Convierte un valor a verdadero si no es nulo, falso si es nulo.</summary>
public class IsNotNullConverter : IValueConverter {
	/// <summary>Convierte un valor para determinar si no es nulo. Puede invertirse la lógica usando el parámetro "Inverse".</summary>
	/// <param name="value">El valor a evaluar.</param>
	/// <param name="targetType">El tipo del destino de la asociación (no utilizado).</param>
	/// <param name="parameter">Un parámetro opcional. Si es "Inverse", la lógica se invierte (devuelve verdadero si el valor es nulo).</param>
	/// <param name="culture">La cultura a usar en el conversor (no utilizada).</param>
	/// <returns>Verdadero si el valor no es nulo (o si es nulo y el parámetro es "Inverse"), falso en caso contrario.</returns>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		// El parameter='Inverse' no es necesario para esta version simple
		// Si el parameter es "Inverse", y el value es null, deberia devolver true
		// Si no hay parameter o es cualquier otra cosa, devolver true si no es null
		if (parameter as string == "Inverse") {
			return value == null;
		}
		return value != null;
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