using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaEdit;
using AvaloniaEdit.Document;
using AvaloniaEdit.Rendering;
using DynamicData;

namespace OneWare.Essentials.EditorExtensions;

public class InlayHint
{
    public TextLocation Location { get; init; }
    
    //Will be used internally
    public int Offset { get; set; } = -1;
    
    public string Text { get; init; } = string.Empty;
}

public class InlayHintRenderer : VisualLineElementGenerator
{
    private readonly TextEditor _editor;
    private readonly List<InlayHint> _hints = [];
    private readonly List<Control> _hintControls = [];
    
    public InlayHintRenderer(TextEditor editor)
    {
        _editor = editor;
    }

    public void SetInlineHints(IEnumerable<InlayHint> hints)
    {
        _hints.Clear();
        _hintControls.Clear();
        
        _hints.AddRange(hints);

        var foreground = Application.Current!.FindResource(Application.Current!.RequestedThemeVariant, "ThemeForegroundLowBrush") as IBrush;
        var background = Application.Current!.FindResource(Application.Current!.RequestedThemeVariant, "ThemeBackgroundBrush") as IBrush;
        
        foreach (var hint in _hints)
        {
            _hintControls.Add(new Border()
            {
                Margin = new Thickness(1, 0, 5, 0),
                Background = background,
                CornerRadius = new CornerRadius(3),
                VerticalAlignment = VerticalAlignment.Center,
                Child = new TextBlock()
                {
                    Text = hint.Text,
                    Foreground = foreground,
                    Margin = new Thickness(2,0),
                    VerticalAlignment = VerticalAlignment.Center
                }
            });
        }
        
        _editor.TextArea.TextView.Redraw();
    }

    public void ClearInlineHints()
    {
        _hints.Clear();
        _editor.TextArea.TextView.Redraw();
    }

    private static InlayHint? FindNextPosition(List<InlayHint> positions, TextLocation startPosition)
    {
        int left = 0;
        int right = positions.Count - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int comparison = positions[mid].Location.CompareTo(startPosition);

            if (comparison >= 0)
            {
                if (mid == 0 || positions[mid - 1].Location.CompareTo(startPosition) < 0)
                {
                    return positions[mid];
                }
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        return null;
    }
    
    public override int GetFirstInterestedOffset(int startOffset)
    {
        var position = _editor.Document.GetLocation(startOffset);
        var next = FindNextPosition(_hints, position);
        
        if(next == null)
            return -1;
        
        next.Offset = _editor.Document.GetOffset(next.Location);
        return next.Offset;
    }

    public override VisualLineElement? ConstructElement(int offset)
    {
        var index = _hints.FindIndex(hint => hint.Offset == offset);

        if (index < 0) return null;
        
        var control = _hintControls[index];
        return new InlineObjectElement(0, control);
    }
}