using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;
using RepetitionDetection.TextGeneration;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace RepetitionDetection
{
    public static class Program
    {
        private static readonly int RunsCount = 100;
        private static readonly int Length = 100000;


        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            using (var outputStream = File.Open(string.Format("D:\\statistics\\output_{0}.txt", Length), FileMode.Create))
            using (var output = new StreamWriter(outputStream))
            {
                var removeStrategy = new RemoveBorderStrategy();
                for (var k = 5; k <= 10; ++k)
                {
                    Console.WriteLine("Calculating conversion for k = {0}", k);
                    var e = new RationalNumber(k, k - 1);
                    var detector = new RepetitionDetector(new StringBuilder(), e, false);
                    var conversionCoeffs = new double[RunsCount];
                    for (var run = 0; run < RunsCount; ++run)
                    {
                        detector.Reset();
                        var sw = Stopwatch.StartNew();
                        RandomWordGenerator.Generate(detector, k, Length, removeStrategy);
                        sw.Stop();
                        conversionCoeffs[run] = RandomWordGenerator.CharsGenerated * 1.0 / Length;
                        Console.WriteLine("Run {0} generated in {1} ms", run + 1, sw.ElapsedMilliseconds);
                    }
                    output.WriteLine("{0} {1}", k, conversionCoeffs.Average());
                }
            }
        }
    }
}
