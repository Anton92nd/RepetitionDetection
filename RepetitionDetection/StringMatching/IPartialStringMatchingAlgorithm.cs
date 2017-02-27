namespace RepetitionDetection.StringMatching
{
    public interface IPartialStringMatchingAlgorithm
    {
        bool CheckMatch(int textLength);

        AlgorithmState State { get; set; }
    }
}