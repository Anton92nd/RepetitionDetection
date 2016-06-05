using System;
using System.Text;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace RepetitionDetection.TextGeneration
{
    public static class RandomWordGenerator
    {
        private static readonly Random Random = new Random(23);

        public static int CharsGenerated { get; private set; }

        private static char GetRandomChar(int alphabetSize)
        {
            ++CharsGenerated;
            return (char) Random.Next('a', 'a' + alphabetSize);
        }

        public static StringBuilder Generate(Detector detector, int alphabetSize, int length, IRemoveStrategy removeStrategy)
        {
            CharsGenerated = 0;
            var text = detector.Text;
            text.EnsureCapacity(length);
            while (text.Length < length)
            {
                //Console.Write("\rText length: {0}   ", text.Length);
                text.Append(GetRandomChar(alphabetSize));
                Repetition repetition;
                if (detector.TryDetect(out repetition))
                {
                    var charsToDelete = removeStrategy.GetCharsToDelete(text.Length, repetition);
                    for (var i = 0; i < charsToDelete; ++i)
                    {
                        detector.BackTrack();
                        text.Remove(text.Length - 1, 1);
                    }
                }
            }
            return text;
        }
    }
}
