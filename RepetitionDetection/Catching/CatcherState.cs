using System.Collections.Immutable;
using RepetitionDetection.Commons;
using RepetitionDetection.StringMatching;

namespace RepetitionDetection.Catching
{
    public class CatcherState
    {
        public CatcherState(ImmutableArray<Repetition> repetitions, StringMatchingState stringMatchingState)
        {
            Repetitions = repetitions;
            StringMatchingState = stringMatchingState;
        }

        public StringMatchingState StringMatchingState { get; private set; }
        public ImmutableArray<Repetition> Repetitions { get; private set; }
    }
}
