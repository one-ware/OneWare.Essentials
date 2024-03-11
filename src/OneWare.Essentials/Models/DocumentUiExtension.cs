namespace OneWare.Essentials.Models;

public class DocumentUiExtension(Type type, Func<IFile, object?> createDataContext)
{
    public Type Type { get; } = type;

    public Func<IFile, object?> CreateDataContext { get; } = createDataContext;
}