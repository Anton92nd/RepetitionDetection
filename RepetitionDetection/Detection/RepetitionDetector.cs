using System.Text;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Detection
{
    public class RepetitionDetector : Detector
    {
        public RepetitionDetector(StringBuilder text, RationalNumber e, bool detectEqual) : base(text, e, detectEqual)
        {
            smallRepetitionsDetector = new SmallRepetitionDetector(text, e, detectEqual);
            largeRepetitionsDetector = new LargeRepetitionDetector(text, e, detectEqual);
            skipBacktrack = false;
        }

        public override bool TryDetect(out Repetition repetition)
        {
            if (smallRepetitionsDetector.TryDetect(out repetition))
            {
                skipBacktrack = true;
                return true;
            }
            return largeRepetitionsDetector.TryDetect(out repetition);
        }

        public override void Backtrack()
        {
            if (!skipBacktrack)
            {
                largeRepetitionsDetector.Backtrack();
                smallRepetitionsDetector.Backtrack();
            }
            skipBacktrack = false;
        }

        public override void Reset()
        {
            smallRepetitionsDetector.Reset();
            largeRepetitionsDetector.Reset();
            skipBacktrack = false;
        }

        private readonly LargeRepetitionDetector largeRepetitionsDetector;
        private readonly SmallRepetitionDetector smallRepetitionsDetector;
        private bool skipBacktrack;
    }
}