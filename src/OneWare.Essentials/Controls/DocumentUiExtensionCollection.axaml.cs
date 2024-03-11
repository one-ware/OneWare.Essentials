using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using OneWare.Essentials.Models;

namespace OneWare.Essentials.Controls;

public class DocumentUiExtensionCollection : TemplatedControl
{
    public static readonly StyledProperty<IFile?> FileProperty =
        AvaloniaProperty.Register<DocumentUiExtensionCollection, IFile?>(nameof(File));
    
    public static readonly StyledProperty<ObservableCollection<DocumentUiExtension>?> ExtensionsProperty =
        AvaloniaProperty.Register<DocumentUiExtensionCollection, ObservableCollection<DocumentUiExtension>?>(nameof(Extensions));
    
    public static readonly StyledProperty<Orientation> OrientationProperty =
        AvaloniaProperty.Register<DocumentUiExtensionCollection, Orientation>(nameof(Orientation));

    protected override Type StyleKeyOverride => typeof(DocumentUiExtensionCollection);
    
    public IFile? File
    {
        get => GetValue(FileProperty);
        set => SetValue(FileProperty, value);
    }
    
    public ObservableCollection<DocumentUiExtension>? Extensions
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