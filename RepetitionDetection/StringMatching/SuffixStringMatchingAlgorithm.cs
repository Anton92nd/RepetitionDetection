using System;
using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.Commons;

namespace RepetitionDetection.StringMatching
{
    public class SuffixStringMatchingAlgorithm : IPartialStringMatchingAlgorithm
    {
        public SuffixStringMatchingAlgorithm(
            [NotNull] StringBuilder text,
            [NotNull] TextSubstring pattern,
            int startPosition,
            int prefixLength,
            int criticalPosition,
            int period)
        {
            this.text = text;
            this.pattern = pattern;

            textPosition = startPosition;
            this.prefixLength = prefixLength;
            this.criticalPosition = criticalPosition;
            this.period = period;
        }

        public bool CheckMatch(int textLength)
        {
            var result = false;
            while (textPosition + criticalPosition + matchedSymbolsCount < textLength)
            {
                if (pattern[criticalPosition + matchedSymbolsCount] == text[textPosition + criticalPosition + matchedSymbolsCount])
                {
                    matchedSymbolsCount++;
                    if (criticalPosition + matchedSymbolsCount == prefixLength)
                    {
                        result = true;
                        matchedSymbolsCount = Math.Max(0, pattern.Length - (criticalPosition + period));
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

        public AlgorithmState State
        {
            get
            {
                return new AlgorithmState(textPosition, matchedSymbolsCount);
            }

            set
            {
                textPosition = value.PositionInText;
                matchedSymbolsCount = value.MatchedSymbolsCount;
            }
        }

        [NotNull]
        private readonly StringBuilder text;

        [NotNull]
        private readonly TextSubstring pattern;

        private readonly int prefixLength;
        private readonly int criticalPosition;
        private readonly int period;

        private int textPosition;
        private int matchedSymbolsCount;
    }
}
