using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using OneWare.Essentials.Models;

namespace OneWare.Essentials.Controls;

public class UiExtensionCollection : TemplatedControl
{
    public static readonly StyledProperty<ObservableCollection<UiExtension>?> ExtensionsProperty =
        AvaloniaProperty.Register<UiExtensionCollection, ObservableCollection<UiExtension>?>(nameof(Extensions));
    
    public static readonly StyledProperty<Orientation> OrientationProperty =
        AvaloniaProperty.Register<UiExtensionCollection, Orientation>(nameof(Orientation));

    protected override Type StyleKeyOverride => typeof(UiExtensionCollection);
    
    public ObservableCollection<UiExtension>? Extensions
    {
        get => GetValue(ExtensionsProperty);
        set => SetValue(ExtensionsProperty, value);
    }
    
    public Orientation Orientation
    {
        get => GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }
}