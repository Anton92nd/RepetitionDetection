using System.Collections.Generic;

namespace RepetitionDetection.TextGeneration
{
    public class Statistics
    {
        public Statistics()
        {
            CharsGenerated = 0;
            Milliseconds = 0;
            CountOfPeriods = new Dictionary<int, int>();
        }

        public void Clear()
        {
            CountOfPeriods.Clear();
            CharsGenerated = 0;
            Milliseconds = 0;
        }

        public int CharsGenerated { get; set; }
        public long Milliseconds { get; set; }
        public readonly Dictionary<int, int> CountOfPeriods;
    }
}