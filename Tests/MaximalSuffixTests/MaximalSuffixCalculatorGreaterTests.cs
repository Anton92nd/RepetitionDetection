using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.MaximalSuffixes;

namespace Tests.MaximalSuffixTests
{
    public class MaximalSuffixCalculatorGreaterTests
    {
        [TestCase("aaa", new[] { 0, 0, 0 })]
        [TestCase("ab", new []{ 0, 0 })]
        [TestCase("ba", new []{ 0, 1 })]
        [TestCase("abacaba", new []{0, 0, 0, 0, 0, 0, 0})]
        [TestCase("cababaca", new []{0, 1, 1, 1, 1, 1, 1, 1})]
        [TestCase("aaabaa", new []{0, 0, 0, 0, 0, 0})]
        [TestCase("aaabaaaa", new []{0, 0, 0, 0, 0, 0, 0, 4})]
        [TestCase("zzzzxzzz", new []{0, 0, 0, 0, 4, 4, 4, 4 })]
        [TestCase("cbcbcd", new []{0, 1, 1, 1, 1, 1})]
        [TestCase("cbcbcbd", new []{0, 1, 1, 1, 1, 1, 1})]
        [TestCase("cbcbca", new []{0, 1, 1, 1, 1, 5})]
        [TestCase("cbcbcba", new []{0, 1, 1, 1, 1, 1, 6})]
        public void TestCase(string pattern, int[] expectedMaximalSuffixes)
        {
            var maximalSuffixCalculator = new MaximalSuffixCalculator(pattern, new CharGreaterComparer());
            var maximalSuffixes = new int[pattern.Length];
            for (var i = 0; i < pattern.Length; i++)
            {
                maximalSuffixCalculator.Calculate(i + 1);
                maximalSuffixes[i] = maximalSuffixCalculator.MaximalSuffixPosition;
            }
            Assert.That(maximalSuffixes, Is.EquivalentTo(expectedMaximalSuffixes));
        }
    }
}
