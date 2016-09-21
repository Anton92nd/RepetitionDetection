using RepetitionDetection.Commons;

namespace RepetitionDetection.TextGeneration.RemoveStrategies
{
    public class RemoveBorderStrategy : IRemoveStrategy
    {
        public int GetCharsToDelete(int textLength, Repetition repetition)
        {
            return textLength - (repetition.LeftPosition + 1) - repetition.Period;
        }

        public int PeriodsToRemove { get { return 0; } }

        public override string ToString()
        {
            return "Remove border strategy";
        }
    }
}
