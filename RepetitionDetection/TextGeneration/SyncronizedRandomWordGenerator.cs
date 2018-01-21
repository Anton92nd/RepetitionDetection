using System;
using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Detection;
using RepetitionDetection.Logging;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace RepetitionDetection.TextGeneration
{
    public static class SyncronizedRandomWordGenerator
    {
        public static StringBuilder Generate([NotNull] Detector detector1, [NotNull] Detector detector2, int length,
            [NotNull] IRemoveStrategy removeStrategy,
            [NotNull] ICharGenerator generator, [CanBeNull] IGeneratorLogger logger = null)
        {
            var text = detector1.Text;
            text.EnsureCapacity(length);
            while (text.Length < length)
            {
                text.Append(generator.Generate());
                logger?.LogAfterGenerate(text);
                var delete1 = 0;
                if (detector1.TryDetect(out var repetition1))
                {
                    delete1 = removeStrategy.GetCharsToDelete(text.Length, repetition1);
                    logger?.LogRepetition(text, repetition1);
                }
                var delete2 = 0;
                if (detector2.TryDetect(out var repetition2))
                {
                    delete2 = removeStrategy.GetCharsToDelete(text.Length, repetition2);
                    logger?.LogRepetition(text, repetition2);
                }
                if (delete1 != delete2)
                    throw new Exception("Not matching");
                for (var i = 0; i < delete1; ++i)
                {
                    detector1.Backtrack();
                    detector2.Backtrack();
                    text.Remove(text.Length - 1, 1);
                }
            }
            return text;
        }
    }
}