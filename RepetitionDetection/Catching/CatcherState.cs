using System.Collections.Generic;
using JetBrains.Annotations;
using RepetitionDetection.Commons;
using RepetitionDetection.StringMatching;

namespace RepetitionDetection.Catching
{
    public class CatcherState
    {
        public CatcherState([NotNull] List<Repetition> repetitions, StringMatchingState stringMatchingState)
        {
            Repetitions = repetitions;
            StringMatchingState = stringMatchingState;
        }

        public StringMatchingState StringMatchingState { get; }

        public List<Repetition> Repetitions { get; }
    }
}