using RepetitionDetection.Commons;

namespace RepetitionDetection.TextGeneration.RemoveStrategies
{
    public interface IRemoveStrategy
    {
        int GetCharsToDelete(int textLength, Repetition repetition);
    }
}
