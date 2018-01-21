using System;
using JetBrains.Annotations;

namespace RepetitionDetection.CriticalFactorization
{
    public class Factorizations : IEquatable<Factorizations>
    {
        public Factorizations([NotNull] string pattern, int prefixLength, int prefixCriticalPosition,
            int patternCriticalPosition)
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
            return $"Pattern: {Pattern}\nPrefix length: {PrefixLength}, Prefix critical position: {PrefixCriticalPosition}\n" 
                + $"Pattern critical position: {PatternCriticalPosition}";
        }

        public bool PatternFactorizationIsGood()
        {
            return PatternCriticalPosition <= Pattern.Length / 2;
        }

        public int PrefixLength { get; }

        public int PrefixCriticalPosition { get; }

        public int PatternCriticalPosition { get; }

        [NotNull]
        private string Pattern { get; }
    }
}