// En ForgottenToDoApp.Core/ViewModels/IUniversoViewModel.cs
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input; // Para ICommand

namespace ForgottenToDoApp.ViewModels;

public interface IUniversoViewModel {

	string Title { get; }
    ICommand AddAreaCommand { get; }
    ICommand NavigateToListsCommand { get; }
    ICommand NavigateToCalendarCommand { get; }
    ICommand NavigateToHabitsCommand { get; }
    ICommand NavigateToSettingsCommand { get; }

    // Para la representación dinámica del mapa:
    // ObservableCollection<AreaDeEnfoqueViewModel> AreasDeEnfoque { get; }
    // ObservableCollection<ConexionViewModel> Conexiones { get; }
    // Donde AreaDeEnfoqueViewModel y ConexionViewModel serían ViewModels simplificados
    // con propiedades como Nombre, ColorHex, PosicionX, PosicionY, etc.
}