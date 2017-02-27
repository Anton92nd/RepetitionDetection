using System;
using JetBrains.Annotations;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Periods
{
    public static class PeriodExtensions
    {
        public static int GetPeriod([NotNull] this string pattern, int prefixLength)
        {
            return ((TextSubstring) pattern).GetPeriod(prefixLength);
        }

        public static int GetPeriod([NotNull] this TextSubstring pattern, int prefixLength)
        {
            if (pattern.Length == 1)
                return 1;
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
