
// 2025-06-12

using CoreAnimation;
using CoreGraphics; // Para CGPoint, CGRect
using Foundation;
using System;
using UIKit;
using System.Collections.ObjectModel;
using System.Linq;
using ForgottenToDoApp.ViewModels;
using ForgottenToDoApp.Views.CommonTestHelpers;
using static ForgottenToDoApp.Views.Ios.UIColorExtensions;
using System.Collections.Generic; // Para Dictionary

namespace ForgottenToDoApp.Views.Ios;

/// <summary>MapViewController: Vista del lienzo del mapa en ForgottenToDoApp.</summary>
public class MapViewController : UIViewController {

    #region Variables

    /// <summary>ViewModel del mapa que proporciona los datos y la logica.</summary>
    private readonly IMapViewModel _viewModel;

    /// <summary>UIScrollView que permite el desplazamiento horizontal y vertical del mapa.</summary>
    private readonly UIScrollView _scrollView = new();

    /// <summary>UIView que actua como el lienzo principal donde se dibujaran las areas de enfoque.</summary>
    private readonly UIView _mapCanvasView = new();

    /// <summary>UIView que representa la figura "Mi Universo" en el centro del mapa.</summary>
    private UIView _myUniverseView = new();

    /// <summary>Diccionario para mantener un seguimiento de las capas de forma de las lineas de conexion.</summary>
    private readonly Dictionary<IAreaOfFocusViewModel, CAShapeLayer> _areaLineLayers = new();

    #endregion Variables

    #region Functions

    #region Constructor

