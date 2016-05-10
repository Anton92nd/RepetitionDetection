using System;
using System.Collections.Generic;
using NUnit.Framework;
using RepetitionDetection.MaximalSuffixes;

namespace RepetitionDetection.Tests
{
    public class MaximalSuffixCalculatorGreaterTests
    {
        private class CharGreaterComparer : IComparer<Char>
        {
            public int Compare(char x, char y)
            {
                return Comparer<Char>.Default.Compare(y, x);
            }
        }

        [TestCase("ab", 0)]
        [TestCase("ba", 1)]
        [TestCase("abacaba", 0)]
        [TestCase("cababaca", 1)]
        [TestCase("aaaaa", 0)]
        [TestCase("aaaaabaaaa", 0)]
        [TestCase("aaabaaaa", 4)]
        [TestCase("zzzzxzzz", 4)]
        public void TestCase(string input, int expectedMaximalSuffix)
        {
            var maximalSuffixCalculator = new MaximalSuffixCalculator(input, new CharGreaterComparer());
            maximalSuffixCalculator.Calculate(input.Length);
            Assert.That(maximalSuffixCalculator.MaximalSuffixPosition, Is.EqualTo(expectedMaximalSuffix));
        }
    }
}
