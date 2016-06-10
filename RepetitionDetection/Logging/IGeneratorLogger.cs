using System.Text;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Logging
{
    public interface IGeneratorLogger
    {
        void LogBeforeGenerate(StringBuilder text);
        void LogAfterGenerate(StringBuilder text);
        void LogRepetition(StringBuilder text, Repetition repetition);
    }
}
