using RepetitionDetection.Commons;

namespace RepetitionDetection.TextGeneration.RemoveStrategies
{
    public class RemovePeriodMultipleStrategy : IRemoveStrategy
    {
        public int PeriodsCount { get; set; }

        public RemovePeriodMultipleStrategy(int periodsCount)
        {
            PeriodsCount = periodsCount;
        }

        public int GetCharsToDelete(int textLength, Repetition repetition)
        {
            return repetition.Period*PeriodsCount;
        }

        public override string ToString()
        {
            return string.Format("Remove {0} period(s) strategy", PeriodsCount);
        }
    }
}
