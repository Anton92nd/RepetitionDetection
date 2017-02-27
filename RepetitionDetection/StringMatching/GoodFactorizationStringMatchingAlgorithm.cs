﻿using System;
using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.Commons;

namespace RepetitionDetection.StringMatching
{
    public class GoodFactorizationStringMatchingAlgorithm : IPartialStringMatchingAlgorithm
    {
        public GoodFactorizationStringMatchingAlgorithm([NotNull] StringBuilder text, [NotNull] TextSubstring pattern, int startPosition, int prefixLength, int criticalPosition, int period)
        { 
            this.text = text;

            this.pattern = pattern;
            this.prefixLength = prefixLength;
            this.criticalPosition = criticalPosition;
            this.period = period;

            textPosition = startPosition;
            matchedSymbolsCount = 0;
        }

        private int matchedSymbolsCount;

        public bool CheckMatch(int textLength)
        {
            var result = false;
            while (textPosition + criticalPosition + matchedSymbolsCount < textLength)
            {
                if (pattern[criticalPosition + matchedSymbolsCount] == text[textPosition + criticalPosition + matchedSymbolsCount] &&
                    pattern[matchedSymbolsCount] == text[textPosition + matchedSymbolsCount])
                {
                    matchedSymbolsCount++;
                    if (criticalPosition + matchedSymbolsCount == prefixLength)
                    {
                        result = true;
                        matchedSymbolsCount = Math.Max(0, prefixLength - (criticalPosition + period));
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
    }
}