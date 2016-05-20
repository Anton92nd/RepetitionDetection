using NUnit.Framework;
using RepetitionDetection.Periods;

namespace RepetitionDetection.Tests
{
    [TestFixture]
    public class PeriodCalculatorTests
    {
        [TestCase("abacaba", 4)]
        [TestCase("abababab", 2)]
        [TestCase("ddddd", 1)]
        [TestCase("abcdef", 6)]
        [TestCase("bacbac", 3)]
        [TestCase("abaabc", 6)]
        public void TestPatternPeriod(string template, int expectedPeriod)
        {
            Assert.That(PeriodCalculator.GetPeriod(template, template.Length), Is.EqualTo(expectedPeriod));
        }
    }
}
