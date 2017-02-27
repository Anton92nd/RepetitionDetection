using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.Periods;

namespace Tests
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
        public void TestPatternPeriod(string pattern, int expectedPeriod)
        {
            Assert.That(pattern.GetPeriod(pattern.Length), Is.EqualTo(expectedPeriod));
        }
    }
}
