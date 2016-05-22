﻿using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RepetitionDetection.CriticalFactorization;
using RepetitionDetection.Periods;
using RepetitionDetection.StringMatching;

namespace RepetitionDetection.Tests
{
    [TestFixture]
    public class IncompleteStringMatchingAlgorithmTests
    {
        [TestCase("abacabadabacababbba", "aba", new []{2, 6, 10, 14, 18})]
        [TestCase("aaaaa", "aa", new []{1, 2, 3, 4})]
        [TestCase("abababababbba", "aba", new []{2, 4, 6, 8, 12})]
        [TestCase("aaababaababababaaaaabaabaaaaaabab", "aaabab", new []{5, 12, 32}, Description = "Not actual second occurence")]
        public void TestFindOccurences(string text, string pattern, int[] expectedOccurences)
        {
            var criticalPosition = Factorizer.GetFactorization(pattern).PatternCriticalPosition;
            var period = PeriodCalculator.GetPeriod(pattern, pattern.Length);

            var sb = new StringBuilder();
            var algorithm = new IncompleteStringMatchingAlgorithm(sb, 0, pattern, pattern.Length, criticalPosition, period);

            var occurences = new List<int>();
            for (var i = 0; i < text.Length; ++i)
            {
                sb.Append(text[i]);
                if (algorithm.CheckMatch(sb.Length))
                {
                    occurences.Add(i);
                }
            }

            Assert.That(occurences, Is.EquivalentTo(expectedOccurences));
        }
    }
}
