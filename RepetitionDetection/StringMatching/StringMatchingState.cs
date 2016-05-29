using JetBrains.Annotations;

namespace RepetitionDetection.StringMatching
{
    public class StringMatchingState
    {
        public StringMatchingState([NotNull] AlgorithmState incompleteAlgorithmState, [NotNull] AlgorithmState completeAlgorithmState)
        {
            IncompleteAlgorithmState = incompleteAlgorithmState;
            CompleteAlgorithmState = completeAlgorithmState;
        }

        [NotNull]
        public AlgorithmState CompleteAlgorithmState { get; private set; }

        [NotNull]
        public AlgorithmState IncompleteAlgorithmState { get; private set; }
    }
}
