namespace RepetitionDetection.StringMatching
{
    public interface IStringMatchingAlgorithm
    {
        bool CheckMatch(int textLength);

        void SetState(AlgorithmState state);

        AlgorithmState State { get; }
    }
}
