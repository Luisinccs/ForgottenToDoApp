// 2025-06-08

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace ForgottenToDoApp.ViewModels;

/// <summary>Interfaz para el ViewModel de un area de enfoque individual.<summary>
public interface IAreaOfFocusViewModel {

	/// <summary>Nombre del area de enfoque. <summary>
	string Nombre { get; }

	/// <summary>Color en formato hexadecimal para el area de enfoque (para la visualizacion).<summary>
	string ColorHex { get; }

	/// <summary>Coleccion de sub-areas de enfoque.<summary>
	ObservableCollection<IAreaOfFocusViewModel> SubAreas { get; }

	/// <sum>PosicionX: Coordenada X del area de enfoque en el mapa (agnostica).</sum>
    double PosicionX { get; }

    /// <sum>PosicionY: Coordenada Y del area de enfoque en el mapa (agnostica).</sum>
    double PosicionY { get; }

    /// <sum>Ancho: Ancho del nodo en el mapa (agnostica).</sum>
    double Ancho { get; }

    /// <sum>Alto: Alto del nodo en el mapa (agnostica).</sum>
    double Alto { get; }
	
}