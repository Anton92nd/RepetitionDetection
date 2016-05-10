using System;
using System.Collections.Generic;

namespace RepetitionDetection.Commons
{
    public class CharGreaterComparer : IComparer<char>
    {
        private readonly IComparer<char> comparer = Comparer<Char>.Default;

        public int Compare(char x, char y)
        {
            return comparer.Compare(y, x);
        }
    }
}