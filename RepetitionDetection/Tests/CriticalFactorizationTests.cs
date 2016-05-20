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
            factorizer.Factorize(template.Length);
            Assert.That(factorizer.CriticalPosition, Is.EqualTo(expectedPosition));
        }

        [TestCase("aaa", 0, 0, 3, 1)]       // (a, aa)
        [TestCase("aba", 0, 0, 3, 1)]       // (a, ba)
        [TestCase("abaaaba", 3, 1, 7, 2)]   // (a, ba) (ab, aaaba)
        [TestCase("aaaba", 3, 1, 4, 3)]     // (a, aa) (aaa, ba)
        [TestCase("abcdefgh", 4, 3, 5, 4)]  // (abc, d) (abcd, efgh)
        [TestCase("abcabcdabc", 6, 2, 7, 6)]// (ab, cabc) (abcabc, dabc)
        public void FactorizerTest(string pattern, int prefixLength, int prefixCriticalPosition, int patternPrefixLength, int patternCriticalPosition)
        {
            var factorization = Factorizer.GetFactorization(pattern);
            Assert.That(factorization, Is.EqualTo(new Factorization(pattern, prefixLength, prefixCriticalPosition, patternCriticalPosition)));
        }
    }
}
