using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.MaximalSuffixes;

namespace RepetitionDetection.Tests
{
    public class MaximalSuffixCalculatorLessTests
    {
        [TestCase("ab", new []{0, 1})]
        [TestCase("ba", new []{0, 0})]
        [TestCase("abacaba", new []{0, 1, 1, 3, 3, 3, 3})]
        [TestCase("cabcaba", new []{0, 0, 0, 0, 0, 0, 0})]
        [TestCase("aaa", new []{0, 0, 0})]
        [TestCase("aabaca", new []{0, 0, 2, 2, 4, 4})]
        [TestCase("zzxzz", new []{0, 0, 0, 0, 0})]
        [TestCase("zzxzzz", new []{0, 0, 0, 0, 0, 3})]
        public void TestCase(string input, int[] expectedMaximalSuffixes)
        {
            var maximalSuffixCalculator = new MaximalSuffixCalculator(input, new CharLessComparer());
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