namespace RepetitionDetection.StringMatching
{
    public class StringMatchingState
    {
        public StringMatchingState(AlgorithmState incompleteAlgorithmState, AlgorithmState completeAlgorithmState)
        {
            IncompleteAlgorithmState = incompleteAlgorithmState;
            CompleteAlgorithmState = completeAlgorithmState;
        }

        public AlgorithmState CompleteAlgorithmState { get; private set; }
        public AlgorithmState IncompleteAlgorithmState { get; private set; }
    }
}
