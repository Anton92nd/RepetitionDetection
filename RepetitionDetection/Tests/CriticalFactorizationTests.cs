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
            Assert.That(factorizer.GetCriticalFactorizationPosition(template.Length), Is.EqualTo(expectedPosition));
        }

        [TestCase("aaa", 0, 0, 1)]       // (a, aa)
        [TestCase("aba", 0, 0, 1)]       // (a, ba)
        [TestCase("abaaaba", 2, 1, 2)]   // (a, b) (ab, aaaba)
        [TestCase("aaaba", 2, 1, 3)]     // (a, a) (aaa, ba)
        [TestCase("abcdefgh", 4, 3, 4)]  // (abc, d) (abcd, efgh)
        [TestCase("abcabcdabc", 3, 2, 6)]// (ab, c) (abcabc, dabc)
        public void FactorizerTest(string pattern, int expectedPrefixLength, int expectedPrefixCriticalPosition, int expectedPatternCriticalPosition)
        {
            var factorization = Factorizer.GetFactorization(pattern);
            Assert.That(factorization, Is.EqualTo(new Factorization(pattern, expectedPrefixLength, expectedPrefixCriticalPosition, expectedPatternCriticalPosition)));
        }
    }
}
