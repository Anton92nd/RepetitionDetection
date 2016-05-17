using System;

namespace RepetitionDetection.CriticalFactorization
{
    public class Factorization : IEquatable<Factorization>
    {
        public string Pattern { get; private set; }
        public int PrefixLength { get; private set; }
        public int PrefixCriticalPosition { get; private set; }
        public int PatternCriticalPosition { get; private set; }

        public Factorization(string pattern, int prefixLength, int prefixCriticalPosition, int patternCriticalPosition)
        {
            Pattern = pattern;
            PrefixLength = prefixLength;
            PrefixCriticalPosition = prefixCriticalPosition;
            PatternCriticalPosition = patternCriticalPosition;
        }

        public bool Equals(Factorization other)
        {
            return Pattern == other.Pattern && PrefixLength == other.PrefixLength &&
                   PrefixCriticalPosition == other.PrefixCriticalPosition &&
                   PatternCriticalPosition == other.PatternCriticalPosition;
        }

        public override string ToString()
        {
            return string.Format("Pattern: {0}, Prefix length: {1}, Prefix critical position: {2}, Pattern critical position: {3}",
                    Pattern, PrefixLength, PrefixCriticalPosition, PatternCriticalPosition);
        }
    }
}
