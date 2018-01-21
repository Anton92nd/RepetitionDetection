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
            var factorizations = pattern.GetFactorizations();
            if (factorizations.PatternFactorizationIsGood())
            {
                shift = 0;
                incompleteAlgorithm = null;
                completeAlgorithm = new GoodFactorizationStringMatchingAlgorithm(text, startPosition, pattern,
                    pattern.Length,
                    factorizations.PatternCriticalPosition, PeriodCalculator.GetPeriod(pattern, pattern.Length));
            }
            else
            {
                shift = pattern.Length - factorizations.PrefixLength;
                incompleteAlgorithm = new SuffixStringMatchingAlgorithm(text, pattern, startPosition, pattern.Length,
                    factorizations.PatternCriticalPosition, PeriodCalculator.GetPeriod(pattern, pattern.Length));
                completeAlgorithm = new GoodFactorizationStringMatchingAlgorithm(text, startPosition, pattern,
                    factorizations.PrefixLength,
                    factorizations.PrefixCriticalPosition,
                    PeriodCalculator.GetPeriod(pattern, factorizations.PrefixLength));
            }
        }

        public bool CheckForMatch(int textLength)
        {
            if (incompleteAlgorithm == null)
                return completeAlgorithm.CheckMatch(textLength);
            var incompleteAlgoResult = incompleteAlgorithm.CheckMatch(textLength);
            var completeAlgoResult = completeAlgorithm.CheckMatch(textLength - shift);
            return incompleteAlgoResult && completeAlgoResult;
        }

        public StringMatchingState State
        {
            get => new StringMatchingState(
                incompleteAlgorithm?.State ?? new AlgorithmState(-1, -1),
                completeAlgorithm.State);

            set
            {
                if (incompleteAlgorithm != null)
                    incompleteAlgorithm.State = value.IncompleteAlgorithmState;
                completeAlgorithm.State = value.CompleteAlgorithmState;
            }
        }

        [NotNull] private readonly IPartialStringMatchingAlgorithm completeAlgorithm;

        [CanBeNull] private readonly IPartialStringMatchingAlgorithm incompleteAlgorithm;

        private readonly int shift;
    }
}