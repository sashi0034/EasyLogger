#nullable enable

using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using SimpleLogger.Main.Control;

namespace SimpleLogger.Main.Present;

public readonly record struct LoggedElement(
    LoggingLine LoggingLine)
{
    public string Tag => LoggingLine.tagText.Text;
    public string Message => LoggingLine.tagText.Text;
};

public class LoggingList
{
    private readonly List<LoggedElement> _data = new();

    public IReadOnlyList<LoggedElement> Data => _data;

    public void Add(LoggedElement log)
    {
        _data.Add(log);
    }

    public void RemoveByTag(StackPanel parent, string tag)
    {
        for (int i = _data.Count - 1; i >= 0; --i)
        {
            if (_data[i].LoggingLine.tagText.Text != tag) continue;
            _data.RemoveAt(i);
            parent.Children.RemoveAt(i);
        }
    }

    public void RemoveAll(StackPanel parent)
    {
        for (int i = _data.Count - 1; i >= 0; --i)
        {
            _data.RemoveAt(i);
            parent.Children.RemoveAt(i);
        }
    }
}