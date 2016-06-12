using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;
using RepetitionDetection.Logging;
using RepetitionDetection.TextGeneration;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace RepetitionDetection
{
    public static class Program
    {

        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            //GenerateBoundaryUniform();
            //GenerateBoundaryBits();
            //GenerateSquare();
        }

        private static void GenerateSquare()
        {
            var Runs = new[] {100};
            var lengths = new[] {100000};
            using (var outputStream = File.Open("D:\\statistics\\square4.txt", FileMode.Create))
            using (var output = new StreamWriter(outputStream))
            {
                for (var i = 0; i < Runs.Length; ++i)
                {
                    var logger = new ConsoleTextLengthLogger(1000);
                    var text = new StringBuilder();

                    var e = new RationalNumber(2);
                    var generator = new RandomNotLastCharGenerator(text, 4);
                    var detector = new RepetitionDetector(text, e, true);
                    var removeStrategy = new RemoveBorderStrategy();

                    Calculate(Runs[i], detector, lengths[i], removeStrategy, generator, logger, output);
                }
            }
        }

        private static void Generate5_7d5_plus()
        {
            using (var outputStream = File.Open("D:\\statistics\\binary_5_7d5+.txt", FileMode.Create))
            using (var output = new StreamWriter(outputStream))
            {
                var Runs = new[] {300, 300, 200, 200, 100, 100, 100, 50};
                var lengths = new[] {100, 200, 300, 400, 500, 600, 700, 800};

                for (var i = 0; i < Runs.Length; ++i)
                {
                    var length = lengths[i];
                    Console.WriteLine("Calculating conversion for length = {0}", length);
                    var text = new StringBuilder();


                    var e = new RationalNumber(7, 5);
                    var generator = new BinaryCharGenerator(text, 5);
                    var detector = new RepetitionDetector(text, e, false);
                    var removeStrategy = new RemoveBorderStrategy();


                    Calculate(Runs[i], detector, lengths[i], removeStrategy, generator, null, output);
                }
            }
        }

        private static void Generate4_7d4_plus()
        {
            using (var outputStream = File.Open("D:\\statistics\\binary_4_7d4+.txt", FileMode.Create))
            using (var output = new StreamWriter(outputStream))
            {
                var Runs = new[] { 100, 100, 100, 50, 50 };
                var lengths = new[] { 100, 200, 300, 400, 500 };

                for (var i = 0; i < Runs.Length; ++i)
                {
                    var length = lengths[i];
                    Console.WriteLine("Calculating conversion for length = {0}", length);
                    var text = new StringBuilder();
                    var logger = new ConsoleTextLengthLogger(1000);


                    var e = new RationalNumber(7, 4);
                    var generator = new BinaryCharGenerator(text, 4);
                    var detector = new RepetitionDetector(text, e, false);
                    var removeStrategy = new RemoveBorderStrategy();

                    Calculate(Runs[i], detector, lengths[i], removeStrategy, generator, logger, output);
                }
            }
        }

        private static void GenerateBoundaryUniform()
        {
            const int length = 100000;
            const int runsCount = 100;
            using (var outputStream = File.Open(string.Format("D:\\statistics\\boundary_uniform_{0}.txt", length), FileMode.Create))
            using (var output = new StreamWriter(outputStream))
            {
                output.WriteLine("k | coeff");

                var removeStrategy = new RemoveBorderStrategy();
                var logger = new ConsoleTextLengthLogger(length/100);
                for (var k = 5; k <= 10; ++k)
                {
                    Console.WriteLine("Conversion for k = {0}", k);
                    var text = new StringBuilder();

                    var e = new RationalNumber(k, k - 1);
                    var generator = new RandomCharGenerator(k);
                    var detector = new RepetitionDetector(text, e, false);

                    Calculate(runsCount, detector, length, removeStrategy, generator, logger, output);
                }
            }
        }

        private static void GenerateBoundaryBits()
        {
            const int length = 100000;
            const int runsCount = 100;
            using (var outputStream = File.Open(string.Format("D:\\statistics\\boundary_bits_{0}.txt", length), FileMode.Create))
            using (var output = new StreamWriter(outputStream))
            {
                output.WriteLine("k | coeff");

                var removeStrategy = new RemoveBorderStrategy();
                var logger = new ConsoleTextLengthLogger(length / 100);
                for (var k = 5; k <= 10; ++k)
                {
                    Console.WriteLine("Conversion for k = {0}", k);
                    var text = new StringBuilder();

                    var e = new RationalNumber(k, k - 1);
                    var generator = new BinaryCharGenerator(text, k);
                    var detector = new RepetitionDetector(text, e, false);

                    Calculate(runsCount, detector, length, removeStrategy, generator, logger, output);
                }
            }
        }

        private static void Calculate(int runs, Detector detector, int length,
            IRemoveStrategy removeStrategy, ICharGenerator generator, IGeneratorLogger logger,
            StreamWriter output)
        {
            Console.WriteLine("Conversion for length = {0}", length);
            var conversionCoeffs = new double[runs];
            var times = new long[runs];
            for (var run = 0; run < runs; ++run)
            {
                detector.Reset();
                var sw = Stopwatch.StartNew();
                RandomWordGenerator.Generate(detector, length, removeStrategy, generator, logger);
                sw.Stop();
                times[run] = sw.ElapsedMilliseconds;
                conversionCoeffs[run] = RandomWordGenerator.CharsGenerated * 1.0 / length;
                Console.WriteLine("\rRun {0} generated in {1} ms", run + 1, sw.ElapsedMilliseconds);
            }
            output.WriteLine("{0} {1} {2} {3}", runs, length, conversionCoeffs.Average(), times.Average());
            output.Flush();
        }
    }
}
