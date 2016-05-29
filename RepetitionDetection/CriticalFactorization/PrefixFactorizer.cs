using System;
using JetBrains.Annotations;
using RepetitionDetection.Commons;
using RepetitionDetection.MaximalSuffixes;

namespace RepetitionDetection.CriticalFactorization
{
    public class PrefixFactorizer
    {
        public PrefixFactorizer([NotNull] string str)
        {
            maximalSuffixCalculatorForLess = new MaximalSuffixCalculator(str, new CharLessComparer());
            maximalSuffixCalculatorForGreater = new MaximalSuffixCalculator(str, new CharGreaterComparer());
        }

        public void Factorize(int prefixLength)
        {
            if (prefixLength <= 1)
                throw new InvalidUsageException("Prefix length must be greater than 1");
            PrefixLength = prefixLength;
            maximalSuffixCalculatorForLess.Calculate(prefixLength);
            maximalSuffixCalculatorForGreater.Calculate(prefixLength);
        }

        public int CriticalPosition
        {
            get
            {
                return Math.Max(1,Math.Max(maximalSuffixCalculatorForLess.MaximalSuffixPosition, maximalSuffixCalculatorForGreater.MaximalSuffixPosition));
            }
        }

        public int PrefixLength { get; private set; }

        [NotNull]
        private readonly MaximalSuffixCalculator maximalSuffixCalculatorForLess;

        [NotNull]
        private readonly MaximalSuffixCalculator maximalSuffixCalculatorForGreater;
    }
}
