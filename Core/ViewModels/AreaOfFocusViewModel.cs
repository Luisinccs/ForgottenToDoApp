// 2025-06-09
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ForgottenToDoApp.ViewModels;

/// <summary>ViewModel para un area de enfoque individual.</summary>
public partial class AreaOfFocusViewModel : ObservableObject, IAreaOfFocusViewModel
{
    #region Variables

    /// <summary>Nombre del area de enfoque.</summary>
    private string _nombre = string.Empty;

    /// <summary>Color en formato hexadecimal para el area de enfoque (para la visualizacion).</summary>
    private string _colorHex = "#FFFFFF"; // Default blanco

    /// <summary>Coleccion de sub-areas de enfoque.</summary>
    private ObservableCollection<IAreaOfFocusViewModel> _subAreas;

    #endregion

    #region Propiedades

    /// <summary>Nombre del area de enfoque.</summary>
    public string Nombre
    {
        get => _nombre;
        set => SetProperty(ref _nombre, value);
    }

    /// <summary>Color en formato hexadecimal para el area de enfoque (para la visualizacion).</summary>
    public string ColorHex
    {
        get => _colorHex;
        set => SetProperty(ref _colorHex, value);
    }

    /// <summary>Coleccion de sub-areas de enfoque.</summary>
    public ObservableCollection<IAreaOfFocusViewModel> SubAreas
    {
        get => _subAreas;
        private set => SetProperty(ref _subAreas, value);
    }

    #endregion

    #region Constructores

    /// <summary>Constructor del ViewModel de un area de enfoque.</summary>
    public AreaOfFocusViewModel()
    {
        _subAreas = new ObservableCollection<IAreaOfFocusViewModel>();
    }

    #endregion
}