using System.Text;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Detection
{
    public abstract class Detector
    {
        public StringBuilder Text { get; private set; }
        public RationalNumber E { get; private set; }
        public bool DetectEqual { get; private set; }
        protected int S { get; private set; }

        public Detector(StringBuilder text, RationalNumber e, bool detectEqual)
        {
            if (text.Length > 0)
            {
                throw new InvalidUsageException("Text must be empty when creating detector");
            }
            Text = text;
            E = e;
            DetectEqual = detectEqual;
            S = (e / (e - 1)).Ceil();
        }

        public abstract bool TryDetect(out Repetition repetition);
        public abstract void Backtrack();
        public abstract void Reset();
    }
}