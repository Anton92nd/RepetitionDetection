using System.Collections.Generic;
using System.Text;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;

namespace RepetitionDetection.TextGeneration
{
    public static class AllWordsGenerator
    {
        public static List<string> Generate(int alphabetSize, RationalNumber e, bool detectEqual)
        {
            var result = new List<string>();
            var sb = new StringBuilder();
            var detector = new RepetitionDetector(sb, e, detectEqual);
            Generate(sb, detector, result, alphabetSize);
            return result;
        }

        private static void Generate(StringBuilder text, IDetector detector, List<string> result, int alphabetSize)
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
