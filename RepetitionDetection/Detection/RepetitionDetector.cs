using System.Text;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Detection
{
    public class RepetitionDetector : IDetector
    {
        private readonly IDetector smallRepetitionsDetector;
        private readonly IDetector largeRepetitionsDetector;
        private bool SkipBacktrack;

        public RepetitionDetector(StringBuilder text, RationalNumber e, bool detectEqual)
        {
            if (text.Length > 0)
            {
                throw new InvalidUsageException("Text must be empty when creating LargeRepetitionDetector");
            }
            smallRepetitionsDetector = new SmallRepetitionDetector(text, e, detectEqual);
            largeRepetitionsDetector = new LargeRepetitionDetector(text, e, detectEqual);
            SkipBacktrack = false;
        }

        public bool TryDetect(out Repetition repetition)
        {
            if (smallRepetitionsDetector.TryDetect(out repetition))
            {
                SkipBacktrack = true;
                return true;
            }
            return largeRepetitionsDetector.TryDetect(out repetition);
        }

        public void BackTrack()
        {
            if (!SkipBacktrack)
            {
                largeRepetitionsDetector.BackTrack();
                smallRepetitionsDetector.BackTrack();
            }
            SkipBacktrack = false;
        }
    }
}
