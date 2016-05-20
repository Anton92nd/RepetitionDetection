using System.Text;
using RepetitionDetection.Commons;
using RepetitionDetection.Periods;

namespace RepetitionDetection.StringMatching
{
    public class CompleteStringMatchingAlgorithm : IStringMatchingAlgorithm
    {
        public CompleteStringMatchingAlgorithm(StringBuilder text, int startPosition, string pattern, int prefixLength, int criticalPosition, int period)
        { 
            if (criticalPosition > pattern.Length / 2)
                throw new InvalidUsageException(string.Format("Invalid usage of Complete string Matching algo:\nTemplate: {0}\nCritical factorization position: {1}", pattern, criticalPosition));
            this.text = text;

            this.pattern = pattern;
            this.prefixLength = prefixLength;
            this.criticalPosition = criticalPosition;
            this.period = period;

            textPosition = startPosition;
            matchedSymbolsCount = 0;
        }

        private int matchedSymbolsCount;

        public bool CheckMatch()
        {
            var result = false;
            while (textPosition + criticalPosition + matchedSymbolsCount < text.Length)
            {
                if (pattern[criticalPosition + matchedSymbolsCount] == text[textPosition + criticalPosition + matchedSymbolsCount] &&
                    pattern[matchedSymbolsCount] == text[textPosition + matchedSymbolsCount])
                {
                    matchedSymbolsCount++;
                    if (criticalPosition + matchedSymbolsCount == prefixLength)
                    {
                        result = true;
                        matchedSymbolsCount = pattern.Length - (criticalPosition + period);
                        textPosition = textPosition + period;
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
        private readonly int prefixLength;
        private readonly int criticalPosition;
        private readonly int period;
        private int textPosition;
    }
}
