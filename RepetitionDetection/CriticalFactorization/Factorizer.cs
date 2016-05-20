namespace RepetitionDetection.CriticalFactorization
{
    public static class Factorizer
    {
        public static Factorization GetFactorizations(string pattern)
        {
            var prefixFactorizer = new PrefixFactorizer(pattern);
            var previousFactorization = new PrefixFactorization(0, 0, 1);
            var currentFactorization = new PrefixFactorization(0, 0, 1);

            for (var position = 1; position < pattern.Length; ++position)
            {
                var factorization = prefixFactorizer.GetCriticalFactorization(position + 1);
                if (factorization.CriticalPosition > currentFactorization.CriticalPosition)
                {
                    previousFactorization = currentFactorization;
                    currentFactorization = factorization;
                    if (currentFactorization.CriticalPosition >= (pattern.Length + 1)/2)
                    {
                        break;
                    }
                }
            }
            var patternFactorization = prefixFactorizer.GetCriticalFactorization(pattern.Length);
            currentFactorization = new PrefixFactorization(currentFactorization.PrefixLength,
                currentFactorization.CriticalPosition, patternFactorization.SuffixPeriod);
            return new Factorization(pattern, previousFactorization, currentFactorization);
        }
    }
}
