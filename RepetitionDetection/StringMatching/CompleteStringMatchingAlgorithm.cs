using System;
using System.Text;

namespace RepetitionDetection.StringMatching
{
    public class CompleteStringMatchingAlgorithm : IStringMatchingAlgorithm
    {
        public CompleteStringMatchingAlgorithm(StringBuilder text, string template, int criticalFactorizationPosition,
            int templatePeriod, int startPosition)
        {
            if (criticalFactorizationPosition > template.Length / 2)
                throw new Exception(string.Format("Invalid usage of Complete string Matching algo:\nTemplate: {0}\nCritical factorization position: {1}", template, criticalFactorizationPosition));
            this.text = text;
            this.template = template;
            this.criticalFactorizationPosition = criticalFactorizationPosition;
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
                if (template[criticalFactorizationPosition + matchedSymbolsCount] == text[position + criticalFactorizationPosition + matchedSymbolsCount] &&
                    template[matchedSymbolsCount] == text[position + matchedSymbolsCount])
                {
                    matchedSymbolsCount++;
                    if (criticalFactorizationPosition + matchedSymbolsCount == template.Length)
                    {
                        result = true;
                        matchedSymbolsCount = template.Length - (criticalFactorizationPosition + templatePeriod);
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
        private readonly string template;
        private readonly int criticalFactorizationPosition;
        private readonly int templatePeriod;
        private int position;
    }
}
