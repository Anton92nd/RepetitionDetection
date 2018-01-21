using System.Collections.Generic;
using JetBrains.Annotations;

namespace RepetitionDetection.Commons
{
    public class CharLessComparer : IComparer<char>
    {
        public int Compare(char x, char y)
        {
            return comparer.Compare(x, y);
        }

        [NotNull] private readonly IComparer<char> comparer = Comparer<char>.Default;
    }
}