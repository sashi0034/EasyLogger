#nullable enable

using System.Collections.Generic;
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
}