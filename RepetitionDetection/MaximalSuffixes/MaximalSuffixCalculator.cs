using System.Collections.Generic;
using JetBrains.Annotations;
using RepetitionDetection.Commons;

namespace RepetitionDetection.MaximalSuffixes
{
    public class MaximalSuffixCalculator
    {
        public MaximalSuffixCalculator([NotNull] TextSubstring word, [NotNull] IComparer<char> charComparer)
        {
            this.charComparer = charComparer;
            currentSuffixPosition = 1;
            commonSymbolsCount = 1;
            MaximalSuffixPosition = 0;
            suffixPeriod = 1;
            this.word = word;
        }

        public void Calculate(int stringLength)
        {
            while (currentSuffixPosition + commonSymbolsCount <= stringLength)
            {
                var fromMaximalSuffix = word[MaximalSuffixPosition + commonSymbolsCount - 1];
                var fromCurrentSuffix = word[currentSuffixPosition + commonSymbolsCount - 1];
                var comparisonResult = charComparer.Compare(fromMaximalSuffix, fromCurrentSuffix);

                if (comparisonResult > 0)
                {
                    currentSuffixPosition = currentSuffixPosition + commonSymbolsCount;
                    commonSymbolsCount = 1;
                    suffixPeriod = currentSuffixPosition - MaximalSuffixPosition;
                }
                else if (comparisonResult == 0)
                {
                    if (commonSymbolsCount == suffixPeriod)
                    {
                        currentSuffixPosition = currentSuffixPosition + suffixPeriod;
                        commonSymbolsCount = 1;
                    }
                    else
                        commonSymbolsCount++;
                }
                else
                {
                    MaximalSuffixPosition = currentSuffixPosition;
                    currentSuffixPosition = MaximalSuffixPosition + 1;
                    commonSymbolsCount = suffixPeriod = 1;
                }
            }
        }

        [NotNull]
        private readonly IComparer<char> charComparer;

        [NotNull]
        private readonly TextSubstring word;

        public int MaximalSuffixPosition { get; private set; }

        private int suffixPeriod;
        private int currentSuffixPosition;
        private int commonSymbolsCount;
    }
}
