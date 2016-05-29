using JetBrains.Annotations;

namespace RepetitionDetection.StringMatching
{
    public interface IStringMatchingAlgorithm
    {
        bool CheckMatch(int textLength);

        void SetState([NotNull] AlgorithmState state);

        [NotNull]
        AlgorithmState State { get; }
    }
}
