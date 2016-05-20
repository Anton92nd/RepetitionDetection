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
            criticalFactorizationPosition = patternFactorization.CriticalPosition;
            if (criticalFactorizationPosition > pattern.Length / 2)
                throw new InvalidUsageException(string.Format("Invalid usage of Complete string Matching algo:\nTemplate: {0}\nCritical factorization position: {1}", pattern, criticalFactorizationPosition));
            this.text = text;
            this.pattern = pattern;
            this.patternPeriod = SmallPeriodCalculator.GetPeriod(pattern, patternFactorization);
            this.position = startPosition;
            matchedSymbolsCount = 0;
        }

        private int matchedSymbolsCount;

        public bool CheckMatch()
        {
            var result = false;
            while (position + criticalFactorizationPosition + matchedSymbolsCount < text.Length)
            {
                if (pattern[criticalFactorizationPosition + matchedSymbolsCount] == text[position + criticalFactorizationPosition + matchedSymbolsCount] &&
                    pattern[matchedSymbolsCount] == text[position + matchedSymbolsCount])
                {
                    matchedSymbolsCount++;
                    if (criticalFactorizationPosition + matchedSymbolsCount == pattern.Length)
                    {
                        result = true;
                        matchedSymbolsCount = pattern.Length - (criticalFactorizationPosition + patternPeriod);
                        position = position + patternPeriod;
                    }
                }
                else
                {
                    position = position + matchedSymbolsCount + 1;
                    matchedSymbolsCount = 0;
                }
            }
            return result;
        }

        public void SetState(AlgorithmState state)
        {
            position = state.PositionInText;
            matchedSymbolsCount = state.MatchedSymbolsCount;
        }

        public AlgorithmState State
        {
            get
            {
                return new AlgorithmState(position, matchedSymbolsCount);
            }
        }

        private readonly StringBuilder text;
        private readonly string pattern;
        private readonly int criticalFactorizationPosition;
        private readonly int patternPeriod;
        private int position;
    }
}
