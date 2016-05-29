using System;
using JetBrains.Annotations;

namespace RepetitionDetection.CriticalFactorization
{
    public class Factorization : IEquatable<Factorization>
    {
        public Factorization([NotNull] string pattern, int prefixLength, int prefixCriticalPosition, int patternCriticalPosition)
        {
            Pattern = pattern;
            PrefixLength = prefixLength;
            PrefixCriticalPosition = prefixCriticalPosition;
            PatternCriticalPosition = patternCriticalPosition;
        }

        [NotNull]
        public string Pattern { get; private set; }

        public int PrefixLength { get; private set; }
        public int PrefixCriticalPosition { get; private set; }
        public int PatternCriticalPosition { get; private set; }

        public bool Equals([NotNull] Factorization other)
        {
            return Pattern.Equals(other.Pattern) && 
                PrefixLength == other.PrefixLength && PrefixCriticalPosition == other.PrefixCriticalPosition && 
                PatternCriticalPosition == other.PatternCriticalPosition;
        }

        [NotNull]
        public override string ToString()
        {
            return string.Format("Pattern: {0}\nPrefix length: {1}, Prefix critical position: {2}\nPattern critical position: {3}",
                Pattern, PrefixLength, PrefixCriticalPosition, PatternCriticalPosition);
        }
    }
}