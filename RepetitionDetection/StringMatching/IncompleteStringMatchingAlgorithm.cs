using System.Text;

namespace RepetitionDetection.StringMatching
{
    public class IncompleteStringMatchingAlgorithm : IStringMatchingAlgorithm
    {
        public IncompleteStringMatchingAlgorithm(StringBuilder text, int startPosition, string pattern, int prefixLength,
            int criticalPosition, int period)
        {
            this.text = text;
            textPosition = startPosition;

            this.pattern = pattern;
            this.prefixLength = prefixLength;
            this.criticalPosition = criticalPosition;
            this.period = period;
        }

        public bool CheckMatch()
        {
            var result = false;
            while (textPosition + criticalPosition + matchedSymbolsCount < text.Length)
            {
                if (pattern[criticalPosition + matchedSymbolsCount] == text[textPosition + criticalPosition + matchedSymbolsCount])
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
        private int matchedSymbolsCount;
    }
}
