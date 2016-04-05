using System;
using System.Collections.Generic;

using NUnit.Framework;
using RepetitionDetection.MSCalculator;

namespace RepetitionDetection.Tests
{
    public class MaximalSuffixCalculatorGreaterTests : MaximalSuffixCalculatorTestBase
    {
        private class CharGreaterComparer : IComparer<Char>
        {
            public int Compare(char x, char y)
            {
                return Comparer<Char>.Default.Compare(y, x);
            }
        }

        [SetUp]
        public override void SetUp()
        {
            maximalSuffixCalculator = new MaximalSuffixCalculator(new CharGreaterComparer());
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
            var data = MSCalculatorData.Default.WithStringLength(input.Length);
            maximalSuffixCalculator.Calculate(input, data);
            Assert.That(data.MSPosition, Is.EqualTo(expectedMaximalSuffix));
        }
    }
}
