namespace RepetitionDetection.Statistics
{
    public class PositionStatistics
    {
        public int LengthDeltaSum { get; set; }

        public int VisitCount { get; set; }

        public int AdvanceCount { get; set; }

        public int AdvanceAfterAdvanceCount { get; set; }

        public int AfterAdvanceCount { get; set; }

        public double MovingAverageAdvance { get; set; }
    }
}