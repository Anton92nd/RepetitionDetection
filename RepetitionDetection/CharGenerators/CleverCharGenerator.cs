using System;
using System.Text;
using RepetitionDetection.Commons;

namespace RepetitionDetection.CharGenerators
{
    public class CleverCharGenerator : ICharGenerator
    {
        public CleverCharGenerator(StringBuilder text, int alphabetSize, RationalNumber E, bool detectEqual)
        {
            this.text = text;
            AlphabetSize = alphabetSize;
            used = new bool[alphabetSize];
            LastSymbols = BinarySearch(E, alphabetSize, detectEqual);
        }

        public char Generate()
        {
            for (var i = 0; i < AlphabetSize; ++i)
                used[i] = false;
            var takeLast = Math.Min(text.Length, LastSymbols);
            for (var i = 0; i < takeLast; ++i)
                used[text[text.Length - 1 - i] - 'a'] = true;
            var rand = RandomNumberGenerator.Generate(1, AlphabetSize - takeLast + 1);
            for (var i = 0; i < AlphabetSize; ++i)
            {
                if (!used[i])
                    rand--;
                if (rand == 0)
                    return (char) (i + 'a');
            }
            throw new InvalidProgramStateException("Invalid program state in CleverCharGenerator");
        }

        public int AlphabetSize { get; }

        private readonly StringBuilder text;
        private readonly bool[] used;

        private static int BinarySearch(RationalNumber E, int alphabetSize, bool detectEqual)
        {
            int l = 0, r = alphabetSize;
            if (DetectsRepetition(detectEqual, new RationalNumber(r + 1, r), E))
                throw new InvalidProgramStateException("Inconsistent exponent and alphabet values");
            while (l + 1 < r)
            {
                var m = (r + l) / 2;
                if (DetectsRepetition(detectEqual, new RationalNumber(m + 1, m), E))
                    l = m;
                else
                    r = m;
            }
            return l;
        }

        private static bool DetectsRepetition(bool detectEqual, RationalNumber cur, RationalNumber E)
        {
            return detectEqual ? cur >= E : cur > E;
        }

        public override string ToString()
        {
            return "CleverCharGenerator";
        }

        public int LastSymbols { get; }
    }
}