using System.Collections.Generic;

namespace RepetitionDetection.MaximalSuffixCalculator
{
    public class MaximalSuffixCalculator
    {
        public MaximalSuffixCalculator(string str, IComparer<char> charComparer)
        {
            this.charComparer = charComparer;
            currentSuffixPosition = 1;
            commonSymbolsCount = 1;
            MaximalSuffixPosition = 0;
            period = 1;
            this.str = str;
        }

        public void Calculate(int stringLength)
        {
            while (currentSuffixPosition + commonSymbolsCount <= stringLength)
            {
                var fromMaximalSuffix = str[MaximalSuffixPosition + commonSymbolsCount - 1];
                var fromCurrentSuffix = str[currentSuffixPosition + commonSymbolsCount - 1];
                var comparisonResult = charComparer.Compare(fromMaximalSuffix, fromCurrentSuffix);

                if (comparisonResult > 0)
                {
                    currentSuffixPosition = currentSuffixPosition + commonSymbolsCount;
                    commonSymbolsCount = 1;
                    period = currentSuffixPosition - MaximalSuffixPosition;
                }
                else if (comparisonResult == 0)
                {
                    if (commonSymbolsCount == period)
                    {
                        currentSuffixPosition = currentSuffixPosition + period;
                        commonSymbolsCount = 1;
                    }
                    else
                        commonSymbolsCount++;
                }
                else
                {
                    MaximalSuffixPosition = currentSuffixPosition;
                    currentSuffixPosition = MaximalSuffixPosition + 1;
                    commonSymbolsCount = period = 1;
                }
            }
        }

        private readonly IComparer<char> charComparer;
        private readonly string str;
        public int MaximalSuffixPosition { get; private set; }
        private int currentSuffixPosition;
        private int commonSymbolsCount;
        private int period;
    }
}
