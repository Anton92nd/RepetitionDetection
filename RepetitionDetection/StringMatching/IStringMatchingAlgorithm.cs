namespace RepetitionDetection.StringMatching
{
    public interface IStringMatchingAlgorithm
    {
        bool CheckMatch();

        void SetState(AlgorithmState state);

        AlgorithmState State { get; }
    }
}
