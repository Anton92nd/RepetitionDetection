using RepetitionDetection.Commons;

namespace RepetitionDetection.Detection
{
    public interface IDetector
    {
        bool TryDetect(out Repetition repetition);
        void BackTrack();
    }
}