using System.IO;
using System.Text;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;
using RepetitionDetection.TextGeneration;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace Experiments
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var e = new RationalNumber(7, 4);
            const int alphabetSize = 3;

            var firstStrategy = new NoMoreThanStrategy(new RemoveBorderStrategy(), 10);
            var secondStrategy = new NoMoreThanStrategy(new RemoveBorderStrategy(), 9);

            var first = new DetectionParameters(new RepetitionDetector(new StringBuilder(), e, detectEqual : false), firstStrategy);
            var second = new DetectionParameters(new RepetitionDetector(new StringBuilder(), e, detectEqual : false), secondStrategy);

            var outputFile = File.OpenWrite("D:\\ouptut.txt");
            var writer = new StreamWriter(outputFile);
            var runner = new ExperimentRunner(first, second, new RandomCharGenerator(alphabetSize), writer);

            for (var i = 0; i < 100; ++i)
            {
                writer.WriteLine($"---------Run #{i + 1}---------");
                runner.Run();
                runner.Reset();
                writer.WriteLine("----------------------\n");
                writer.Flush();
            }
        }
    }
}
