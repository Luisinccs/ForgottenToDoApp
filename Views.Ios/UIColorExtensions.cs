// 2025-06-12

using UIKit; // Necesario para UIColor

namespace ForgottenToDoApp.Views.Ios; // Tu namespace donde estaran las extensiones, puede ser Common.

/// <summary>UIColorExtensions: Metodos de extension para la clase UIColor.</summary>
public static class UIColorExtensions {
	/// <summary>FromHexString: Convierte una cadena hexadecimal a un objeto UIColor.</summary>
	/// <param name="hexString">La cadena hexadecimal del color (ej. "#RRGGBB" o "RRGGBBAA").</param>
	/// <returns>Un objeto UIColor correspondiente a la cadena hexadecimal.</returns>
	public static UIColor FromHexStringValue(this string hexString) {

		var hex = hexString.Replace("#", string.Empty);
		nfloat r = 0, g = 0, b = 0, a = 1.0f; // Default alpha to 1.0 (fully opaque)

		if (hex.Length == 6) // #RRGGBB
		{
			r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
			g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
			b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
		} else if (hex.Length == 8) // #RRGGBBAA
		  {
			r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
			g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
			b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
			a = int.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
		} else {
			// Puedes manejar un error o lanzar una excepcion para formatos invalidos
			Console.WriteLine($"[WARNING] Invalid hex string format: {hexString}. Returning default color.");
			return UIColor.Black; // Color por defecto para strings invalidos
		}

		return UIColor.FromRGBA(r, g, b, a);
	}
}