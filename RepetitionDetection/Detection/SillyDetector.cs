using System;
using System.Collections.Generic;
using System.Text;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Detection
{
    public class SillyDetector : IDetector
    {
        private readonly StringBuilder text;
        private readonly RationalNumber e;
        private readonly bool detectEqual;
        private readonly List<int> suffixFunction;

        public SillyDetector(StringBuilder text, RationalNumber e, bool detectEqual)
        {
            this.text = text;
            this.e = e;
            this.detectEqual = detectEqual;
            suffixFunction = new List<int>();
        }

        public bool TryDetect(out Repetition repetition)
        {
            repetition = new Repetition(0, 0);
            var n = text.Length;
            while (suffixFunction.Count < text.Length)
                suffixFunction.Add(0);
            suffixFunction[0] = 0;
            var result = false;
            var period = 0;
            for (var i = 1; i < text.Length && !result; ++i)
            {
                var j = suffixFunction[i - 1];
                while (j > 0 && text[n - 1 - i] != text[n - 1 - j])
                    j = suffixFunction[j - 1];
                if (text[n - 1 - i] == text[n - 1 - j])
                    j++;
                suffixFunction[i] = j;
                period = Math.Max(period, i + 1 - suffixFunction[i]);
                if (i + 1 - (detectEqual ? 0 : 1) >= (e * period).Ceil())
                {
                    repetition = new Repetition(n - 2 - i, period);
                    result = true;
                }
            }
            return result;
        }

        public void BackTrack()
        {
        }
    }
}
