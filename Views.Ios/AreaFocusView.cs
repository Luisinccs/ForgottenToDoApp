// 2025-06-12

using CoreGraphics;
using Foundation;
using UIKit;
using ForgottenToDoApp.ViewModels; // Necesario para IAreaOfFocusViewModel
using static ForgottenToDoApp.Views.Ios.UIColorExtensions; // Para la extension de color

namespace ForgottenToDoApp.Views.Ios;

/// <summary>AreaFocusView: Representa visualmente un area de enfoque en el mapa.</summary>
public class AreaFocusView : UIView
{
    #region Variables

    /// <summary>ViewModel asociado a esta vista de area de enfoque.</summary>
    public IAreaOfFocusViewModel ViewModel { get; private set; }

    /// <summary>Etiqueta para mostrar el nombre del area.</summary>
    private UILabel _nameLabel;

    /// <summary>La posicion inicial de la vista cuando comienza un arrastre.</summary>
    private CGPoint _initialCenter;

    /// <summary>Callback que se invoca cuando la posicion de la vista ha cambiado.</summary>
    public Action<IAreaOfFocusViewModel>? PositionUpdatedCallback { get; set; }

    #endregion Variables

    #region Functions

    #region Constructor

    /// <summary>AreaFocusView: Constructor para inicializar con un ViewModel.</summary>
    /// <param name="frame">El marco inicial de la vista.</param>
    /// <param name="viewModel">El ViewModel del area de enfoque.</param>
    public AreaFocusView(CGRect frame, IAreaOfFocusViewModel viewModel) : base(frame)
    {
        ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        InitializeView();
        AddGestureRecognizers(); // Añade los reconocedores de gestos al inicializar
    }

    #endregion Constructor

    #region Internal Functions

    /// <summary>InitializeView: Configura la apariencia y sub-vistas del area de enfoque.</summary>
    private void InitializeView()
    {
        BackgroundColor = ViewModel.ColorHex.FromHexStringValue();
        Layer.CornerRadius = 10;
        Layer.BorderColor = UIColor.Black.CGColor;
        Layer.BorderWidth = 1;
        UserInteractionEnabled = true; // Habilitar interacciones para que detecte gestos

        _nameLabel = new UILabel(new CGRect(5, 5, Frame.Width - 10, 20)) {
            Text = ViewModel.Nombre,
            TextColor = UIColor.Black,
            Font = UIFont.SystemFontOfSize(14, UIFontWeight.Bold),
            TextAlignment = UITextAlignment.Center
        };
        AddSubview(_nameLabel);
    }

    /// <summary>AddGestureRecognizers: Añade los reconocedores de gestos a la vista.</summary>
    private void AddGestureRecognizers() {
        // Gesto de Toque (Tap)
        var tapGesture = new UITapGestureRecognizer();
        tapGesture.AddTarget(() => OnAreaTapped());
        AddGestureRecognizer(tapGesture);

        // Gesto de Arrastre (Pan)
        var panGesture = new UIPanGestureRecognizer();
        panGesture.AddTarget(() => OnAreaDragged(panGesture)); // Pasa el gesto para acceder a su estado
        AddGestureRecognizer(panGesture);
    }

    /// <summary>OnAreaTapped: Maneja el evento de toque en un area de enfoque.</summary>
    private void OnAreaTapped() {
        System.Diagnostics.Debug.WriteLine($"[AreaFocusView] Area '{ViewModel.Nombre}' tocada.");
        // Aquí podrías invocar un comando o evento si ViewModel.SeleccionarAreaDeEnfoqueCommand no es accesible directamente
        // Para esta implementación, asumimos que el MapViewController se encargará de reaccionar al tap
        // si la interacción debe ir mas allá de un simple log.
    }

    /// <summary>OnAreaDragged: Maneja el evento de arrastre en un area de enfoque.</summary>
    /// <param name="gesture">El UIPanGestureRecognizer que desencadeno el evento.</param>
    private void OnAreaDragged(UIPanGestureRecognizer gesture) {
        switch (gesture.State) {
            case UIGestureRecognizerState.Began:
                // Guarda la posicion inicial del centro de la vista cuando el arrastre comienza
                _initialCenter = Center;
                System.Diagnostics.Debug.WriteLine($"[AreaFocusView] Arrastre iniciado para '{ViewModel.Nombre}'. Centro inicial: ({_initialCenter.X}, {_initialCenter.Y})");
                break;

            case UIGestureRecognizerState.Changed:
                // Obtiene la traslacion desde el inicio del arrastre
                var translation = gesture.TranslationInView(Superview); // Superview es _mapCanvasView

                // Calcula la nueva posicion del centro
                Center = new CGPoint(_initialCenter.X + translation.X, _initialCenter.Y + translation.Y);
                break;

            case UIGestureRecognizerState.Ended:
            case UIGestureRecognizerState.Cancelled:
                // Cuando el arrastre termina, actualiza las coordenadas en el ViewModel
                // La posicion se guarda como el origen del Frame (top-left)
                ViewModel.PosicionX = Frame.X;
                ViewModel.PosicionY = Frame.Y;
                System.Diagnostics.Debug.WriteLine($"[AreaFocusView] Arrastre finalizado para '{ViewModel.Nombre}'. Nuevas coordenadas en ViewModel: ({ViewModel.PosicionX}, {ViewModel.PosicionY})");

                // Notifica que la posicion ha cambiado
                PositionUpdatedCallback?.Invoke(ViewModel);
                break;
        }
    }

    #endregion Internal Functions

    #endregion Functions
}