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
}