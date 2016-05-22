using System.Text;
using RepetitionDetection.CriticalFactorization;

namespace RepetitionDetection.StringMatching
{
    public class StringMatchingAlgorithm
    {
        public StringMatchingAlgorithm(StringBuilder text, string pattern)
        {
            var factorizations = Factorizer.GetFactorization(pattern);
        }

        public AlgorithmState State { get; private set; }
    }
}
