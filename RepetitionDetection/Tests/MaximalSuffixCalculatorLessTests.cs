using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RepetitionDetection.MSCalculator;

namespace RepetitionDetection.Tests
{
    public class MaximalSuffixCalculatorLessTests : MaximalSuffixCalculatorTestBase
    {
        [SetUp]
        public override void SetUp()
        {
            maximalSuffixCalculator = new MaximalSuffixCalculator(Comparer<Char>.Default);
        }

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
            var sb = new StringBuilder(input);
            var data = MSCalculatorData.Default.WithStringLength(input.Length);
            var result = maximalSuffixCalculator.Calculate(sb, data);
            Assert.That(result.MSPosition, Is.EqualTo(expectedMaximalSuffix));
        }
    }
}
