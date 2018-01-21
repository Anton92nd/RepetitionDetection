using System.IO;
using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Logging
{
    public class OutputLogger : IGeneratorLogger
    {
        public OutputLogger([CanBeNull] StreamWriter output)
        {
            Output = output;
            Output?.WriteLine("text | repetition length | border length");
        }

        public void LogAfterGenerate([NotNull] StringBuilder text)
        {
            TextLength = text.Length;
        }

        public void LogRepetition([NotNull] StringBuilder text, Repetition repetition)
        {
            if (Output != null)
            {
                var length = text.Length - 1 - repetition.LeftPosition;
                Output.WriteLine($"{text} {length} {length - repetition.Period}");
            }
        }

        public void LogBeforeGenerate([NotNull] StringBuilder text)
        {
        }

        public StreamWriter Output { get; }
        public volatile int TextLength;
    }
}