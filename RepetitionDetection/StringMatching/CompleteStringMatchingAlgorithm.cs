using System;
using System.Text;
using RepetitionDetection.Commons;
using RepetitionDetection.CriticalFactorization;

namespace RepetitionDetection.StringMatching
{
    public class CompleteStringMatchingAlgorithm : IStringMatchingAlgorithm
    {
        public CompleteStringMatchingAlgorithm(StringBuilder text, string pattern, int templatePeriod, int startPosition)
        {
            criticalFactorizationPosition = Factorizer.GetFactorization(pattern).PatternCriticalPosition;
            if (criticalFactorizationPosition > pattern.Length / 2)
                throw new InvalidUsageException(string.Format("Invalid usage of Complete string Matching algo:\nTemplate: {0}\nCritical factorization position: {1}", pattern, criticalFactorizationPosition));
            this.text = text;
            this.pattern = pattern;
            this.templatePeriod = templatePeriod;

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
                        matchedSymbolsCount = pattern.Length - (criticalFactorizationPosition + templatePeriod);
                        position = position + templatePeriod;
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
        private readonly int templatePeriod;
        private int position;
    }
}
