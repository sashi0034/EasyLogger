using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleLogger.Main.Control;

public partial class LoggingLine : UserControl
{
    public LoggingLine()
    {
        InitializeComponent();
    }

    public void ChangeTextColor(Brush brush)
    {
        timeText.Foreground = brush;
        tagText.Foreground = brush;
        messageText.Foreground = brush;
    }

    public void ApplyTooltip()
    {
        if (tagText.Text.Length < 12) return;
        tagText.ToolTip = tagText.Text;
    }
}