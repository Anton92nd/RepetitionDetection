using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;
using RepetitionDetection.TextGeneration;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace Tests.TextGenerationTests
{
    [TestFixture]
    public class RandomWordsGeneratorTests
    {
        [TestCase(4, 7, 4, true, typeof(RepetitionDetector))]
        [TestCase(4, 7, 4, false, typeof(RepetitionDetector))]
        [TestCase(4, 7, 4, true, typeof(SillyDetector))]
        [TestCase(4, 7, 4, false, typeof(SillyDetector))]
        public void TestDetector(int alphabet, int numerator, int denominator, bool detectEqual, Type detectorType)
        {
            var lengths = new[] {1000, 2000};
            var generator = new RandomCharGenerator(alphabet);
            var detector = GetDetector(detectorType, detectEqual, new RationalNumber(numerator, denominator));
            foreach (var length in lengths)
            {
                detector.Reset();
                var sw = Stopwatch.StartNew();
                RandomWordGenerator.Generate(detector, length, new RemoveBorderStrategy(), generator);
                sw.Stop();
                Console.WriteLine($"Length: {length}\n\tTime: {sw.ElapsedMilliseconds} ms\n\tConversion coeff: {RandomWordGenerator.Statistics.CharsGenerated * 1.0 / length:0.000000}");
            }
        }

        [TestCase(3, 7, 4, true, typeof(RepetitionDetector), 10)]
        public void Test(int alphabet, int numerator, int denominator, bool detectEqual, Type detectorType, int length)
        {
            var generator = new RandomCharGenerator(alphabet);
            var detector = GetDetector(detectorType, detectEqual, new RationalNumber(numerator, denominator));
            detector.Reset();
            var sw = Stopwatch.StartNew();
            RandomWordGenerator.Generate(detector, length, new RemoveBorderStrategy(), generator);
            sw.Stop();
            Console.WriteLine($"Length: {length}\n\tTime: {sw.ElapsedMilliseconds} ms\n\tConversion coeff: {RandomWordGenerator.Statistics.CharsGenerated * 1.0 / length:0.000000}");
        }

        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        public void TestBoundaryLanguages(int k)
        {
            var lengthsBoundary = Enumerable.Range(1, 10).Select(n => n * 100).ToArray();
            var strategy = new RemoveBorderStrategy();
            var e = new RationalNumber(k, k - 1);
            var detector = new RepetitionDetector(new StringBuilder(), e, false);
            var generator = new RandomCharGenerator(k);
            var charsGenerated = 0;
            var times = new List<long>();
            var coefs = new List<double>();
            var sw = Stopwatch.StartNew();
            foreach (var length in lengthsBoundary)
            {
                RandomWordGenerator.Generate(detector, length, strategy, generator);
                charsGenerated += RandomWordGenerator.Statistics.CharsGenerated;
                times.Add(sw.ElapsedMilliseconds);
                coefs.Add(charsGenerated * 1.0 / length);
            }
            sw.Stop();
            Console.WriteLine($"Alphabet size = {k}, e = {e}+");
            for (var i = 0; i < lengthsBoundary.Length; ++i)
                Console.WriteLine($"\tLength: {lengthsBoundary[i]}, Time: {times[i]} ms, Conversion coeff: {coefs[i]}");
        }

        private static Detector GetDetector(Type detectorType, bool detectEqual, RationalNumber e)
        {
            var text = new StringBuilder();
            Detector detector;
            if (detectorType == typeof(SillyDetector))
                detector = new SillyDetector(text, e, detectEqual);
            else if (detectorType == typeof(RepetitionDetector))
                detector = new RepetitionDetector(text, e, detectEqual);
            else
                throw new InvalidProgramStateException($"Wrong type of detector: {detectorType.FullName}");
            return detector;
        }
    }
}