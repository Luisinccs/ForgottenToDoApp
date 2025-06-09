// 2025-06-09

using System.Collections.ObjectModel;
using System.Windows.Input;
using ForgottenToDoApp.ViewModels;

namespace ForgottenToDoApp.App.Maui.Tests;

/// <summary>Implementacion mock para probar la interfaz IForgottenToDoMapViewModel.</summary>
public class MockForgottenToDoMapViewModel : IForgottenToDoMapViewModel {

	/// <summary>Propiedad mock para AreasDeEnfoque.</summary>
	public ObservableCollection<IAreaOfFocusViewModel> AreasDeEnfoque { get; }

    /// <summary>Comando mock para SeleccionarAreaDeEnfoqueCommand.</summary>
    public ICommand SeleccionarAreaDeEnfoqueCommand { get; }

    /// <summary>Comando mock para CargarMapaCommand.</summary>
    public ICommand CargarMapaCommand { get; }

    /// <summary>Constructor del mock del ViewModel del mapa.</summary>
    public MockForgottenToDoMapViewModel() {

		AreasDeEnfoque = new ObservableCollection<IAreaOfFocusViewModel> {
            new MockAreaOfFocusViewModel { Nombre = "Mock Area 1", ColorHex = "#FF0000" },
            new MockAreaOfFocusViewModel { Nombre = "Mock Area 2", ColorHex = "#00FF00" }
        };

        // Estos comandos no necesitan hacer nada real por ahora, solo estar presentes
        SeleccionarAreaDeEnfoqueCommand = new Command((param) => Console.WriteLine($"Mock Area seleccionada: {((IAreaOfFocusViewModel)param!).Nombre}"));
        CargarMapaCommand = new Command(() => Console.WriteLine("Mock CargarMapaCommand ejecutado."));
    }
}

/// <summary>Implementacion mock para probar la interfaz IAreaOfFocusViewModel.</summary>
public class MockAreaOfFocusViewModel : IAreaOfFocusViewModel {
    /// <summary>Propiedad mock para el nombre del area de enfoque.</summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>Propiedad mock para el color hexadecimal del area de enfoque.</summary>
    public string ColorHex { get; set; } = "#FFFFFF";

    /// <summary>Propiedad mock para las sub-areas de enfoque.</summary>
    public ObservableCollection<IAreaOfFocusViewModel> SubAreas { get; } = new ObservableCollection<IAreaOfFocusViewModel>();
}