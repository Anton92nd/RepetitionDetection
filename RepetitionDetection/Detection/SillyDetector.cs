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
            suffixFunction.Clear();
            suffixFunction.Add(0);
            var result = false;
            var period = 0;
            for (var i = 1; i < n && !result; ++i)
            {
                var j = suffixFunction[i - 1];
                while (j > 0 && Text[n - 1 - i] != Text[n - 1 - j])
                    j = suffixFunction[j - 1];
                if (Text[n - 1 - i] == Text[n - 1 - j])
                    j++;
                suffixFunction.Add(j);
                period = Math.Max(period, i + 1 - suffixFunction[i]);
                var rep = new Repetition(n - i - 2, period);
                if (FoundRepetition(rep))
                {
                    repetition = rep;
                    result = true;
                }
            }
            return result;
        }

        private bool FoundRepetition(Repetition repetition)
        {
            return DetectEqual ? new RationalNumber(Text.Length - (repetition.LeftPosition + 1), repetition.Period) >= E
                : new RationalNumber(Text.Length - (repetition.LeftPosition + 1), repetition.Period) > E;
        }

        public override void Backtrack()
        {
        }

        public override void Reset()
        {
            Text.Clear();
        }
    }
}
