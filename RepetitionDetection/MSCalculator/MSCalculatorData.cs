namespace RepetitionDetection.MSCalculator
{
    public class MSCalculatorData
    {
        public int MSPosition { get; set; }
        public int CSPosition { get; set; }
        public int Period { get; set; }
        public int CommonSymbolsCount { get; set; }
        public int StringLength { get; set; }

        public static MSCalculatorData Default
        {
            get
            {
                return new MSCalculatorData
                {
                    CommonSymbolsCount = 1,
                    CSPosition = 1,
                    MSPosition = 0,
                    Period = 1,
                    StringLength = 0
                };
            }
        }
    }
}
