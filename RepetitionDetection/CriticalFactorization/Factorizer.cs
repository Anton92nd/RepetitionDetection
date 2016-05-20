namespace RepetitionDetection.CriticalFactorization
{
    public static class Factorizer
    {
        public static Factorization GetFactorization(string pattern)
        {
            var prefixFactorizer = new PrefixFactorizer(pattern);
            var previousCriticalPosition = 0;
            var previousPrefixLength = 0;
            var currentCriticalPosition = 0;
            var currentPrefixLength = 0;

            for (var position = 1; position < pattern.Length; ++position)
            {
                prefixFactorizer.Factorize(position + 1);
                if (prefixFactorizer.CriticalPosition >= currentCriticalPosition)
                {
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
            }
            return new Factorization(pattern, previousPrefixLength, previousCriticalPosition, currentCriticalPosition);
        }
    }
}
