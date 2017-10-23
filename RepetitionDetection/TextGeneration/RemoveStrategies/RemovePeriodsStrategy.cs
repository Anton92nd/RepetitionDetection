using RepetitionDetection.Commons;

namespace RepetitionDetection.TextGeneration.RemoveStrategies
{
    public class RemovePeriodsStrategy : IRemoveStrategy
    {
        public int PeriodsCount { get; set; }

        public RemovePeriodsStrategy(int periodsCount)
        {
            PeriodsCount = periodsCount;
        }

        public int GetCharsToDelete(int textLength, Repetition repetition)
        {
            return repetition.Period*PeriodsCount;
        }

        public override string ToString()
        {
            return string.Format("RemovePeriodsStrategy({0})", PeriodsCount);
        }
    }
}
