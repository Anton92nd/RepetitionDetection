using System;
using RepetitionDetection.Commons;
using RepetitionDetection.MaximalSuffixes;

namespace RepetitionDetection.CriticalFactorization
{
    public class PrefixFactorizer
    {
        public PrefixFactorizer(string str)
        {
            maximalSuffixCalculatorForLess = new MaximalSuffixCalculator(str, new CharLessComparer());
            maximalSuffixCalculatorForGreater = new MaximalSuffixCalculator(str, new CharGreaterComparer());
        }

        public PrefixFactorization GetCriticalFactorization(int prefixLength)
        {
            if (prefixLength <= 1)
                throw new InvalidUsageException("Prefix length must be greater than 1");
            maximalSuffixCalculatorForLess.Calculate(prefixLength);
            maximalSuffixCalculatorForGreater.Calculate(prefixLength);

            int criticalPosition, period;
            if (maximalSuffixCalculatorForLess.MaximalSuffixPosition >=
                maximalSuffixCalculatorForGreater.MaximalSuffixPosition)
            {
                criticalPosition = maximalSuffixCalculatorForLess.MaximalSuffixPosition;
                period = maximalSuffixCalculatorForLess.Period;
            }
            else
            {
                criticalPosition = maximalSuffixCalculatorForGreater.MaximalSuffixPosition;
                period = maximalSuffixCalculatorForGreater.Period;
            }
            return new PrefixFactorization(prefixLength, Math.Max(1, criticalPosition), period);
        }


        private readonly MaximalSuffixCalculator maximalSuffixCalculatorForLess;
        private readonly MaximalSuffixCalculator maximalSuffixCalculatorForGreater;
    }
}
