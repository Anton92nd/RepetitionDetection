using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.CriticalFactorization;
using RepetitionDetection.Periods;

namespace RepetitionDetection.StringMatching
{
    public class StringMatchingAlgorithm
    {
        public StringMatchingAlgorithm([NotNull] StringBuilder text, [NotNull] string pattern, int startPosition)
        {
            this.text = text;
            var factorizations = Factorizer.GetFactorization(pattern);
            if (factorizations.PatternCriticalPosition > pattern.Length/2)
            {
                shift = pattern.Length - factorizations.PrefixLength;
                incompleteAlgorithm = new IncompleteStringMatchingAlgorithm(text, startPosition, pattern, pattern.Length,
                    factorizations.PatternCriticalPosition, PeriodCalculator.GetPeriod(pattern, pattern.Length));
                completeAlgorithm = new CompleteStringMatchingAlgorithm(text, startPosition, pattern, factorizations.PrefixLength,
                    factorizations.PrefixCriticalPosition, PeriodCalculator.GetPeriod(pattern, factorizations.PrefixLength));
            }
            else
            {
                shift = 0;
                incompleteAlgorithm = null;
                completeAlgorithm = new CompleteStringMatchingAlgorithm(text, startPosition, pattern, pattern.Length,
                    factorizations.PatternCriticalPosition, PeriodCalculator.GetPeriod(pattern, pattern.Length));
            }
        }

        public bool CheckForMatch()
        {
            if (incompleteAlgorithm == null)
            {
                return completeAlgorithm.CheckMatch(text.Length);
            }
            var incompleteAlgoResult = incompleteAlgorithm.CheckMatch(text.Length);
            var completeAlgoResult = completeAlgorithm.CheckMatch(text.Length - shift);
            return incompleteAlgoResult && completeAlgoResult;
        }

        public StringMatchingState State
        {
            get
            {
                return new StringMatchingState(incompleteAlgorithm == null ? new AlgorithmState(-1, -1) : incompleteAlgorithm.State, completeAlgorithm.State);
            }
        }

        public void SetState(StringMatchingState state)
        {
            if (incompleteAlgorithm != null)
                incompleteAlgorithm.SetState(state.IncompleteAlgorithmState);
            completeAlgorithm.SetState(state.CompleteAlgorithmState);
        }

        [NotNull]
        private readonly IStringMatchingAlgorithm completeAlgorithm;

        [CanBeNull]
        private readonly IStringMatchingAlgorithm incompleteAlgorithm;

        private readonly int shift;

        [NotNull]
        private readonly StringBuilder text;
    }
}
