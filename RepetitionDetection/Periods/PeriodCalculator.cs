using System;

namespace RepetitionDetection.Periods
{
    public static class PeriodCalculator
    {
        public static int GetPeriod(string pattern, int prefixLength)
        {
            var prefixFunction = new int[prefixLength];
            prefixFunction[0] = 0;
            var result = 0;
            for (int i = 1; i < prefixLength; ++i)
            {
                var j = prefixFunction[i - 1];
                while (j > 0  && pattern[i] != pattern[j])
                {
                    j = prefixFunction[j - 1];
                }
                if (pattern[i] == pattern[j])
                    ++j;
                prefixFunction[i] = j;
                result = Math.Max(result, i - prefixFunction[i] + 1);
            }
            return result;
        }
    }
}
