using System.Collections.Generic;
using System.Text;

namespace RepetitionDetection
{
    public class MaximalSuffixCalculator
    {
        public MaximalSuffixCalculator(IComparer<char> charComparer)
        {
            this.charComparer = charComparer;
            maximaxSuffixPosition = -1;
            currentSuffixPosition = 0;
            equalSymbolsCount = 1;
            period = 1;
            str = new StringBuilder();
        }

        public void Append(char c)
        {
            str.Append(c);
            if(currentSuffixPosition + equalSymbolsCount >= str.Length)
                return;

            var fromMaximalSuffix = str[maximaxSuffixPosition + equalSymbolsCount];
            var fromCurrentSuffix = str[currentSuffixPosition + equalSymbolsCount];
            var comparisonResult = charComparer.Compare(fromMaximalSuffix, fromCurrentSuffix);

            if(comparisonResult > 0)
            {
                currentSuffixPosition = currentSuffixPosition + equalSymbolsCount;
                equalSymbolsCount = 1;
                period = currentSuffixPosition - maximaxSuffixPosition;
            }
            else if(comparisonResult == 0)
            {
                if(equalSymbolsCount == period)
                {
                    currentSuffixPosition = currentSuffixPosition + period;
                    equalSymbolsCount = 1;
                }
                else
                    equalSymbolsCount++;
            }
            else
            {
                maximaxSuffixPosition = currentSuffixPosition;
                currentSuffixPosition = maximaxSuffixPosition + 1;
                equalSymbolsCount = period = 1;
            }
        }

        public int GetPosition()
        {
            return maximaxSuffixPosition + 1;
        }

        public int GetMaximalSuffixPeriod()
        {
            return period;
        }

        private readonly StringBuilder str;
        private readonly IComparer<char> charComparer; 
        private int maximaxSuffixPosition, currentSuffixPosition, equalSymbolsCount, period;
    }
}
