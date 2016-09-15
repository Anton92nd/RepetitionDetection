using System;
using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;
using RepetitionDetection.Logging;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace RepetitionDetection.TextGeneration
{
    public static class SyncronizedRandomWordGenerator
    {
        public static int CharsGenerated;

        public static StringBuilder Generate([NotNull] Detector detector1, [NotNull] Detector detector2, int length,
            [NotNull] IRemoveStrategy removeStrategy,
            [NotNull] ICharGenerator generator, [CanBeNull] IGeneratorLogger logger = null)
        {
            CharsGenerated = 0;
            var text = detector1.Text;
            text.EnsureCapacity(length);
            while (text.Length < length)
            {
                text.Append(generator.Generate());
                CharsGenerated++;
                if (logger != null)
                    logger.LogAfterGenerate(text);
                Repetition repetition1, repetition2;
                var delete1 = 0;
                if (detector1.TryDetect(out repetition1))
                {
                    if (logger != null)
                        logger.LogRepetition(text, repetition1);
                    delete1 = removeStrategy.GetCharsToDelete(text.Length, repetition1);
                }
                var delete2 = 0;
                if (detector2.TryDetect(out repetition2))
                {
                    if (logger != null)
                        logger.LogRepetition(text, repetition2);
                    delete2 = removeStrategy.GetCharsToDelete(text.Length, repetition2);
                }
                if (delete1 != delete2)
                    throw new Exception("Not matching");
                for (var i = 0; i < delete1; ++i)
                {
                    detector1.BackTrack();
                    detector2.BackTrack();
                    text.Remove(text.Length - 1, 1);
                }
            }
            return text;
        }
    }
}
