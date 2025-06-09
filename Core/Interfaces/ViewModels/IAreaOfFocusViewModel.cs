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
	
}