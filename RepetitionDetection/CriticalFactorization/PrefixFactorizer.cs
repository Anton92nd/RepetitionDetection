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

        public int GetCriticalFactorizationPosition(int prefixLength)
        {
            maximalSuffixCalculatorForLess.Calculate(prefixLength);
            maximalSuffixCalculatorForGreater.Calculate(prefixLength);
            return Math.Max(maximalSuffixCalculatorForLess.MaximalSuffixPosition,
                maximalSuffixCalculatorForGreater.MaximalSuffixPosition);
        }

        private readonly MaximalSuffixCalculator maximalSuffixCalculatorForLess;
        private readonly MaximalSuffixCalculator maximalSuffixCalculatorForGreater;
    }
}
