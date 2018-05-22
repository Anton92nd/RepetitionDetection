using System.Diagnostics;
using System.Text;
using System.Threading;
using JetBrains.Annotations;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;
using RepetitionDetection.Logging;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace RepetitionDetection.TextGeneration
{
    public static class RandomWordGenerator
    {
        public static StringBuilder Generate(
            [NotNull] Detector detector, 
            int length,
            [NotNull] IRemoveStrategy removeStrategy,
            [NotNull] ICharGenerator generator,
            [CanBeNull] Statistics statistics = null,
            [CanBeNull] GenerationLogger logger = null,
            [CanBeNull] CancellationToken? token = null)
        {
            statistics = statistics ?? new Statistics();
            statistics.Clear();
            var afterAdvance = true;
            var text = detector.Text;
            text.EnsureCapacity(length);
            var sw = Stopwatch.StartNew();
            while (text.Length < length)
            {
                if (token?.IsCancellationRequested ?? false)
                    break;
                text.Append(generator.Generate());
                statistics.CharsGenerated++;
                statistics.TextLength = text.Length;
                if (detector.TryDetect(out var repetition))
                {
                    AddRepetitionToStats(statistics, repetition, text.Length);
                    var charsToDelete = removeStrategy.GetCharsToDelete(text.Length, repetition);
                    statistics.AdvanceCalculator.Advance(text.Length - 1, -charsToDelete + 1, afterAdvance);
                    logger?.LogRepetition(text, repetition);
                    for (var i = 0; i < charsToDelete; ++i)
                    {
                        detector.Backtrack();
                        text.Length -= 1;
                    }
                    afterAdvance = false;
                }
                else
                {
                    statistics.AdvanceCalculator.Advance(text.Length - 1, 1, afterAdvance);
                    afterAdvance = true;
                }
            }
            sw.Stop();
            statistics.Milliseconds = sw.ElapsedMilliseconds;

            statistics.Aggregate();
            return text;
        }

        private static void AddRepetitionToStats(Statistics statistics, Repetition repetition, int textLength)
        {
            var run = new Run(textLength - (repetition.LeftPosition + 1), repetition.Period);
            if (!statistics.CountOfRuns.ContainsKey(run))
                statistics.CountOfRuns[run] = 1;
            else
                statistics.CountOfRuns[run]++;
        }
    }
}