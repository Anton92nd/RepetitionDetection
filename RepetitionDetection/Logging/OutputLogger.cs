using System.IO;
using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Logging
{
    public class OutputLogger : IGeneratorLogger
    {
        public volatile int TextLength;

        public OutputLogger([CanBeNull] StreamWriter output)
        {
            Output = output;
            if (Output != null)
            {
                Output.WriteLine("text | repetition length");
            }
        }

        public StreamWriter Output { get; private set; }

        public void LogBeforeGenerate([NotNull] StringBuilder text)
        {
        }

        public void LogAfterGenerate([NotNull] StringBuilder text)
        {
            TextLength = text.Length;
        }

        public void LogRepetition([NotNull] StringBuilder text, Repetition repetition)
        {
            if (Output != null)
            {
                Output.WriteLine("{0} {1}", text, text.Length - 1 - repetition.LeftPosition);
            }
        }
    }
}