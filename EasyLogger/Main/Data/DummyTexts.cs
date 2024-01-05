#nullable enable

using System.Collections.Generic;

namespace SimpleLogger.Main.Data;

public static class DummyTexts
{
    private static readonly List<string> _data = new()
    {
        "This is virus.",
        "#W Do not erase me.",
        "#Clear Info",
        "Start happy.",
        "#I The quick brown fox jumps over the lazy dog.",
        "#I She sells seashells by the seashore.",
        "#I How can a clam cram in a clean cream can?",
        "#I Fuzzy fuzzy was a bear, fuzzy fuzzy had no hair.",
        "#I I saw Susie sitting in a shoeshine shop.",
        "#E Two tiny tigers take two taxis to town.",
        "#I A proper copper coffee pot.",
        "#Game Lesser leather never weathered wetter weather better.",
        "#I A swan swam over the sea, swim swan swim!",
        "#Warn I wish to wash my Irish wristwatch.",
        "#W A big black bug bit a big black bear.",
        "#Tick",
        "#Info How much wood would a woodchuck chuck if a woodchuck could chuck wood?\nif a woodchuck could chuck wood?",
        "Peter Piper picked a peck of pickled peppers.",
        "#E If a dog chews shoes, whose shoes does he choose?",
        "#I Six slippery snails slid silently down the slope.",
        "#Error Betty Botter bought some butter, but the butter was bitter.",
        "The great Greek grape growers grow great Greek grapes.",
        "#I I need not your needles; they're needless to me.",
        "#Game Round the rugged rocks the ragged rascal ran.",
        "#I We surely shall see the sun shine soon.",
        "#Tag-Only",
        "#Actor-Last-Update",
    };

    public static IReadOnlyList<string> Data => _data;
}