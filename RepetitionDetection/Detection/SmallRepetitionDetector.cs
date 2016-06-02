using System;
using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Detection
{
    public class SmallRepetitionDetector : IDetector
    {
        public SmallRepetitionDetector([NotNull] StringBuilder text, RationalNumber e, bool detectEqual)
        {
            this.text = text;
            var s = Math.Max(2, (e/(e - 1)).Ceil());
            lastSymbolsCount = s - 1;
            this.detectEqual = detectEqual;
            this.e = e;
            suffixFunction = new int[lastSymbolsCount];
        }

        public bool TryDetect(out Repetition repetition)
        {
            repetition = new Repetition(0, 0);
            var result = false;
            var n = text.Length;
            suffixFunction[0] = 0;
            var period = 1;
            for (var i = 1; i < Math.Min(lastSymbolsCount, text.Length) && !result; ++i)
            {
                var j = suffixFunction[i - 1];
                while (j > 0 && text[n - 1 - i] != text[n - 1 - j])
                    j = suffixFunction[j - 1];
                if (text[n - 1 - i] == text[n - 1 - j])
                    j++;
                suffixFunction[i] = j;
                period = Math.Max(period, i + 1 - suffixFunction[i]);
                if (i + 1 - (detectEqual ? 0 : 1) >= (e*period).Ceil())
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

        [NotNull]
        private readonly StringBuilder text;

        private readonly RationalNumber e;
        private readonly int lastSymbolsCount;
        private readonly bool detectEqual;
        private readonly int[] suffixFunction;
    }
}
