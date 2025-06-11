/*
// 2025-06-09
using System.Collections.ObjectModel;
using System.Windows.Input; 

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input; // Ahora usando ICommand de este namespace
// using ForgottenToDo.Core.Models; // Descomentar cuando tengas tus modelos de datos reales

namespace ForgottenToDoApp.ViewModels;

/// <summary>ViewModel para el mapa principal de ForgottenToDoApp.</summary>
public partial class ForgottenToDoMapViewModel : ObservableObject, IForgottenToDoMapViewModel
{
    #region Variables

    /// <summary>Coleccion de las areas de enfoque principales a mostrar en el mapa.</summary>
    private ObservableCollection<IAreaOfFocusViewModel> _areasDeEnfoque;

    #endregion

    #region Propiedades

    /// <summary>Coleccion de las areas de enfoque principales a mostrar en el mapa.</summary>
    public ObservableCollection<IAreaOfFocusViewModel> AreasDeEnfoque
    {
        get => _areasDeEnfoque;
        private set => SetProperty(ref _areasDeEnfoque, value);
    }

    /// <summary>Comando para cuando se selecciona un area de enfoque en el mapa.</summary>
    public ICommand SeleccionarAreaDeEnfoqueCommand { get; }

    /// <summary>Comando para cargar los datos iniciales del mapa.</summary>
    public ICommand CargarMapaCommand { get; }

    #endregion

    #region Constructores

    /// <summary>Constructor del ViewModel del mapa.</summary>
    public ForgottenToDoMapViewModel()
    {
        _areasDeEnfoque = new ObservableCollection<IAreaOfFocusViewModel>();
        SeleccionarAreaDeEnfoqueCommand = new AsyncRelayCommand<IAreaOfFocusViewModel>(OnSeleccionarAreaDeEnfoque);
        CargarMapaCommand = new AsyncRelayCommand(CargarMapaInicial);

        // Cargar datos de ejemplo al inicializar (esto se movera a un servicio real luego)
        CargarMapaInicial().Wait();
    }

    #endregion

    #region Funciones Internas

    /// <summary>Metodo invocado cuando se selecciona un area de enfoque.</summary>
    /// <param name="area">El area de enfoque seleccionada.</param>
    private async Task OnSeleccionarAreaDeEnfoque(IAreaOfFocusViewModel? area)
    {
        if (area == null) return;
		await Task.Delay(50); // Simula una operacion asincrona
        // Logica para navegar a la vista de lista de tareas del area seleccionada
        Console.WriteLine($"Area seleccionada: {area.Nombre}");
        // Aca se inyectara un servicio de navegacion o un EventAggregator
    }

    /// <summary>Metodo para cargar los datos iniciales del mapa.</summary>
    private async Task CargarMapaInicial()
    {
        // Simulamos la carga de datos
        await Task.Delay(50); // Simula una operacion asincrona

        AreasDeEnfoque.Clear();
        AreasDeEnfoque.Add(new AreaOfFocusViewModel { Nombre = "Proyectos", ColorHex = "#FF5733" });
        AreasDeEnfoque.Add(new AreaOfFocusViewModel { Nombre = "Aprendizaje", ColorHex = "#33FF57" });
        AreasDeEnfoque.Add(new AreaOfFocusViewModel { Nombre = "Tareas Personales", ColorHex = "#3357FF" });
    }

    #endregion
}*/