namespace OneWare.Essentials.Models;

public class UiExtension(Type type, object? dataContext = null)
{
    public Type Type { get; } = type;

    public object? DataContext { get; } = dataContext;
}