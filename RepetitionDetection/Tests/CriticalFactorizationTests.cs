using NUnit.Framework;
using RepetitionDetection.CriticalFactorization;

namespace RepetitionDetection.Tests
{
    [TestFixture]
    public class CriticalFactorizationTests
    {
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

        public void FactorizerTest(string pattern, int expectedPrefixLength, int expectedPrefixPosition, int expectedPatternPosition)
        {
            var factorization = Factorizer.GetFactorization(pattern);
            Assert.That(factorization, Is.EqualTo(new Factorization(pattern, expectedPrefixLength, expectedPrefixPosition, expectedPatternPosition)));
        }
    }
}
