using System;
using System.Text;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Logging
{
    public class ConsoleTextLengthLogger : IGeneratorLogger
    {
        public ConsoleTextLengthLogger(int lengthStep)
        {
            length = lengthStep;
        }

        public void LogAfterGenerate(StringBuilder text)
        {
        }

        public void LogRepetition(StringBuilder text, Repetition repetition)
        {
        }

        public void LogBeforeGenerate(StringBuilder text)
        {
            if (text.Length % length == 0)
                Console.Write("\rText length: {0}    ", text.Length);
        }

        private readonly int length;
    }
}