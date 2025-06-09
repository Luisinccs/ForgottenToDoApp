// 2025-06-07

using System.IO;
using System.Runtime.InteropServices; // Para la detección de SO básica, aunque la ruta la pasaremos

namespace ForgottenToDoApp.Infrastructure.Data;

/// <sum>Provee la ruta al archivo de base de datos en el NAS.</sum>
/// <remarks>
/// Esta clase ahora recibe la ruta base del NAS de forma externa para mantener la capa de infraestructura agnostica a la plataforma UI.
/// </remarks>
public class NasPathProvider {
	private const string DatabaseFileName = "ForgottenToDoApp.db";
	private readonly string _nasSharePath; // La ruta del NAS se inyectara/establecera

	/// <sum>Inicializa una nueva instancia de NasPathProvider con la ruta compartida del NAS.</sum>
	/// <param name="nasSharePath">La ruta compartida del NAS (ej. "\\IP\Share" para Windows o "/Volumes/Share" para macOS).</param>
	public NasPathProvider(string nasSharePath) {
		_nasSharePath = nasSharePath;
	}

	/// <sum>Obtiene la ruta completa al archivo de base de datos en el NAS.</sum>
	/// <returns>La ruta completa del archivo de base de datos.</returns>
	/// <exception cref="DirectoryNotFoundException">Se lanza si la ruta base del NAS no es accesible.</exception>
	public string GetDatabasePath() {
		// En este punto, _nasSharePath ya deberia ser la ruta especifica de la plataforma (Windows o macOS).
		// Solo validamos que sea accesible.
		if (!Directory.Exists(_nasSharePath)) {
			throw new DirectoryNotFoundException($"El directorio base del NAS '{_nasSharePath}' no es accesible o no existe. Verifica la configuracion y permisos de la unidad de red.");
		}

		return Path.Combine(_nasSharePath, DatabaseFileName);
	}
	
}