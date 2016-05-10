using System;
using System.Collections.Generic;
using NUnit.Framework;
using RepetitionDetection.MSCalculator;

namespace RepetitionDetection.Tests
{
    public class MaximalSuffixCalculatorLessTests
    {
        [TestCase("ab", 1)]
        [TestCase("ba", 0)]
        [TestCase("abacaba", 3)]
        [TestCase("cabcabcaba", 0)]
        [TestCase("aaaaa", 0)]
        [TestCase("aaabaaaa", 3)]
        [TestCase("zzzzxzzzz", 0)]
        [TestCase("zzzzxzzzzz", 5)]
        public void TestCase(string input, int expectedMaximalSuffix)
        {
            var maximalSuffixCalculator = new MaximalSuffixCalculator(input, Comparer<Char>.Default);
            maximalSuffixCalculator.Calculate(input.Length);
            Assert.That(maximalSuffixCalculator.MaximalSuffixPosition, Is.EqualTo(expectedMaximalSuffix));
        }
    }
}
