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
        public static int CharsGenerated { get; private set; }

        public static StringBuilder Generate([NotNull] Detector detector, int length, [NotNull] IRemoveStrategy removeStrategy,
            [NotNull] ICharGenerator generator, [CanBeNull] IGeneratorLogger logger = null, [CanBeNull] CancellationToken? token = null)
        {
            CharsGenerated = 0;
            var text = detector.Text;
            text.EnsureCapacity(length);
            while (text.Length < length)
            {
                if (token != null && token.Value.IsCancellationRequested)
                    break;
                if (logger != null)
                    logger.LogBeforeGenerate(text);
                text.Append(generator.Generate());
                CharsGenerated++;
                if (logger != null)
                    logger.LogAfterGenerate(text);
                Repetition repetition;
                if (detector.TryDetect(out repetition))
                {
                    if (logger != null)
                        logger.LogRepetition(text, repetition);
                    var charsToDelete = removeStrategy.GetCharsToDelete(text.Length, repetition);
                    for (var i = 0; i < charsToDelete; ++i)
                    {
                        detector.BackTrack();
                        text.Remove(text.Length - 1, 1);
                    }
                }
            }
            return text;
        }
    }
}
