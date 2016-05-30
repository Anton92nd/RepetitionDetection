using System.Text;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Detection
{
    public class RepetitionDetector : IDetector
    {
        private readonly IDetector smallRepetitionsDetector;
        private readonly IDetector largeRepetitionsDetector;

        public RepetitionDetector(StringBuilder text, RationalNumber e, bool detectEqual)
        {
            if (text.Length > 0)
            {
                throw new InvalidUsageException("Text must be empty when creating LargeRepetitionDetector");
            }
            smallRepetitionsDetector = new SmallRepetitionDetector(text, e, detectEqual);
            largeRepetitionsDetector = new LargeRepetitionDetector(text, e, detectEqual);
        }

        public bool TryDetect(out Repetition repetition)
        {
            var result = largeRepetitionsDetector.TryDetect(out repetition) || smallRepetitionsDetector.TryDetect(out repetition);
            return result;
        }

        public void BackTrack()
        {
            largeRepetitionsDetector.BackTrack();
            smallRepetitionsDetector.BackTrack();
        }
    }
}
