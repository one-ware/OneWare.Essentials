using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using OneWare.Essentials.Models;

namespace OneWare.Essentials.Converters;

public class DocumentUiExtensionConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count != 2) return null;
        
        var file = values[0] as IFile;
        var uiExtension = values[1] as DocumentUiExtension;
        
        if(file == null || uiExtension == null) return null;

        if (Activator.CreateInstance(uiExtension.Type) is Control control)
        {
            control.DataContext = uiExtension.CreateDataContext(file);
            return control;
        }
        return null;
    }
}