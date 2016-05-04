namespace RepetitionDetection.MSCalculator
{
    public class MSCalculatorData
    {
        public MSCalculatorData(int mSPosition, int cSPosition, int period, int commonSymbolsCount, int stringLength)
        {
            MSPosition = mSPosition;
            CSPosition = cSPosition;
            Period = period;
            CommonSymbolsCount = commonSymbolsCount;
            StringLength = stringLength;
        }

        public MSCalculatorData WithStringLength(int stringLength)
        {
            return new MSCalculatorData(MSPosition, CSPosition, Period, CommonSymbolsCount, stringLength);
        }

        public int MSPosition { get; private set; }
        public int CSPosition { get; private set; }
        public int Period { get; private set; }
        public int CommonSymbolsCount { get; private set; }
        public int StringLength { get; private set; }

        public static MSCalculatorData Default
        {
            get
            {
                return new MSCalculatorData(0, 1, 1, 1, 0);
            }
        }
    }
}
