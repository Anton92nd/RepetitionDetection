using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.CriticalFactorization;
using RepetitionDetection.Periods;
using RepetitionDetection.StringMatching;

namespace RepetitionDetection.Tests
{
    [TestFixture]
    public class CompleteStringMatchingAlgorithmTests
    {
        [TestCase("abacabadabacaba", "aba", 2, 6, 10, 14)]
        [TestCase("aaaaa", "aa", 1, 2, 3, 4)]
        [TestCase("ababababa", "aba", 2, 4, 6, 8)]
        public void TestFindOccurences(string text, string pattern, params int[] expectedOccurences)
        {
            var criticalPosition = Factorizer.GetFactorization(pattern).PatternCriticalPosition;
            var period = PeriodCalculator.GetPeriod(pattern, pattern.Length);

            var sb = new StringBuilder();
            var algorithm = new CompleteStringMatchingAlgorithm(sb, 0, pattern, pattern.Length, criticalPosition, period);

            var occurences = new List<int>();
            for (var i = 0; i < text.Length; ++i)
            {
                sb.Append(text[i]);
                if (algorithm.CheckMatch())
                {
                    occurences.Add(i);
                }
            }

            Assert.That(occurences, Is.EquivalentTo(expectedOccurences));
        }

        [Test]
        public void TestFailsOnBadFactorization()
        {
            var pattern = "abcabcdabc";
            var criticalPosition = Factorizer.GetFactorization(pattern).PatternCriticalPosition;
            var period = PeriodCalculator.GetPeriod(pattern, pattern.Length);
            Assert.That(() =>
            {
                new CompleteStringMatchingAlgorithm(new StringBuilder(), 0, pattern, pattern.Length, criticalPosition, period);
            }, Throws.InstanceOf<InvalidUsageException>());
        }
    }
}
