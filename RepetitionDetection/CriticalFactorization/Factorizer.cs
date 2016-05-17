namespace RepetitionDetection.CriticalFactorization
{
    public static class Factorizer
    {
        public static Factorization GetFactorization(string pattern)
        {
            var prefixFactorizer = new PrefixFactorizer(pattern);
            int currentPosition = -1, lastPosition = -1, prefixLength = -1;
            for (var position = 0; position < pattern.Length; ++position)
            {
                prefixLength = position;
                lastPosition = currentPosition;
                currentPosition = prefixFactorizer.GetCriticalFactorizationPosition(position + 1);
                if (currentPosition > (pattern.Length + 1) / 2)
                {
                    break;
                }
            }
            return new Factorization(pattern, prefixLength, lastPosition, currentPosition);
        }
    }
}
