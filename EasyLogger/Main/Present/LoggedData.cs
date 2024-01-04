#nullable enable

using System.Collections.Generic;
using System.Windows.Documents;
using SimpleLogger.Main.Control;

namespace SimpleLogger.Main.Present;

public record LoggedElement(
    string Message,
    LoggingLine LoggingLine);

public class LoggedData
{
    private readonly Dictionary<string, List<LoggedElement>> _data = new();

    public IReadOnlyCollection<string> Keys => _data.Keys;

    public IReadOnlyList<LoggedElement> GetList(string tag)
    {
        return _data.TryGetValue(tag, out var list) ? list : new List<LoggedElement>();
    }

    public void Add(string tag, LoggedElement message)
    {
        if (_data.TryGetValue(tag, out var list))
        {
            list.Add(message);
            var d = _data.Keys;
        }
        else
        {
            _data[tag] = new List<LoggedElement>() { message };
        }
    }
}