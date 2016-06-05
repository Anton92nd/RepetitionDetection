using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;
using RepetitionDetection.TextGeneration;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace RepetitionDetection.Tests.TextGenerationTests
{
    [TestFixture]
    public class RandomWordsGeneratorTests
    {
        private static readonly int[] Lengths = { 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000 };

        [TestCase(4, 7, 4, true, typeof(RepetitionDetector))]
        [TestCase(4, 7, 4, false, typeof(RepetitionDetector))]
        [TestCase(4, 7, 4, true, typeof(SillyDetector))]
        [TestCase(4, 7, 4, false, typeof(SillyDetector))]
        public void TestDetector(int alphabet, int numerator, int denominator, bool detectEqual, Type detectorType)
        {
            var detector = GetDetector(detectorType, detectEqual, new RationalNumber(numerator, denominator));
            foreach (var length in Lengths)
            {
                detector.Reset();
                var sw = Stopwatch.StartNew();
                RandomWordGenerator.Generate(detector, alphabet, length, new RemoveBorderStrategy());
                sw.Stop();
                Console.WriteLine("Length: {0}\n\tTime: {1} ms\n\tConversion coeff: {2:0.000000}", length, sw.ElapsedMilliseconds, RandomWordGenerator.CharsGenerated * 1.0 / length);
            }
        }

        [TestCase(3, 7, 4, true, typeof(RepetitionDetector), 10)]
        public void Test(int alphabet, int numerator, int denominator, bool detectEqual, Type detectorType, int length)
        {
            var detector = GetDetector(detectorType, detectEqual, new RationalNumber(numerator, denominator));
            detector.Reset();
            var sw = Stopwatch.StartNew();
            RandomWordGenerator.Generate(detector, alphabet, length, new RemoveBorderStrategy());
            sw.Stop();
            Console.WriteLine("Length: {0}\n\tTime: {1} ms\n\tConversion coeff: {2:0.000000}", length, sw.ElapsedMilliseconds, RandomWordGenerator.CharsGenerated * 1.0 / length);
        }

        private static readonly int[] LengthsBoundary = Enumerable.Range(1, 10).Select(n => n * 1000).ToArray();

        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        public void TestBoundaryLanguages(int k)
        {
            var strategy = new RemoveBorderStrategy();
            var e = new RationalNumber(k, k - 1);
            var detector = new RepetitionDetector(new StringBuilder(), e, false);
            var charsGenerated = 0;
            var times = new List<long>();
            var coefs = new List<double>();
            var sw = Stopwatch.StartNew();
            foreach (var length in LengthsBoundary)
            {
                RandomWordGenerator.Generate(detector, k, length, strategy);
                charsGenerated += RandomWordGenerator.CharsGenerated;
                times.Add(sw.ElapsedMilliseconds);
                coefs.Add(charsGenerated * 1.0 / length);
            }
            sw.Stop();
            Console.WriteLine("Alphabet size = {0}, e = {1}+", k, e);
            for (var i = 0; i < LengthsBoundary.Length; ++i)
            {
                Console.WriteLine("\tLength: {0}, Time: {1} ms, Conversion coeff: {2}", LengthsBoundary[i], times[i], coefs[i]);
            }
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
                throw new InvalidUsageException(string.Format("Wrong type of detector: {0}", detectorType.FullName));
            }
            return detector;
        }
    }
}
