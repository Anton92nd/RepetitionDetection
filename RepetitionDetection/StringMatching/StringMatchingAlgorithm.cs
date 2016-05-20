using System.Text;
using RepetitionDetection.CriticalFactorization;

namespace RepetitionDetection.StringMatching
{
    public class StringMatchingAlgorithm : IStringMatchingAlgorithm
    {
        public StringMatchingAlgorithm(StringBuilder text, string pattern)
        {
            var factorizations = Factorizer.GetFactorizations(pattern);

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
