﻿using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.CriticalFactorization;
using RepetitionDetection.Periods;

namespace RepetitionDetection.StringMatching
{
    public class StringMatchingAlgorithm
    {
        public StringMatchingAlgorithm([NotNull] StringBuilder text, [NotNull] string pattern, int startPosition)
        {
            var factorizations = Factorizer.GetFactorization(pattern);
            if (factorizations.PatternCriticalPosition > pattern.Length/2)
            {
                shift = pattern.Length - factorizations.PrefixLength;
                incompleteAlgorithm = new SuffixStringMatchingAlgorithm(text, pattern, startPosition, pattern.Length,
                    factorizations.PatternCriticalPosition, PeriodCalculator.GetPeriod(pattern, pattern.Length));
                completeAlgorithm = new GoodFactorizationStringMatchingAlgorithm(text, startPosition, pattern, factorizations.PrefixLength,
                    factorizations.PrefixCriticalPosition, PeriodCalculator.GetPeriod(pattern, factorizations.PrefixLength));
            }
            else
            {
                shift = 0;
                incompleteAlgorithm = null;
                completeAlgorithm = new GoodFactorizationStringMatchingAlgorithm(text, startPosition, pattern, pattern.Length,
                    factorizations.PatternCriticalPosition, PeriodCalculator.GetPeriod(pattern, pattern.Length));
            }
        }

        public bool CheckForMatch(int textLength)
        {
            if (incompleteAlgorithm == null)
            {
                return completeAlgorithm.CheckMatch(textLength);
            }
            var incompleteAlgoResult = incompleteAlgorithm.CheckMatch(textLength);
            var completeAlgoResult = completeAlgorithm.CheckMatch(textLength - shift);
            return incompleteAlgoResult && completeAlgoResult;
        }

        public StringMatchingState State
        {
            get
            {
                return new StringMatchingState(incompleteAlgorithm == null ? new AlgorithmState(-1, -1) : incompleteAlgorithm.State, completeAlgorithm.State);
            }

            set
            {
                if (incompleteAlgorithm != null)
                    incompleteAlgorithm.State = value.IncompleteAlgorithmState;
                completeAlgorithm.State = value.CompleteAlgorithmState;
            }
        }

        [NotNull]
        private readonly IPartialStringMatchingAlgorithm completeAlgorithm;

        [CanBeNull]
        private readonly IPartialStringMatchingAlgorithm incompleteAlgorithm;

        private readonly int shift;
    }
}
