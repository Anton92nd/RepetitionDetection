using System;
using System.IO;
using System.Text;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace Experiments
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var e = new RationalNumber(7, 4);

            var first = new DetectionParameters(new RepetitionDetector(new StringBuilder(), e, detectEqual : false), new CustomFirstStrategy());
            var second = new DetectionParameters(new RepetitionDetector(new StringBuilder(), e, detectEqual : false), new CustomSecondStrategy());

            var outputFile = File.Open("D:\\ouptut.txt", FileMode.Create);
            var writer = new StreamWriter(outputFile);
            var runner = new ExperimentRunner(first, second, new CustomCharGenerator(first.Detector.Text, second.Detector.Text), writer);

            for (var i = 0; i < 100; ++i)
            {
                writer.WriteLine($"---------Run #{i + 1}---------");
                runner.Run();
                runner.Reset();
                writer.WriteLine("----------------------\n");
                writer.Flush();
            }
        }

        private class CustomCharGenerator : ICharGenerator
        {
            public CustomCharGenerator(StringBuilder firstText, StringBuilder secondText)
            {
                this.firstText = firstText;
                this.secondText = secondText;
                AlphabetSize = alphabet.Length;
            }

            public char Generate()
            {
                if(firstText.Length == 0 || secondText.Length == 0)
                    return alphabet[RandomNumberGenerator.Generate(alphabet.Length)];
                var firstChar = firstText[firstText.Length - 1];
                var secondChar = secondText[secondText.Length - 1];
                if (firstChar == secondChar)
                    return alphabet[(alphabet.IndexOf(firstChar) + RandomNumberGenerator.Generate(1, alphabet.Length)) % alphabet.Length];
                foreach (var c in alphabet)
                {
                    if (c != firstChar && c != secondChar)
                        return c;
                }
                throw new InvalidProgramStateException("This can not happen");
            }

            public int AlphabetSize { get; }

            private const string alphabet = "abc";

            private readonly StringBuilder firstText;
            private readonly StringBuilder secondText;
        }

        private class CustomFirstStrategy : IRemoveStrategy
        {
            public int GetCharsToDelete(int textLength, Repetition repetition)
            {
                return Math.Min(10, baseStrategy.GetCharsToDelete(textLength, repetition));
            }

            private readonly IRemoveStrategy baseStrategy = new RemoveBorderStrategy();
        }

        private class CustomSecondStrategy : IRemoveStrategy
        {
            public int GetCharsToDelete(int textLength, Repetition repetition)
            {
                if (repetition.Period >= 12 && repetition.Period <= 13)
                    return 9;
                return Math.Min(10, baseStrategy.GetCharsToDelete(textLength, repetition));
            }

            private readonly IRemoveStrategy baseStrategy = new RemoveBorderStrategy();
        }
    }
}
