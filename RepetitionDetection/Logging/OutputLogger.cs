using System.IO;
using System.Text;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Logging
{
    public class OutputLogger : IGeneratorLogger
    {
        public OutputLogger(StreamWriter output)
        {
            Output = output;
        }

        public volatile int TextLength;
        public StreamWriter Output { get; private set; }

        public void LogBeforeGenerate(StringBuilder text)
        {
        }

        public void LogAfterGenerate(StringBuilder text)
        {
            TextLength = text.Length;
        }

        public void LogRepetition(StringBuilder text, Repetition repetition)
        {
            if (Output != null)
            {
                Output.Write(text.Length - 1 - repetition.LeftPosition);
                Output.Write(repetition.Period);
            }
        }
    }
}
