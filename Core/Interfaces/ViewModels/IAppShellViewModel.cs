// 2025-06-12

using System.Windows.Input;

namespace ForgottenToDoApp.ViewModels;

/// <summary>Interfaz para el ViewModel de la shell o navegacion principal de la aplicacion.</summary>
public interface IAppShellViewModel {

    /// <summary>Comando para añadir una nueva area (si es un boton de shell global).</summary>
    ICommand AddAreaCommand { get; }

    /// <summary>Comando para navegar a la vista de listas.</summary>
    ICommand NavigateToListsCommand { get; }

    /// <summary>Comando para navegar a la vista de calendario.</summary>
    ICommand NavigateToCalendarCommand { get; }

    /// <summary>Comando para navegar a la vista de habitos.</summary>
    ICommand NavigateToHabitsCommand { get; }

    /// <summary>Comando para navegar a la vista de configuracion.</summary>
    ICommand NavigateToSettingsCommand { get; }
}