using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.CriticalFactorization;
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
            var sb = new StringBuilder();
            var algorithm = new CompleteStringMatchingAlgorithm(sb, pattern, 0, new PrefixFactorization(pattern.Length, Factorizer.GetFactorizations(pattern).PatternFactorization.CriticalPosition));

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
            Assert.That(() =>
            {
                var pattern = "abcabcdabc";
                var algorithm = new CompleteStringMatchingAlgorithm(new StringBuilder(), pattern, 0,
                    new PrefixFactorization(pattern.Length,
                        Factorizer.GetFactorizations(pattern).PatternFactorization.CriticalPosition));
            }, Throws.InstanceOf<InvalidUsageException>());
        }
    }
}
