using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.TextGeneration;

namespace RepetitionDetection.Tests.TextGenerationTests
{
    [TestFixture]
    public class AllWordsTests
    {
        public static void Main()
        {
        }

        [TestCase(3, 7, 5, new [] { "abca", "acba", "bacb", "bcab", "cabc", "cbac" })]
        [TestCase(4, 7, 6, new []
        {
            "abcd", "abdc", "acbd", "acdb", "adbc", "adcb", 
            "bacd", "badc", "bcad", "bcda", "bdac", "bdca",
            "cabd", "cadb", "cbad", "cbda", "cdab", "cdba",
            "dabc", "dacb", "dbac", "dbca", "dcab", "dcba"
        })]
        public void SmallTests(int alphabet, int numerator, int denominator, string[] expectedWords)
        {
            var result = AllWordsGenerator.Generate(alphabet, new RationalNumber(numerator, denominator), true);
            Assert.That(result, Is.EquivalentTo(expectedWords));
        }

        [TestCase(3, 7, 4, true, "sygma3.txt")]
        [TestCase(4, 7, 5, true, "sygma4.txt")]
        public void LargeTests(int alphabet, int numerator, int denominator, bool detectEqual, string fileName)
        {
            var resultPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Tests", "Files", fileName);
            var expectedStrings = File.ReadAllLines(resultPath);
            var sw = Stopwatch.StartNew();
            var result = AllWordsGenerator.Generate(alphabet, new RationalNumber(numerator, denominator), detectEqual);
            sw.Stop();
            Assert.That(result, Is.EquivalentTo(expectedStrings));
            Console.WriteLine("Time: {0} ms", sw.ElapsedMilliseconds);
        }
    }
}
