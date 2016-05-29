using System.Collections.Immutable;
using JetBrains.Annotations;
using RepetitionDetection.Commons;
using RepetitionDetection.StringMatching;

namespace RepetitionDetection.Catching
{
    public class CatcherState
    {
        public CatcherState(ImmutableArray<Repetition> repetitions, [NotNull] StringMatchingState stringMatchingState)
        {
            Repetitions = repetitions;
            StringMatchingState = stringMatchingState;
        }

        [NotNull]
        public StringMatchingState StringMatchingState { get; private set; }

        public ImmutableArray<Repetition> Repetitions { get; private set; }
    }
}
