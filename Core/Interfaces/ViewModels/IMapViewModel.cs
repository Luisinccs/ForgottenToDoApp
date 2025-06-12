// 2025-06-12

using System.Collections.ObjectModel;
using System.Windows.Input; 

namespace ForgottenToDoApp.ViewModels;

/// <summary>Interfaz para el ViewModel del mapa principal de ForgottenToDoApp.</summary>
public interface IMapViewModel {

    /// <summary>Titulo del mapa.</summary>
    string Title { get; } // El título podría ser específico del mapa

    /// <summary>Coleccion de las areas de enfoque principales a mostrar en el mapa.</summary>
    ObservableCollection<IAreaOfFocusViewModel> AreasDeEnfoque { get; }

    /// <summary>Comando para cuando se selecciona un area de enfoque en el mapa.</summary>
    ICommand SeleccionarAreaDeEnfoqueCommand { get; }

    /// <summary>Comando para cargar los datos iniciales del mapa.</summary>
    ICommand CargarMapaCommand { get; }

    // Los comandos de navegacion y AddAreaCommand seran movidos a IAppShellViewModel
}