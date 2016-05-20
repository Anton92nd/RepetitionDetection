using NUnit.Framework;
using RepetitionDetection.CriticalFactorization;

namespace RepetitionDetection.Tests
{
    [TestFixture]
    public class CriticalFactorizationTests
    {
        [TestCase("aaa", 1)]
        [TestCase("aba", 1)]
        [TestCase("abaaaba", 2)]
        [TestCase("aaaba", 3)]
        [TestCase("abcdefgh", 7)]
        [TestCase("abcabcdabc", 6)]
        public void PrefixFactorizerTest(string template, int expectedPosition)
        {
            var factorizer = new PrefixFactorizer(template);
            Assert.That(factorizer.GetCriticalFactorization(template.Length).CriticalPosition, Is.EqualTo(expectedPosition));
        }

        [TestCase("aaa", 0, 0, 1, 2, 1, 1)]       // (a, a)
        [TestCase("aba", 0, 0, 1, 2, 1, 2)]       // (a, ba)
        [TestCase("abaaaba", 2, 1, 1, 4, 2, 4)]   // (a, b) (ab, aaaba)
        [TestCase("aaaba", 2, 1, 1, 4, 3, 2)]     // (a, a) (aaa, ba)
        [TestCase("abcdefgh", 4, 3, 1, 5, 4, 4)]  // (abc, d) (abcd, efgh)
        [TestCase("abcabcdabc", 3, 2, 1, 7, 6, 1)]// (ab, c) (abcabc, d)
        public void FactorizerTest(string pattern, int prefixLength, int prefixCriticalPosition, int prefixSuffixPeriod, int patternPrefixLength, int patternCriticalPosition, int patternSuffixPeriod)
        {
            var factorization = Factorizer.GetFactorizations(pattern);
            Assert.That(factorization, Is.EqualTo(new Factorization(pattern, new PrefixFactorization(prefixLength, prefixCriticalPosition, prefixSuffixPeriod), new PrefixFactorization(patternPrefixLength, patternCriticalPosition, patternSuffixPeriod))));
        }
    }
}
