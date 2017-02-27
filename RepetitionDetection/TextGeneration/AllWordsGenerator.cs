using System;
using System.Collections.Generic;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;

namespace RepetitionDetection.TextGeneration
{
    public static class AllWordsGenerator
    {
        public static List<string> Generate(Detector detector, int alphabetSize)
        {
            var result = new List<string>();
            var text = detector.Text;
            var found = new List<bool>{false};
            text.Append('a');
            while (text.Length > 0)
            {
                //Console.Write("\rText length: {0}", text.Length);
                if (text[text.Length - 1] - 'a' + 1 > alphabetSize)
                {
                    if (text.Length == 1)
                        break;
                    text.Remove(text.Length - 1, 1);
                    detector.Backtrack();
                    if (!found[text.Length])
                        result.Add(text.ToString());
                    found.RemoveAt(found.Count - 1);
                    text[text.Length - 1]++;
                    continue;
                }
                Repetition repetition;
                if (detector.TryDetect(out repetition))
                {
                    detector.Backtrack();
                    text[text.Length - 1]++;
                }
                else
                {
                    found[text.Length - 1] = true;
                    text.Append('a');
                    found.Add(false);
                }
            }
            return result;
        }
    }
}
