using System.Collections.Generic;
using JetBrains.Annotations;

namespace RepetitionDetection.Commons
{
    public class CharGreaterComparer : IComparer<char>
    {
        public int Compare(char x, char y) => comparer.Compare(y, x);

        [NotNull] private readonly IComparer<char> comparer = Comparer<char>.Default;
    }
}