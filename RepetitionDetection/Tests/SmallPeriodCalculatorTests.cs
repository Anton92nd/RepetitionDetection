using NUnit.Framework;
using RepetitionDetection.CriticalFactorization;
using RepetitionDetection.Periods;

namespace RepetitionDetection.Tests
{
    [TestFixture]
    public class SmallPeriodCalculatorTests
    {
        [TestCase("abacaba", 4)]
        [TestCase("abababab", 2)]
        public void TestPatternPeriod(string template, int expectedPeriod)
        {
            var factorizations = Factorizer.GetFactorizations(template);
            Assert.That(SmallPeriodCalculator.GetPeriod(template, factorizations.PatternFactorization), Is.EqualTo(expectedPeriod));
        }
    }
}