    /// <summary>MapViewController: Constructor de la vista del mapa.</summary>
    /// <param name="viewModel">El IMapViewModel asociado a esta vista.</param>
    public MapViewController(IMapViewModel viewModel) : base((string?)null, (NSBundle?)null) {
        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    #endregion Constructor

    #region Internal Functions

    /// <summary>InitializeUserInterface: Configura los elementos principales de la interfaz de usuario.</summary>
    private void InitializeUserInterface() {

        ArgumentNullException.ThrowIfNull(View);

        View.BackgroundColor = UIColor.SystemBackground;
        // Configurar el UIScrollView para permitir el desplazamiento
        _scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
        _scrollView.MinimumZoomScale = 0.5f;
        _scrollView.MaximumZoomScale = 2.0f;
        _scrollView.ZoomScale = 1.0f;
        _scrollView.BackgroundColor = UIColor.FromRGB(38, 47, 60);
        View.AddSubview(_scrollView);

        // Configurar las restricciones del UIScrollView para que ocupe toda la vista
        NSLayoutConstraint.ActivateConstraints(new[]{
            _scrollView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor),
            _scrollView.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor),
            _scrollView.LeadingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeadingAnchor),
            _scrollView.TrailingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TrailingAnchor)
        });

        // Configurar el UIView que sera el lienzo del mapa (similar al AbsoluteLayout de MAUI)
        _mapCanvasView.Frame = new CGRect(0, 0, 2000, 2000); // Tamaño fijo grande para el lienzo del mapa
        _mapCanvasView.BackgroundColor = UIColor.LightGray;
        _scrollView.AddSubview(_mapCanvasView);

        // Establecer el ContentSize del UIScrollView para que se pueda desplazar el lienzo completo
        _scrollView.ContentSize = _mapCanvasView.Frame.Size;

        // <<<<<<<<<<<<<<< NUEVO CÓDIGO: FIGURA "MI UNIVERSO" >>>>>>>>>>>>>>>>>
        // Definir el tamaño y posicion de "Mi Universo" en el centro del lienzo
        nfloat universeSize = 120; // Tamaño de la figura
        CGPoint universeCenter = new CGPoint(_mapCanvasView.Frame.Width / 2, _mapCanvasView.Frame.Height / 2);
        _myUniverseView = new UIView(new CGRect(
            universeCenter.X - universeSize / 2,
            universeCenter.Y - universeSize / 2,
            universeSize,
            universeSize
        )) {
            BackgroundColor = UIColor.SystemBlue, // Un color distintivo
            Layer = {
                CornerRadius = universeSize / 2, // Para hacerlo circular
                BorderColor = UIColor.White.CGColor,
                BorderWidth = 2
            },
            UserInteractionEnabled = false // No se puede mover
        };

        var universeLabel = new UILabel(new CGRect(0, (universeSize / 2) - 10, universeSize, 20)) {
            Text = "Mi Universo",
            TextColor = UIColor.White,
            Font = UIFont.SystemFontOfSize(16, UIFontWeight.Bold),
            TextAlignment = UITextAlignment.Center
        };
        _myUniverseView.AddSubview(universeLabel);
        _mapCanvasView.AddSubview(_myUniverseView);
        // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    }

    /// <summary>LoadMapData: Carga y posiciona las areas de enfoque en el lienzo del mapa.</summary>
    private void LoadMapData() {
        if (_viewModel.AreasDeEnfoque == null) return;

        // Limpia cualquier vista existente antes de cargar (útil si se llama varias veces)
        foreach (var subview in _mapCanvasView.Subviews.Where(v => v is AreaFocusView)) {
            subview.RemoveFromSuperview();
        }
        // Limpia las lineas existentes
        foreach (var layer in _areaLineLayers.Values) {
            layer.RemoveFromSuperLayer();
        }
        _areaLineLayers.Clear();


        foreach (var area in _viewModel.AreasDeEnfoque) {
            // Crea una instancia de AreaFocusView en lugar de un UIView basico
            var areaView = new AreaFocusView(
                new CGRect(area.PosicionX, area.PosicionY, area.Ancho, area.Alto),
                area // Pasa el ViewModel del area a la AreaFocusView
            );

            // Suscribe al callback de posicion actualizada para redibujar la linea
            areaView.PositionUpdatedCallback = (updatedArea) => OnAreaFocusViewPositionUpdated(updatedArea);

            _mapCanvasView.AddSubview(areaView);

            System.Diagnostics.Debug.WriteLine($"[Catalyst Map] Added '{area.Nombre}' at ({area.PosicionX}, {area.PosicionY}) with size {area.Ancho}x{area.Alto}");
        }

        // Dibuja las lineas de conexion iniciales
        DrawConnectingLines();

        // Llamar al comando de carga del ViewModel (si tiene logica adicional)
        _viewModel.CargarMapaCommand?.Execute(null);
    }

    /// <summary>OnAreaFocusViewPositionUpdated: Maneja la notificacion de que un AreaFocusView ha cambiado de posicion.</summary>
    /// <param name="area">El IAreaOfFocusViewModel del area que ha cambiado.</param>
    private void OnAreaFocusViewPositionUpdated(IAreaOfFocusViewModel area) {
        System.Diagnostics.Debug.WriteLine($"[MapViewController] Area '{area.Nombre}' notificada con nueva posicion. Actualizando linea.");
        UpdateLineForArea(area);
    }

    /// <summary>DrawConnectingLines: Dibuja las lineas de conexion entre las areas de enfoque y "Mi Universo".</summary>
    private void DrawConnectingLines() {
        // Limpia las lineas existentes antes de redibujar todas (para la carga inicial)
        foreach (var layer in _areaLineLayers.Values) {
            layer.RemoveFromSuperLayer();
        }
        _areaLineLayers.Clear();

        foreach (var area in _viewModel.AreasDeEnfoque) {
            UpdateLineForArea(area); // Llama a la funcion que crea/actualiza la linea para cada area
        }
    }

    /// <summary>UpdateLineForArea: Crea o actualiza la linea de conexion para un area especifica.</summary>
    /// <param name="area">El IAreaOfFocusViewModel para el cual actualizar la linea.</param>
    private void UpdateLineForArea(IAreaOfFocusViewModel area) {
        // Calcula el centro del area de enfoque
        CGPoint areaCenter = new CGPoint(area.PosicionX + (area.Ancho / 2), area.PosicionY + (area.Alto / 2));

        // Calcula el centro de la figura "Mi Universo"
        CGPoint universeCenter = _myUniverseView.Center;

        // Crea o actualiza el path de la linea
        var path = new UIBezierPath();
        path.MoveTo(areaCenter);
        path.AddLineTo(universeCenter);

        CAShapeLayer lineLayer;
        if (_areaLineLayers.TryGetValue(area, out lineLayer)) {
            // Si la capa ya existe, simplemente actualiza su path
            lineLayer.Path = path.CGPath;
        } else {
            // Si la capa no existe, crea una nueva
            lineLayer = new CAShapeLayer {
                Path = path.CGPath,
                StrokeColor = UIColor.DarkGray.CGColor, // Color de la linea
                LineWidth = 2, // Ancho de la linea
                FillColor = UIColor.Clear.CGColor // No rellenar
            };
            _mapCanvasView.Layer.AddSublayer(lineLayer);
            _areaLineLayers.Add(area, lineLayer);
        }
    }

    #endregion Internal Functions

    #region Overrides

    /// <summary>ViewDidLoad: Se llama despues de que la vista del controlador es cargada en la memoria.</summary>
    public override void ViewDidLoad() {
        base.ViewDidLoad();
        InitializeUserInterface();
        LoadMapData();
    }

    /// <summary>ViewDidLayoutSubviews: Se llama cuando el sistema de layout ha actualizado las vistas de los subviews del controlador.</summary>
    public override void ViewDidLayoutSubviews() {
        base.ViewDidLayoutSubviews();
        // Cuando las vistas se reajustan (ej. rotacion), es buena idea redibujar las lineas
        // Esto es mas importante si el _mapCanvasView cambia de tamaño o si la relacion de aspecto del scrollview se adapta.
        // Por ahora, como el _mapCanvasView tiene un tamaño fijo, no es estrictamente necesario,
        // pero es una buena practica para dinamismo.
        // Podrias llamar a DrawConnectingLines(); aqui si los tamaños del _mapCanvasView fueran dinamicos.
    }

    #endregion Overrides

    #endregion Functions
}