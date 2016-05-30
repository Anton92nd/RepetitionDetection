namespace RepetitionDetection.StringMatching
{
    public struct StringMatchingState
    {
        public StringMatchingState(AlgorithmState incompleteAlgorithmState, AlgorithmState completeAlgorithmState)
        {
            IncompleteAlgorithmState = incompleteAlgorithmState;
            CompleteAlgorithmState = completeAlgorithmState;
        }

        public override string ToString()
        {
            return string.Format("Complete: ({0}), Incomplete: ({1})", CompleteAlgorithmState, IncompleteAlgorithmState);
        }

        public readonly AlgorithmState CompleteAlgorithmState;

        public readonly AlgorithmState IncompleteAlgorithmState;
    }
}
