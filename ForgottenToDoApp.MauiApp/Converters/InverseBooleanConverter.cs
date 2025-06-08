// 2025-06-07

using System.Globalization;
using Microsoft.Maui.Controls; // Necesario para Binding.DoNothing

namespace ForgottenToDoApp.MauiApp.Converters;

/// <summary>Convierte un valor booleano a su inverso.</summary>
public class InverseBooleanConverter : IValueConverter {
	/// <summary>Convierte verdadero a falso y falso a verdadero.</summary>
	/// <param name="value">El valor booleano a invertir.</param>
	/// <param name="targetType">El tipo del destino de la asociación (no utilizado).</param>
	/// <param name="parameter">El parámetro del conversor (no utilizado).</param>
	/// <param name="culture">La cultura a usar en el conversor (no utilizada).</param>
	/// <returns>El inverso del valor booleano de entrada. Si la entrada no es un booleano, devuelve <see cref="Binding.DoNothing"/>.</returns>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is bool b) {
			return !b;
		}
		// Si el valor no es booleano, es mejor no intentar una conversión
		// o devolver un valor por defecto que no cause un error de binding.
		return Binding.DoNothing;
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