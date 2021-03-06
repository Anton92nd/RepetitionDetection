﻿using System;
using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Detection
{
    public class SmallRepetitionDetector : Detector
    {
        public SmallRepetitionDetector([NotNull] StringBuilder text, RationalNumber e, bool detectEqual) : base(text, e,
            detectEqual)
        {
            lastSymbolsCount = S - 1;
            suffixFunction = new int[lastSymbolsCount];
        }

        public override bool TryDetect(out Repetition repetition)
        {
            repetition = new Repetition(0, 0);
            var result = false;
            var n = Text.Length;
            suffixFunction[0] = 0;
            var period = 1;
            for (var i = 1; i < Math.Min(lastSymbolsCount, Text.Length) && !result; ++i)
            {
                var j = suffixFunction[i - 1];
                while (j > 0 && Text[n - 1 - i] != Text[n - 1 - j])
                    j = suffixFunction[j - 1];
                if (Text[n - 1 - i] == Text[n - 1 - j])
                    j++;
                suffixFunction[i] = j;
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
            var exponent = new RationalNumber(Text.Length - (repetition.LeftPosition + 1), repetition.Period);
            return DetectEqual ? exponent >= E : exponent > E;
        }

        public override void Backtrack()
        {
        }

        public override void Reset()
        {
            Text.Clear();
        }

        private readonly int lastSymbolsCount;
        private readonly int[] suffixFunction;
    }
}