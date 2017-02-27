using System;
using JetBrains.Annotations;

namespace RepetitionDetection.CriticalFactorization
{
    public static class FactorizationExtensions
    {
        [NotNull]
        public static Factorizations GetFactorizations([NotNull] this string pattern)
        {
            var prefixFactorizer = new PrefixFactorizer(pattern);
            var previousCriticalPosition = 0;
            var previousPrefixLength = 0;
            var currentCriticalPosition = 0;
            var currentPrefixLength = 0;

            for (var position = 1; position < pattern.Length; ++position)
            {
                prefixFactorizer.Factorize(position + 1);
                if (prefixFactorizer.CriticalPosition > currentCriticalPosition)
                {
                    previousCriticalPosition = currentCriticalPosition;
                    previousPrefixLength = currentPrefixLength;
                }
                currentCriticalPosition = prefixFactorizer.CriticalPosition;
                currentPrefixLength = prefixFactorizer.PrefixLength;
                if (prefixFactorizer.CriticalPosition >= (pattern.Length + 1)/2)
                {
                    break;
                }         
            }
            return new Factorizations(pattern, Math.Max(previousPrefixLength, previousCriticalPosition * 2), previousCriticalPosition, currentCriticalPosition);
        }
    }
}
