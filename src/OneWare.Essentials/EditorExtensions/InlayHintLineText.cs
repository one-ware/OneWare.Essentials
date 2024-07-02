using System.Diagnostics;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using AvaloniaEdit.Rendering;

namespace OneWare.Essentials.EditorExtensions;

public class InlayHintLineText : VisualLineText
{
    /// <summary>
    /// Creates a visual line text element with the specified length.
    /// It uses the <see cref="ITextRunConstructionContext.VisualLine"/> and its
    /// <see cref="VisualLineElement.RelativeTextOffset"/> to find the actual text string.
    /// </summary>
    public InlayHintLineText(VisualLine parentVisualLine, int length) : base(parentVisualLine, length)
    {
        
    }

    /// <inheritdoc/>
    public override TextRun CreateTextRun(int startVisualColumn, ITextRunConstructionContext context)
    {
        this.TextRunProperties.SetForegroundBrush(context.TextView.LinkTextForegroundBrush);
        this.TextRunProperties.SetBackgroundBrush(context.TextView.LinkTextBackgroundBrush);
        
        if (context.TextView.LinkTextUnderline)
            this.TextRunProperties.SetTextDecorations(TextDecorations.Underline);
        return base.CreateTextRun(startVisualColumn, context);
    }
}