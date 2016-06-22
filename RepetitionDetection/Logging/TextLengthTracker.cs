using System.Text;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Logging
{
    public class TextLengthTracker : IGeneratorLogger
    {
        public volatile int TextLength;

        public void LogBeforeGenerate(StringBuilder text)
        {
            TextLength = text.Length;
        }

        public void LogAfterGenerate(StringBuilder text)
        {
        }

        public void LogRepetition(StringBuilder text, Repetition repetition)
        {
        }
    }
}
