using System.Text;
using RepetitionDetection.Commons;
using RepetitionDetection.CriticalFactorization;
using RepetitionDetection.Periods;

namespace RepetitionDetection.StringMatching
{
    public class CompleteStringMatchingAlgorithm : IStringMatchingAlgorithm
    {
        public CompleteStringMatchingAlgorithm(StringBuilder text, string pattern, int startPosition, PrefixFactorization patternFactorization)
        { 
            factorization = patternFactorization;
            if (factorization.CriticalPosition > pattern.Length / 2)
                throw new InvalidUsageException(string.Format("Invalid usage of Complete string Matching algo:\nTemplate: {0}\nCritical factorization position: {1}", pattern, factorization.CriticalPosition));
            this.text = text;

            this.pattern = pattern;

            patternPeriod = PeriodCalculator.GetPeriod(pattern, patternFactorization.PrefixLength);
            textPosition = startPosition;
            matchedSymbolsCount = 0;
        }

        private int matchedSymbolsCount;

        public bool CheckMatch()
        {
            var result = false;
            while (textPosition + factorization.CriticalPosition + matchedSymbolsCount < text.Length)
            {
                if (pattern[factorization.CriticalPosition + matchedSymbolsCount] == text[textPosition + factorization.CriticalPosition + matchedSymbolsCount] &&
                    pattern[matchedSymbolsCount] == text[textPosition + matchedSymbolsCount])
                {
                    matchedSymbolsCount++;
                    if (factorization.CriticalPosition + matchedSymbolsCount == factorization.PrefixLength)
                    {
                        result = true;
                        matchedSymbolsCount = pattern.Length - (factorization.CriticalPosition + patternPeriod);
                        textPosition = textPosition + patternPeriod;
                    }
                }
                else
                {
                    textPosition = textPosition + matchedSymbolsCount + 1;
                    matchedSymbolsCount = 0;
                }
            }
            return result;
        }

        public void SetState(AlgorithmState state)
        {
            textPosition = state.PositionInText;
            matchedSymbolsCount = state.MatchedSymbolsCount;
        }

        public AlgorithmState State
        {
            get
            {
                return new AlgorithmState(textPosition, matchedSymbolsCount);
            }
        }

        private readonly StringBuilder text;
        private readonly string pattern;
        private readonly int patternPeriod;
        private readonly PrefixFactorization factorization;
        private int textPosition;
    }
}
