using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.MaximalSuffixes;

namespace RepetitionDetection.Tests
{
    public class MaximalSuffixCalculatorGreaterTests
    {
        [TestCase("ab", new []{ 0, 0 })]
        [TestCase("ba", new []{ 0, 1 })]
        [TestCase("abacaba", new []{0, 0, 0, 0, 0, 0, 0})]
        [TestCase("cababaca", new []{0, 1, 1, 1, 1, 1, 1, 1})]
        [TestCase("aaabaa", new []{0, 0, 0, 0, 0, 0})]
        [TestCase("aaabaaaa", new []{0, 0, 0, 0, 0, 0, 0, 4})]
        [TestCase("zzzzxzzz", new []{0, 0, 0, 0, 4, 4, 4, 4 })]
        public void TestCase(string input, int[] expectedMaximalSuffixes)
        {
            var maximalSuffixCalculator = new MaximalSuffixCalculator(input, new CharGreaterComparer());
            var maximalSuffixes = new int[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                maximalSuffixCalculator.Calculate(i + 1);
                maximalSuffixes[i] = maximalSuffixCalculator.MaximalSuffixPosition;
            }
            Assert.That(maximalSuffixes, Is.EquivalentTo(expectedMaximalSuffixes));
        }
    }
}
