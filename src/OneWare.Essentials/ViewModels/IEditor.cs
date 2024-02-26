using AvaloniaEdit.Document;
using OneWare.Essentials.EditorExtensions;

namespace OneWare.Essentials.ViewModels;

public interface IEditor : IExtendedDocument
{
    public ExtendedTextEditor Editor { get; }
    public TextDocument CurrentDocument { get; }
    public void Select(int offset, int length);
    public event EventHandler? FileSaved;
}