using RepetitionDetection.Commons;

namespace RepetitionDetection.TextGeneration.RemoveStrategies
{
    public class RemovePeriodsStrategy : IRemoveStrategy
    {
        public RemovePeriodsStrategy(int periodsCount)
        {
            PeriodsCount = periodsCount;
        }

        public int GetCharsToDelete(int textLength, Repetition repetition) => repetition.Period * PeriodsCount;

        public override string ToString() => $"RemovePeriodsStrategy({PeriodsCount})";

        public int PeriodsCount { get; }
    }
}