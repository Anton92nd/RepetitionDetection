using System.Text;
using RepetitionDetection.CriticalFactorization;

namespace RepetitionDetection.StringMatching
{
    public class StringMatchingAlgorithm : IStringMatchingAlgorithm
    {
        private readonly int patternPeriod, prefixPeriod;

        public StringMatchingAlgorithm(StringBuilder text, string pattern)
        {
            var prefixFactorizer = new PrefixFactorizer(pattern);
            int currentPosition = -1, lastPosition = -1, prefixLength = -1;
            for (var position = 0; position < pattern.Length; ++position)
            {
                prefixLength = position;
                lastPosition = currentPosition;
                currentPosition = prefixFactorizer.GetCriticalFactorizationPosition(position + 1);
                if (currentPosition > (pattern.Length + 1)/2)
                {
                    break;
                }
            }
        }

        public bool CheckMatch()
        {
            throw new System.NotImplementedException();
        }

        public void SetState(AlgorithmState state)
        {
            throw new System.NotImplementedException();
        }

        public AlgorithmState State { get; private set; }
    }
}
