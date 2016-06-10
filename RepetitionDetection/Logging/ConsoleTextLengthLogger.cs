using System;
using System.Text;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Logging
{
    public class ConsoleTextLengthLogger : IGeneratorLogger
    {
        private readonly int length;

        public ConsoleTextLengthLogger(int lengthStep)
        {
            this.length = lengthStep;
        }

        public void LogBeforeGenerate(StringBuilder text)
        {
            if (text.Length % length == 0)
                Console.Write("\rText length: {0}    ", text.Length);
        }

        public void LogAfterGenerate(StringBuilder text)
        {
        }

        public void LogRepetition(StringBuilder text, Repetition repetition)
        {
        }
    }
}
