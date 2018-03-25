using System.IO;
using JetBrains.Annotations;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Commons;

namespace Experiments
{
    public class ExperimentRunner
    {
        public ExperimentRunner(
            [NotNull] DetectionParameters first,
            [NotNull] DetectionParameters second,
            [NotNull] ICharGenerator charGenerator,
            [NotNull] StreamWriter writer)
        {
            First = first;
            Second = second;
            CharGenerator = charGenerator;
            Writer = writer;
        }

        public void Run()
        {
            if (ReferenceEquals(First.Detector.Text, Second.Detector.Text))
                throw new InvalidProgramStateException("Detetors must have different instances of text");

            Writer.WriteLine("Started running while equal...\n");
            var repetitions = RunWhileEqual();

            var firstCharsToRemove = First.RemoveStrategy.GetCharsToDelete(First.Detector.Text.Length, repetitions.First);
            var secondCharsToRemove = Second.RemoveStrategy.GetCharsToDelete(Second.Detector.Text.Length, repetitions.Second);
            Writer.WriteLine($"Text: {First.Detector.Text}\n" +
                             $"Repetition: {repetitions.First}\n" +
                             $"First removes: {firstCharsToRemove}\n" +
                             $"Second removes: {secondCharsToRemove}\n");

            for (var i = 0; i < firstCharsToRemove; i++)
            {
                First.Detector.Backtrack();
                First.Detector.Text.Length -= 1;
            }
            for (var i = 0; i < secondCharsToRemove; i++)
            {
                Second.Detector.Backtrack();
                Second.Detector.Text.Length -= 1;
            }
            Writer.WriteLine("Runnig to next difference...\n");
            RunToNextDifference();
            Writer.WriteLine($"First text:  {First.Detector.Text}");
            Writer.WriteLine($"Second text: {Second.Detector.Text}");
        }

        public void Reset()
        {
            First.Detector.Reset();
            First.Detector.Text.Clear();

            Second.Detector.Reset();
            Second.Detector.Text.Clear();
        }

        private (Repetition First, Repetition Second) RunWhileEqual()
        {
            while (true)
            {
                var generatedChar = CharGenerator.Generate();
                First.Detector.Text.Append(generatedChar);
                Second.Detector.Text.Append(generatedChar);

                var firstDetected = First.Detector.TryDetect(out var firstRepetition);
                var secondDetected = Second.Detector.TryDetect(out var secondRepetition);
                if (firstDetected ^ secondDetected)
                    throw new InvalidProgramStateException($"Detectors give different responses on text: {First.Detector.Text}");

                if (firstDetected && firstRepetition != secondRepetition)
                    throw new InvalidProgramStateException($"Detectors have found different repetitions on text: {First.Detector.Text}\n" +
                                                           $"First repetition: {firstRepetition}\n" +
                                                           $"Second repetition: {secondRepetition}");

                if (firstDetected)
                {
                    var firstCharsToRemove = First.RemoveStrategy.GetCharsToDelete(First.Detector.Text.Length, firstRepetition);
                    var secondCharsToRemove = Second.RemoveStrategy.GetCharsToDelete(Second.Detector.Text.Length, secondRepetition);
                    if (firstCharsToRemove != secondCharsToRemove)
                        return (firstRepetition, secondRepetition);

                    for (var i = 0; i < firstCharsToRemove; i++)
                    {
                        First.Detector.Backtrack();
                        First.Detector.Text.Length -= 1;
                        Second.Detector.Backtrack();
                        Second.Detector.Text.Length -= 1;
                    }
                }
            }
        }

        private void RunToNextDifference()
        {
            while (true)
            {
                var generatedChar = CharGenerator.Generate();
                First.Detector.Text.Append(generatedChar);
                Second.Detector.Text.Append(generatedChar);

                var firstDetected = First.Detector.TryDetect(out var firstRepetition);
                var secondDetected = Second.Detector.TryDetect(out var secondRepetition);
                if (firstDetected ^ secondDetected)
                {
                    Writer.WriteLine($"First detected: {firstDetected}");
                    if (firstDetected)
                        Writer.WriteLine($"\tRepetition: {firstRepetition}");
                    Writer.WriteLine($"Second detected: {secondDetected}");
                    if (secondDetected)
                        Writer.WriteLine($"\tRepetition: {secondRepetition}");
                    return;
                }

                if (firstDetected && firstRepetition != secondRepetition)
                {
                    Writer.WriteLine("Detectors have found different repetitions:");
                    Writer.WriteLine($"\tFirst repetition: {firstRepetition}");
                    Writer.WriteLine($"\tSecond repetition: {secondRepetition}");
                    return;
                }

                if (firstDetected)
                {
                    var firstCharsToRemove = First.RemoveStrategy.GetCharsToDelete(First.Detector.Text.Length, firstRepetition);
                    var secondCharsToRemove = Second.RemoveStrategy.GetCharsToDelete(Second.Detector.Text.Length, secondRepetition);
                    if (firstCharsToRemove != secondCharsToRemove)
                    {
                        Writer.WriteLine("Different symbols to remove again:");
                        Writer.WriteLine($"\tFirst repetition: {firstRepetition}");
                        Writer.WriteLine($"\tFirst removes: {firstCharsToRemove}");
                        Writer.WriteLine($"\tSecond repetition: {secondRepetition}");
                        Writer.WriteLine($"\tSecond removes: {secondCharsToRemove}");
                        return;
                    }

                    for (var i = 0; i < firstCharsToRemove; i++)
                    {
                        First.Detector.Backtrack();
                        First.Detector.Text.Length -= 1;
                        Second.Detector.Backtrack();
                        Second.Detector.Text.Length -= 1;
                    }
                }
            }
        }

        [NotNull]
        public DetectionParameters First { get; }

        [NotNull]
        public DetectionParameters Second { get; }

        [NotNull]
        public ICharGenerator CharGenerator { get; }

        [NotNull]
        private StreamWriter Writer { get; }
    }
}