using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Logging
{
    public class GenerationLogger : IDisposable
    {
        public GenerationLogger([NotNull] IList<(LogLevel Level, StreamWriter Writer)> logSettings)
        {
            Loggers = logSettings.ToDictionary(p => p.Level, p => p.Writer);
        }

        public void Log(LogLevel logLevel, [NotNull] string message)
        {
            TryGetLogger(logLevel)?.WriteLine(message);
        }

        public void LogRepetition([NotNull] StringBuilder text, Repetition repetition, LogLevel logLevel = LogLevel.Full)
        {
            var length = text.Length - 1 - repetition.LeftPosition;
            TryGetLogger(logLevel)?.WriteLine($"{text} {length} {length - repetition.Period} {repetition.Period}");
        }

        public void Flush(LogLevel logLevel)
        {
            TryGetLogger(logLevel)?.Flush();
        }

        [CanBeNull]
        private StreamWriter TryGetLogger(LogLevel logLevel)
        {
            return Loggers.TryGetValue(logLevel, out var result) ? result : null;
        }

        public void Dispose()
        {
            foreach (var logger in Loggers.Values)
            {
                logger.Dispose();
            }
        }

        private Dictionary<LogLevel, StreamWriter> Loggers { get; }
    }
}