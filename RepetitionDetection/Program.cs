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
            using (var outputStream = File.Open("D:\\statistics\\square4_10000_smart.txt", FileMode.Create))
            using (var output = new StreamWriter(outputStream))
            {
                //GenerateBoundary(output);
                GenerateOther(output);
            }
        }

        private static void GenerateOther(StreamWriter output)
        {
            var Runs = new[] {100};  //300, 300, 200, 200, 100, 100, 100, 50};
            var lengths = new[] {10000}; //100, 200, 300, 400, 500, 600, 700, 800 };
            
            for (var i = 0; i < Runs.Length; ++i)
            {
                var logger = new ConsoleTextLengthLogger(1000);
                var length = lengths[i];
                Console.WriteLine("Calculating conversion for length = {0}", length);
                var text = new StringBuilder();


                var e = new RationalNumber(2);
                var generator = new RandomNotLastCharGenerator(text, 4);//ByTailCharGenerator(text, 4);
                var detector = new RepetitionDetector(text, e, true);
                var removeStrategy = new RemoveBorderStrategy();


                var conversionCoeffs = new double[Runs[i]];
                var times = new long[Runs[i]];
                for (var run = 0; run < Runs[i]; ++run)
                {
                    detector.Reset();
                    var sw = Stopwatch.StartNew();
                    RandomWordGenerator.Generate(detector, length, removeStrategy, generator, logger);
                    sw.Stop();
                    times[run] = sw.ElapsedMilliseconds;
                    conversionCoeffs[run] = RandomWordGenerator.CharsGenerated * 1.0 / length;
                    Console.WriteLine("\rRun {0} generated in {1} ms", run + 1, sw.ElapsedMilliseconds);
                }
                output.WriteLine("{0} {1} {2} {3}", Runs[i], length, conversionCoeffs.Average(), times.Average());
                output.Flush();
            }
        }

        private static void GenerateBoundary(StreamWriter output)
        {
            const int length = 100000;
            const int runsCount = 100;
            var removeStrategy = new RemoveBorderStrategy();
            var logger = new ConsoleTextLengthLogger(length / 100);
            for (var k = 5; k <= 10; ++k)
            {
                Console.WriteLine("Calculating conversion for k = {0}", k);
                var e = new RationalNumber(k, k - 1);
                var text = new StringBuilder();
                var generator = new ByTailCharGenerator(text, k);
                var detector = new RepetitionDetector(text, e, false);
                var conversionCoeffs = new double[runsCount];
                for (var run = 0; run < runsCount; ++run)
                {
                    detector.Reset();
                    var sw = Stopwatch.StartNew();
                    RandomWordGenerator.Generate(detector, length, removeStrategy, generator, logger);
                    sw.Stop();
                    conversionCoeffs[run] = RandomWordGenerator.CharsGenerated*1.0/length;
                    Console.WriteLine("\rRun {0} generated in {1} ms", run + 1, sw.ElapsedMilliseconds);
                }
                output.WriteLine("{0} {1}", k, conversionCoeffs.Average());
            }
        }
    }
}
