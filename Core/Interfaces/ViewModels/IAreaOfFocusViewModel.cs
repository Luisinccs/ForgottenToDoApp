// 2025-06-12

using System.Collections.ObjectModel;
// using CommunityToolkit.Mvvm.Input; // No es estrictamente necesario si no tiene comandos propios

namespace ForgottenToDoApp.ViewModels;

/// <summary>Interfaz para el ViewModel de un area de enfoque individual.</summary>
public interface IAreaOfFocusViewModel {

    /// <summary>Nombre del area de enfoque. </summary>
    string Nombre { get; }

    /// <summary>Color en formato hexadecimal para el area de enfoque (para la visualizacion).</summary>
    string ColorHex { get; }

    /// <summary>Coordenada X de la posicion del area de enfoque en el mapa.</summary>
    double PosicionX { get; set; } 

    /// <summary>Coordenada Y de la posicion del area de enfoque en el mapa.</summary>
    double PosicionY { get; set; } 

    /// <summary>Ancho del area de enfoque en el mapa.</summary>
    double Ancho { get; } // Asumimos que es de solo lectura para la vista, o se actualiza de otra forma

    /// <summary>Alto del area de enfoque en el mapa.</summary>
    double Alto { get; } // Asumimos que es de solo lectura para la vista, o se actualiza de otra forma

    /// <summary>Coleccion de sub-areas de enfoque.</summary>
    ObservableCollection<IAreaOfFocusViewModel> SubAreas { get; }

}