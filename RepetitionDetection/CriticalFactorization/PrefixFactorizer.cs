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
            return 0;
        }

        private readonly MaximalSuffixCalculator maximalSuffixCalculatorForLess;
        private readonly MaximalSuffixCalculator maximalSuffixCalculatorForGreater;
    }
}
