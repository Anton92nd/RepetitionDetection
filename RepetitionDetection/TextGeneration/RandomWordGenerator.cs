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
        public static StringBuilder Generate([NotNull] Detector detector, int length,
            [NotNull] IRemoveStrategy removeStrategy,
            [NotNull] ICharGenerator generator, [CanBeNull] IGeneratorLogger logger = null,
            [CanBeNull] CancellationToken? token = null)
        {
            Statistics.Clear();
            var text = detector.Text;
            text.EnsureCapacity(length);
            var sw = Stopwatch.StartNew();
            while (text.Length < length)
            {
                if (token != null && token.Value.IsCancellationRequested)
                    break;
                text.Append(generator.Generate());
                Statistics.CharsGenerated++;
                logger?.LogAfterGenerate(text);
                if (detector.TryDetect(out var repetition))
                {
                    AddToStats(repetition, text.Length);
                    var charsToDelete = removeStrategy.GetCharsToDelete(text.Length, repetition);
                    logger?.LogRepetition(text, repetition);
                    for (var i = 0; i < charsToDelete; ++i)
                    {
                        detector.Backtrack();
                        text.Remove(text.Length - 1, 1);
                    }
                }
            }
            sw.Stop();
            Statistics.Milliseconds = sw.ElapsedMilliseconds;
            return text;
        }

        private static void AddToStats(Repetition repetition, int textLength)
        {
            var (period, border) = (repetition.Period, textLength - (repetition.LeftPosition + 1) - repetition.Period);
            if (!Statistics.CountOfRuns.ContainsKey((period, border)))
                Statistics.CountOfRuns[(period, border)] = 1;
            else
                Statistics.CountOfRuns[(period, border)]++;
        }

        public static readonly Statistics Statistics = new Statistics();
    }
}