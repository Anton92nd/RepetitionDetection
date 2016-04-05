namespace RepetitionDetection.MSCalculator
{
    public static class MSCalculatorExtensions
    {
        public static MSCalculatorData WithStringLength(this MSCalculatorData data, int length)
        {
            data.StringLength = length;
            return data;
        }
    }
}
