using RepetitionDetection.Commons;

namespace RepetitionDetection.TextGeneration.RemoveStrategies
{
    public class CustomRemoveStrategy : IRemoveStrategy
    {
        public int GetCharsToDelete(int textLength, Repetition repetition)
        {
            if (repetition.Period >= 12 && repetition.Period <= 30)
            {
                return 9;
            }
            return baseStrategy.GetCharsToDelete(textLength, repetition);
        }

        public override string ToString()
        {
            return $"NoMoreThanStrategy([12, 30], 9, {baseStrategy})";
        }

        private readonly IRemoveStrategy baseStrategy = new RemoveBorderStrategy();
    }
}