﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RepetitionDetection.StringMatching;

namespace Tests.StringMatchingTests
{
    [TestFixture]
    public class StringMatchingAlgoPerfomanceTests
    {
        [TestCase(3, "abc")]
        [TestCase(3, "aaaabc")]
        [TestCase(2, "aaaaab")]
        [TestCase(3, "aaaabcaaaabc")]
        [TestCase(3, "aaaabcaaaabcaaaabccaaabc")]
        [TestCase(26, "abcdefghijklmnopqrstuvwxyz")]
        public void RandomTest(int alphabet, string pattern)
        {
            var occurences = new List<int>();
            var milliseconds = new List<long>();
            var rnd = new Random();
            foreach (var textLength in TextLengths)
            {
                var sb = new StringBuilder();
                var algo = new StringMatchingAlgorithm(sb, pattern, 0);
                var sw = Stopwatch.StartNew();
                var occ = 0;
                for (var i = 0; i < textLength; ++i)
                {
                    sb.Append((char) ('a' + rnd.Next() % alphabet));
                    if (algo.CheckForMatch(i + 1))
                        occ++;
                }
                sw.Stop();
                occurences.Add(occ);
                milliseconds.Add(sw.ElapsedMilliseconds);
            }
            for (var i = 0; i < TextLengths.Length; ++i)
                Console.WriteLine("Text length: {0}, Occurences count: {1}, Elapsed ms: {2}",
                    TextLengths[i], occurences[i], milliseconds[i]);
        }

        [Test]
        public void Tests()
        {
            for (var i = 1; i <= TestsCount; ++i)
            {
                var testNumber = string.Empty + (char) ('0' + i / 10) + (char) ('0' + i % 10);
                var inputFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "Files", testNumber + ".tst");
                var outputFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "Files", testNumber + ".ans");
                DoTest(inputFile, outputFile);
            }
        }

        private const int TestsCount = 21;

        private static void DoTest(string inputFile, string outputFile)
        {
            var outputData = File.ReadAllLines(outputFile);
            var result = outputData[0] == "0"
                ? new int[] { }
                : outputData[1]
                    .Split(null as char[], StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
            Console.WriteLine("Testing file: {0}", inputFile);
            var inputData = File.ReadAllLines(inputFile);
            var sb = new StringBuilder();
            var algo = new StringMatchingAlgorithm(sb, inputData[1], 0);
            var occurences = new List<int>();
            var sw = Stopwatch.StartNew();
            for (var i = 0; i < inputData[0].Length; ++i)
            {
                sb.Append(inputData[0][i]);
                if (algo.CheckForMatch(i + 1))
                    occurences.Add(i - inputData[1].Length + 2);
            }
            sw.Stop();
            Assert.That(occurences, Is.EquivalentTo(result));
            Console.WriteLine(
                "\tText length: {0}\n\tPattern length: {1}\n\tOccurences found: {2}\n\tElapsed milliseconds: {3}\n",
                inputData[0].Length, inputData[1].Length, occurences.Count, sw.ElapsedMilliseconds);
        }

        private static readonly int[] TextLengths = {1000, 10000, 100000, 1000000, 10000000};
    }
}