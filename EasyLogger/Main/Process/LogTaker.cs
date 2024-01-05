#nullable enable

using System.Windows.Controls;
using System.Windows.Media;
using SimpleLogger.Main.Control;
using SimpleLogger.Main.Data;
using SimpleLogger.Main.Present;

namespace SimpleLogger.Main.Process;

public record LogTaker(
    LoggingList LoggingList,
    StackPanel StackPanel,
    FilteringProcess FilteringProcess)
{
    private ulong _tickedFrame = 0;

    public void Take(string text)
    {
        if (text == "") return;
        int cursor = 0;
        var tag = getTag(text, ref cursor);
        var message = text.Substring(cursor, text.Length - cursor);
        if (checkFunctionTag(tag, message)) return;

        var logging = new LoggingLine
        {
            timeText =
            {
                Text = _tickedFrame.ToString(),
            },
            tagText =
            {
                Text = tag,
            },
            messageText =
            {
                Text = message
            }
        };
        logging.ApplyTooltip();

        FilteringProcess.CheckAddFilter(tag);
        FilteringProcess.AppleElement(new LoggedElement(logging));

        checkChangeColor(logging, tag);

        StackPanel.Children.Add(logging);
        LoggingList.Add(new LoggedElement(logging));
    }

    private static string getTag(string text, ref int cursor)
    {
        if (text[0] != '#') return ReservedTags.Info;

        int spaceIndex = text.IndexOf(' ');
        cursor = spaceIndex + 1;
        switch (spaceIndex)
        {
        case -1:
            cursor = text.Length;
            if (text.Length == 1) return ReservedTags.Unknown;
            return text.Substring(1, text.Length - 1);
        case 1:
            return ReservedTags.Unknown;
        case 2:
            switch (text[1])
            {
            case 'I':
                return ReservedTags.Info;
            case 'W':
                return ReservedTags.Warn;
            case 'E':
                return ReservedTags.Error;
            default:
                return text[1].ToString();
            }

        default:
            return text.Substring(1, spaceIndex - 1);
        }
    }

    private bool checkFunctionTag(string tag, string message)
    {
        switch (tag)
        {
        case ReservedTags.Tick:
            _tickedFrame++;
            return true;
        case ReservedTags.Clear:
            if (message == "") LoggingList.RemoveAll(StackPanel);
            else LoggingList.RemoveByTag(StackPanel, message);
            return true;
        default:
            return false;
        }
    }

    private void checkChangeColor(LoggingLine logging, string tag)
    {
        switch (tag)
        {
        case ReservedTags.Info:
            logging.ChangeTextColor(Brushes.LightCyan);
            return;
        case ReservedTags.Warn:
            logging.ChangeTextColor(Brushes.Gold);
            return;
        case ReservedTags.Error:
            logging.ChangeTextColor(Brushes.OrangeRed);
            return;
        default:
            return;
        }
    }
}