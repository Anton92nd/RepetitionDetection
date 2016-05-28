namespace RepetitionDetection.Commons
{
    public struct Repetition
    {
        public readonly int LeftPosition;
        public readonly int Period;

        public Repetition(int leftPosition, int period)
        {
            LeftPosition = leftPosition;
            Period = period;
        }
    }
}
