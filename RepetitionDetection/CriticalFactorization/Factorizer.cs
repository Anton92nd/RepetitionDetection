namespace RepetitionDetection.CriticalFactorization
{
    public static class Factorizer
    {
        public static Factorization GetFactorizations(string pattern)
        {
            var prefixFactorizer = new PrefixFactorizer(pattern);
            var previousFactorization = new PrefixFactorization(0, 0);
            var currentFactorization = new PrefixFactorization(0, 0);

            for (var position = 1; position < pattern.Length; ++position)
            {
                var factorization = prefixFactorizer.GetCriticalFactorization(position + 1);
                if (factorization.CriticalPosition >= currentFactorization.CriticalPosition)
                {
                    if (factorization.CriticalPosition > currentFactorization.CriticalPosition)
                        previousFactorization = currentFactorization;
                    currentFactorization = factorization;
                    if (currentFactorization.CriticalPosition >= (pattern.Length + 1)/2)
                    {
                        break;
                    }
                }
                else
                {
                    if (factorization.CriticalPosition == previousFactorization.CriticalPosition)
                    {
                        previousFactorization = factorization;
                    }
                }
            }
            return new Factorization(pattern, previousFactorization, currentFactorization);
        }
    }
}
