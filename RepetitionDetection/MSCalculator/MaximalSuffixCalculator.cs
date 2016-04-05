using System.Collections.Generic;
using System.Text;

namespace RepetitionDetection.MSCalculator
{
    public class MaximalSuffixCalculator
    {
        public MaximalSuffixCalculator(IComparer<char> charComparer)
        {
            this.charComparer = charComparer;
        }

        public void Calculate(string str, MSCalculatorData data)
        {
            while (data.CSPosition + data.CommonSymbolsCount <= data.StringLength)
            {
                var fromMaximalSuffix = str[data.MSPosition + data.CommonSymbolsCount - 1];
                var fromCurrentSuffix = str[data.CSPosition + data.CommonSymbolsCount - 1];
                var comparisonResult = charComparer.Compare(fromMaximalSuffix, fromCurrentSuffix);

                if (comparisonResult > 0)
                {
                    data.CSPosition = data.CSPosition + data.CommonSymbolsCount;
                    data.CommonSymbolsCount = 1;
                    data.Period = data.CSPosition - data.MSPosition;
                }
                else if (comparisonResult == 0)
                {
                    if (data.CommonSymbolsCount == data.Period)
                    {
                        data.CSPosition = data.CSPosition + data.Period;
                        data.CommonSymbolsCount = 1;
                    }
                    else
                        data.CommonSymbolsCount++;
                }
                else
                {
                    data.MSPosition = data.CSPosition;
                    data.CSPosition = data.MSPosition + 1;
                    data.CommonSymbolsCount = data.Period = 1;
                }
            }
        }

        private readonly IComparer<char> charComparer; 
    }
}
