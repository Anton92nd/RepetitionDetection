using System;
using RepetitionDetection.CriticalFactorization;

namespace RepetitionDetection.Periods
{
    public static class SmallPeriodCalculator
    {
        public static int GetPeriod(string pattern, PrefixFactorization factorization)
        {
            var good = false;
            if (factorization.CriticalPosition < pattern.Length/2)
            {
                good = true;
                for (var i = 0; i < factorization.CriticalPosition && good; ++i)
                {
                    if (pattern[factorization.CriticalPosition + factorization.SuffixPeriod - 1 - i] !=
                        pattern[factorization.CriticalPosition - 1 - i])
                        good = false;
                }

            }
            return good ? factorization.SuffixPeriod
                : Math.Max(factorization.CriticalPosition, pattern.Length - factorization.CriticalPosition);
        }
    }
}
