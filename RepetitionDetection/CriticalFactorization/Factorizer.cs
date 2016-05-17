namespace RepetitionDetection.CriticalFactorization
{
    public static class Factorizer
    {
        private class PrefixFactorization
        {
            public int PrefixLength { get; set; }
            public int CriticalPosition { get; set; }

            public override string ToString()
            {
                return string.Format("Prefix length: {0}, critical position: {1}", PrefixLength, CriticalPosition);
            }
        }

        public static Factorization GetFactorization(string pattern)
        {
            var prefixFactorizer = new PrefixFactorizer(pattern);
            var previousFactorization = new PrefixFactorization {PrefixLength = 0};
            var currentFactorization = new PrefixFactorization {PrefixLength = 0};

            for (var position = 1; position < pattern.Length; ++position)
            {
                var factorizationPosition = prefixFactorizer.GetCriticalFactorizationPosition(position + 1);
                if (factorizationPosition > currentFactorization.CriticalPosition)
                {
                    previousFactorization = currentFactorization;
                    currentFactorization = new PrefixFactorization
                    {
                        PrefixLength = position + 1,
                        CriticalPosition = factorizationPosition
                    };
                    if (currentFactorization.CriticalPosition >= (pattern.Length + 1)/2)
                    {
                        break;
                    }
                }
            }
            return new Factorization(pattern, previousFactorization.PrefixLength, previousFactorization.CriticalPosition, currentFactorization.CriticalPosition);
        }
    }
}
