using System;
using System.Collections.Generic;
using System.Text;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Detection
{
    public class SillyDetector : Detector
    {
        private readonly List<int> suffixFunction;

        public SillyDetector(StringBuilder text, RationalNumber e, bool detectEqual) : base(text, e, detectEqual)
        {
            suffixFunction = new List<int>();
        }

        public override bool TryDetect(out Repetition repetition)
        {
            repetition = new Repetition(0, 0);
            var n = Text.Length;
            while (suffixFunction.Count < Text.Length)
                suffixFunction.Add(0);
            suffixFunction[0] = 0;
            var result = false;
            var period = 0;
            for (var i = 1; i < Text.Length && !result; ++i)
            {
                var j = suffixFunction[i - 1];
                while (j > 0 && Text[n - 1 - i] != Text[n - 1 - j])
                    j = suffixFunction[j - 1];
                if (Text[n - 1 - i] == Text[n - 1 - j])
                    j++;
                suffixFunction[i] = j;
                period = Math.Max(period, i + 1 - suffixFunction[i]);
                if (i + 1 - (DetectEqual ? 0 : 1) >= (E * period).Ceil())
                {
                    repetition = new Repetition(n - 2 - i, period);
                    result = true;
                }
            }
            return result;
        }

        public override void BackTrack()
        {
        }
    }
}
