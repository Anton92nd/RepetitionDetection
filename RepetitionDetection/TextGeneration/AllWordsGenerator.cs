using System.Collections.Generic;
using System.Text;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;

namespace RepetitionDetection.TextGeneration
{
    public static class AllWordsGenerator
    {
        public static List<string> Generate(Detector detector, int alphabetSize)
        {
            var result = new List<string>();
            Generate(detector.Text, detector, result, alphabetSize);
            return result;
        }

        private static void Generate(StringBuilder text, Detector detector, List<string> result, int alphabetSize)
        {
            Repetition repetition;
            var foundSymbol = false;
            for (var i = 0; i < alphabetSize; i++)
            {
                var c = (char) ('a' + i);
                text.Append(c);
                if (!detector.TryDetect(out repetition))
                {
                    foundSymbol = true;
                    Generate(text, detector, result, alphabetSize);
                }
                detector.BackTrack();
                text.Remove(text.Length - 1, 1);
            }
            if (!foundSymbol)
            {
                result.Add(text.ToString());
            }
        }
    }
}
