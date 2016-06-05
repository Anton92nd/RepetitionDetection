using RepetitionDetection.Commons;

namespace RepetitionDetection.TextGeneration.RemoveStrategies
{
    public class RemoveBorderStrategy : IRemoveStrategy
    {
        public int GetCharsToDelete(int textLength, Repetition repetition)
        {
            return textLength - (repetition.LeftPosition + 1) - repetition.Period;
        }
    }
}
