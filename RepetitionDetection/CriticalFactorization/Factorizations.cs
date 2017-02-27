using System;
using JetBrains.Annotations;
using RepetitionDetection.Commons;

namespace RepetitionDetection.CriticalFactorization
{
    public class Factorizations : IEquatable<Factorizations>
    {
        public Factorizations([NotNull] TextSubstring pattern, int prefixLength, int prefixCriticalPosition, int patternCriticalPosition)
        {
            Pattern = pattern;
            PrefixLength = prefixLength;
            PrefixCriticalPosition = prefixCriticalPosition;
            PatternCriticalPosition = patternCriticalPosition;
        }

        public bool Equals([CanBeNull] Factorizations other)
        {
            if (other == null)
                return false;
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

        public bool PatternFactorizationIsGood()
        {
            return PatternCriticalPosition <= Pattern.Length/2;
        }

        public int PrefixLength { get; private set; }

        public int PrefixCriticalPosition { get; private set; }

        public int PatternCriticalPosition { get; private set; }

        [NotNull]
        private TextSubstring Pattern { get; set; }
    }
}