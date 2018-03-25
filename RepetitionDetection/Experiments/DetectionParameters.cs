using JetBrains.Annotations;
using RepetitionDetection.Detection;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace Experiments
{
    public class DetectionParameters
    {
        public DetectionParameters([NotNull] Detector detector, [NotNull] IRemoveStrategy removeStrategy)
        {
            Detector = detector;
            RemoveStrategy = removeStrategy;
        }

        [NotNull]
        public Detector Detector { get; }

        [NotNull]
        public IRemoveStrategy RemoveStrategy { get; }
    }
}