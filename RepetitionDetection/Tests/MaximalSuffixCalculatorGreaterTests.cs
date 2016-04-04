using System;
using System.Collections.Generic;

using NUnit.Framework;

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
        public void TestCase(string input, int expectedMaximalSuffix)
        {
            foreach (var c in input)
            {
                maximalSuffixCalculator.Append(c);
            }
            Assert.That(maximalSuffixCalculator.GetPosition(), Is.EqualTo(expectedMaximalSuffix));
        }
    }
}
