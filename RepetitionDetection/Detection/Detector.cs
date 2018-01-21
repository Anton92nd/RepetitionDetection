using System.Text;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Detection
{
    public abstract class Detector
    {
        protected Detector(StringBuilder text, RationalNumber e, bool detectEqual)
        {
            if (text.Length > 0)
                throw new InvalidProgramStateException("Text must be empty when creating detector");
            Text = text;
            E = e;
            DetectEqual = detectEqual;
            S = (e / (e - 1)).Ceil();
        }

        public abstract bool TryDetect(out Repetition repetition);
        public abstract void Backtrack();
        public abstract void Reset();
        public StringBuilder Text { get; }
        public RationalNumber E { get; }
        public bool DetectEqual { get; }
        protected int S { get; }
    }
}