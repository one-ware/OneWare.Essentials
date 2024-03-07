using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls.Primitives;
using OneWare.Essentials.Models;

namespace OneWare.Essentials.Controls;

public class UiExtensionCollection : TemplatedControl
{
    public static readonly StyledProperty<ObservableCollection<UiExtension>?> ExtensionsProperty =
        AvaloniaProperty.Register<UiExtensionCollection, ObservableCollection<UiExtension>?>(nameof(UiExtensionCollection));

    protected override Type StyleKeyOverride => typeof(UiExtensionCollection);
    
    public ObservableCollection<UiExtension>? Extensions
    {
        get => GetValue(ExtensionsProperty);
        set
        {
            if(Extensions != null)
                Extensions.CollectionChanged -= OnExtensionsChanged;
            
            SetValue(ExtensionsProperty, value);
            
            if (value == null)
                return;

            value.CollectionChanged += OnExtensionsChanged;
        } 
    }
    
    private void OnExtensionsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        
    }
}