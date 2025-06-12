// 2025-06-11

using System.Collections.ObjectModel;
// Se elimina: using Microsoft.Maui.Graphics; // No se necesita para un ViewModel agnostico
using ForgottenToDoApp.ViewModels; // Usando el namespace de tus interfaces de ViewModel

namespace ForgottenToDoApp.Views.CommonTestHelpers; // Namespace para los mocks

/// <sum>MockAreaOfFocusViewModel: Implementacion de prueba para IAreaOfFocusViewModel.</sum>
public class MockAreaOfFocusViewModel : IAreaOfFocusViewModel {
	/// <sum>Nombre: Nombre del area de enfoque.</sum>
	public string Nombre { get; }

	/// <sum>ColorHex: Color en formato hexadecimal para el area de enfoque.</sum>
	public string ColorHex { get; }

	/// <sum>SubAreas: Coleccion de sub-areas de enfoque.</sum>
	public ObservableCollection<IAreaOfFocusViewModel> SubAreas { get; }

	/// <sum>PosicionX: Coordenada X del area de enfoque en el mapa.</sum>
	public double PosicionX { get; set; }

	/// <sum>PosicionY: Coordenada Y del area de enfoque en el mapa.</sum>
	public double PosicionY { get; set; }

	/// <sum>Ancho: Ancho del nodo en el mapa.</sum>
	public double Ancho { get; }

	/// <sum>Alto: Alto del nodo en el mapa.</sum>
	public double Alto { get; }

	// Se elimina: PosicionRect; ahora las propiedades son individuales

	/// <sum>MockAreaOfFocusViewModel: Constructor del MockAreaOfFocusViewModel.</sum>
	/// <param name="nombre">Nombre del area.</param>
	/// <param name="colorHex">Color hexadecimal del area.</param>
	/// <param name="posX">Posicion X del nodo.</param>
	/// <param name="posY">Posicion Y del nodo.</param>
	/// <param name="ancho">Ancho del nodo.</param>
	/// <param name="alto">Alto del nodo.</param>
	public MockAreaOfFocusViewModel(string nombre, string colorHex, double posX, double posY, double ancho = 150, double alto = 100) {
		Nombre = nombre;
		ColorHex = colorHex;
		PosicionX = posX;
		PosicionY = posY;
		Ancho = ancho;
		Alto = alto;
		SubAreas = new ObservableCollection<IAreaOfFocusViewModel>();
	}
}