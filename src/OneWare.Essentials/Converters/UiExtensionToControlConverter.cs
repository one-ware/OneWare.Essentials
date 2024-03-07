using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using OneWare.Essentials.Models;

namespace OneWare.Essentials.Converters;

public class UiExtensionToControlConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not UiExtension ext) return null;
        var control = Activator.CreateInstance(ext.Type) as Control;

        if (control != null)
        {
            control.DataContext = ext.DataContext;
        }
            
        return control;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}