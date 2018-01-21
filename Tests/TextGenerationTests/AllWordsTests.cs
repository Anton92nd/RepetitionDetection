using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;
using RepetitionDetection.TextGeneration;

namespace Tests.TextGenerationTests
{
    [TestFixture]
    public class AllWordsTests
    {
        [TestCase(3, 7, 5, true, new [] { "abca", "acba", "bacb", "bcab", "cabc", "cbac" }, typeof(SillyDetector))]
        [TestCase(3, 7, 5, true ,new [] { "abca", "acba", "bacb", "bcab", "cabc", "cbac" }, typeof(RepetitionDetector))]
        [TestCase(4, 7, 6, true, new[]
        {
            "abcd", "abdc", "acbd", "acdb", "adbc", "adcb", 
            "bacd", "badc", "bcad", "bcda", "bdac", "bdca",
            "cabd", "cadb", "cbad", "cbda", "cdab", "cdba",
            "dabc", "dacb", "dbac", "dbca", "dcab", "dcba"
        }, typeof(SillyDetector))]
        [TestCase(4, 7, 6, true, new []
        {
            "abcd", "abdc", "acbd", "acdb", "adbc", "adcb", 
            "bacd", "badc", "bcad", "bcda", "bdac", "bdca",
            "cabd", "cadb", "cbad", "cbda", "cdab", "cdba",
            "dabc", "dacb", "dbac", "dbca", "dcab", "dcba"
        }, typeof(RepetitionDetector))]
        public void SmallTests(int alphabet, int numerator, int denominator, bool detectEqual, string[] expectedWords, Type detectorType)
        {
            var e = new RationalNumber(numerator, denominator);
            var detector = GetDetector(detectorType, detectEqual, e);
            var result = AllWordsGenerator.Generate(detector, alphabet);
            Assert.That(result, Is.EquivalentTo(expectedWords));
        }

        private static Detector GetDetector(Type detectorType, bool detectEqual, RationalNumber e)
        {
            var text = new StringBuilder();
            Detector detector;
            if (detectorType == typeof(SillyDetector))
            {
                detector = new SillyDetector(text, e, detectEqual);
            }
            else if (detectorType == typeof(RepetitionDetector))
            {
                detector = new RepetitionDetector(text, e, detectEqual);
            }
            else
            {
                throw new InvalidProgramStateException($"Wrong type of detector: {detectorType.FullName}");
            }
            return detector;
        }

        [TestCase(3, 7, 4, true, "sygma3.txt", typeof(SillyDetector))]
        [TestCase(3, 7, 4, true, "sygma3.txt", typeof(RepetitionDetector))]
        [TestCase(4, 7, 5, true, "sygma4.txt", typeof(SillyDetector))]
        [TestCase(4, 7, 5, true, "sygma4.txt", typeof(RepetitionDetector))]
        [TestCase(4, 4, 3, false, "sygma4+.txt", typeof(SillyDetector))]
        [TestCase(4, 4, 3, false, "sygma4+.txt", typeof(RepetitionDetector))]
        public void LargeTests(int alphabet, int numerator, int denominator, bool detectEqual, string fileName, Type detectorType)
        {
            var resultPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Files", fileName);
            var expectedStrings = File.ReadAllLines(resultPath);
            var e = new RationalNumber(numerator, denominator);
            var detector = GetDetector(detectorType, detectEqual, e);
            var sw = Stopwatch.StartNew();
            var result = AllWordsGenerator.Generate(detector, alphabet);
            sw.Stop();
            Assert.That(result, Is.EquivalentTo(expectedStrings));
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");
        }
    }
}
