using System;
using System.Collections.Generic;

namespace RepetitionDetection.TextGeneration
{
    public class Statistics
    {
        public Statistics()
        {
            CharsGenerated = 0;
            Milliseconds = 0;
            CountOfRuns = new Dictionary<ValueTuple<int, int>, int>();
        }

        public void Clear()
        {
            CountOfRuns.Clear();
            CharsGenerated = 0;
            Milliseconds = 0;
            TextLength = 0;
        }

        public int CharsGenerated { get; set; }

        public volatile int TextLength;

        public long Milliseconds { get; set; }
        public readonly Dictionary<ValueTuple<int, int>, int> CountOfRuns;
    }
}