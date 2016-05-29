namespace RepetitionDetection.StringMatching
{
    public struct StringMatchingState
    {
        public StringMatchingState(AlgorithmState incompleteAlgorithmState, AlgorithmState completeAlgorithmState)
        {
            IncompleteAlgorithmState = incompleteAlgorithmState;
            CompleteAlgorithmState = completeAlgorithmState;
        }

        public readonly AlgorithmState CompleteAlgorithmState;

        public readonly AlgorithmState IncompleteAlgorithmState;
    }
}
