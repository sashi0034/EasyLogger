#nullable enable

using System;
using System.Collections.Generic;

namespace SimpleLogger.Main.Data;

public class FilteringModel
{
    public bool IsAllFiltered { get; set; } = true;
    private readonly Dictionary<string, bool> _tagTable = new();

    public IReadOnlyCollection<string> Tags => _tagTable.Keys;

    public bool IsFiltered(string tag)
    {
        if (_tagTable.TryGetValue(tag, out var enable)) return enable;
        return false;
    }

    public void SetFiltered(string tag, bool enabled)
    {
        _tagTable[tag] = enabled;
    }
}