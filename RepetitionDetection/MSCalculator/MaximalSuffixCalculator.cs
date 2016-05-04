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

        public MSCalculatorData Calculate(StringBuilder str, MSCalculatorData data)
        {
            var CSPosition = data.CSPosition;
            var MSPosition = data.MSPosition;
            var Period = data.Period;
            var CommonSymbolsCount = data.CommonSymbolsCount;
            while (CSPosition + CommonSymbolsCount <= data.StringLength)
            {
                var fromMaximalSuffix = str[MSPosition + CommonSymbolsCount - 1];
                var fromCurrentSuffix = str[CSPosition + CommonSymbolsCount - 1];
                var comparisonResult = charComparer.Compare(fromMaximalSuffix, fromCurrentSuffix);

                if (comparisonResult > 0)
                {
                    CSPosition = CSPosition + CommonSymbolsCount;
                    CommonSymbolsCount = 1;
                    Period = CSPosition - MSPosition;
                }
                else if (comparisonResult == 0)
                {
                    if (CommonSymbolsCount == Period)
                    {
                        CSPosition = CSPosition + Period;
                        CommonSymbolsCount = 1;
                    }
                    else
                        CommonSymbolsCount++;
                }
                else
                {
                    MSPosition = CSPosition;
                    CSPosition = MSPosition + 1;
                    CommonSymbolsCount = Period = 1;
                }
            }
            return new MSCalculatorData(MSPosition, CSPosition, Period, CommonSymbolsCount, data.StringLength);
        }

        private readonly IComparer<char> charComparer; 
    }
}
