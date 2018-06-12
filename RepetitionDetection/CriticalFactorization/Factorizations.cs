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
            return string.Join("\n", 
                $"Pattern: {Pattern}", 
                $"Prefix length: {PrefixLength}, Prefix critical position: {PrefixCriticalPosition}",
                $"Pattern critical position: {PatternCriticalPosition}");
        }

        public bool PatternFactorizationIsGood() => PatternCriticalPosition <= Pattern.Length / 2;

        public int PrefixLength { get; }

        public int PrefixCriticalPosition { get; }

        public int PatternCriticalPosition { get; }

        [NotNull]
        private string Pattern { get; }
    }
}