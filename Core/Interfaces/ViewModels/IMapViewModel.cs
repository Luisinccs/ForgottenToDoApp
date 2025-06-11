// 2025-06-11

using System.Collections.ObjectModel;
using System.Windows.Input; // Para ICommand

namespace ForgottenToDoApp.ViewModels;

/// <sum>Interfaz para el ViewModel del mapa principal de ForgottenToDoApp.</sum>
public interface IMapViewModel {
	/// <sum>Title: Titulo del mapa.</sum>
	string Title { get; }

	/// <sum>Coleccion de las areas de enfoque principales a mostrar en el mapa.</sum>
	ObservableCollection<IAreaOfFocusViewModel> AreasDeEnfoque { get; }

	/// <sum>Comando para cuando se selecciona un area de enfoque en el mapa.</sum>
	ICommand SeleccionarAreaDeEnfoqueCommand { get; }

	/// <sum>Comando para cargar los datos iniciales del mapa.</sum>
	ICommand CargarMapaCommand { get; }

	/// <sum>AddAreaCommand: Comando para añadir una nueva area.</sum>
	ICommand AddAreaCommand { get; }

	/// <sum>NavigateToListsCommand: Comando para navegar a la vista de listas.</sum>
	ICommand NavigateToListsCommand { get; }

	/// <sum>NavigateToCalendarCommand: Comando para navegar a la vista de calendario.</sum>
	ICommand NavigateToCalendarCommand { get; }

	/// <sum>NavigateToHabitsCommand: Comando para navegar a la vista de habitos.</sum>
	ICommand NavigateToHabitsCommand { get; }

	/// <sum>NavigateToSettingsCommand: Comando para navegar a la vista de configuracion.</sum>
	ICommand NavigateToSettingsCommand { get; }
}