#nullable enable

using System.Windows.Controls;
using SimpleLogger.Main.Control;

namespace SimpleLogger.Main.Process;

public record LogAddition(
    StackPanel StackPanel)
{
    public void Add(string text)
    {
        if (text == "") return;
        int cursor = 0;
        var tag = getTag(text, ref cursor);
        var message = text.Substring(cursor, text.Length - cursor);

        var logging = new LoggingLine
        {
            tagText =
            {
                Text = tag
            },
            messageText =
            {
                Text = message
            }
        };

        StackPanel.Children.Add(logging);
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
}