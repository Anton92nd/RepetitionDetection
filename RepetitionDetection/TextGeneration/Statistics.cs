using System.Collections.Generic;
using System.Linq;
using RepetitionDetection.Commons;
using RepetitionDetection.Statistics;

namespace RepetitionDetection.TextGeneration
{
    public class Statistics
    {
        public Statistics()
        {
            AdvanceCalculator = new AdvanceCalculator();
            TotalAdvances = new List<(int Count, int Advance)>();

            CharsGenerated = 0;
            TotalCharsGenerated = 0;

            Milliseconds = 0;
            TotalMilliseconds = 0;

            CountOfRuns = new Dictionary<Run, int>();
            TotalCountOfRuns = new Dictionary<Run, int>();
        }

        public void Clear()
        {
            AdvanceCalculator.Clear();
            CountOfRuns.Clear();
            CharsGenerated = 0;
            Milliseconds = 0;
            TextLength = 0;
        }

        public void Aggregate()
        {
            foreach (var pair in CountOfRuns)
            {
                if (TotalCountOfRuns.ContainsKey(pair.Key))
                    TotalCountOfRuns[pair.Key] += pair.Value;
                else
                    TotalCountOfRuns[pair.Key] = pair.Value;
            }

            TotalCharsGenerated += CharsGenerated;
            TotalMilliseconds += Milliseconds;

            var counters = AdvanceCalculator.Counters.ToArray();
            if (TotalAdvances.Count < counters.Length)
                TotalAdvances.AddRange(Enumerable.Repeat((0, 0), counters.Length - TotalAdvances.Count));
            for (var i = 0; i < counters.Length; ++i)
            {
                TotalAdvances[i] = (
                    TotalAdvances[i].Count + counters[i].Count,
                    TotalAdvances[i].Advance + counters[i].Advance
                );
            }
        }

        public int CharsGenerated { get; set; }

        public long Milliseconds { get; set; }

        public Dictionary<Run, int> CountOfRuns { get; }

        public AdvanceCalculator AdvanceCalculator { get; }

        public volatile int TextLength;

        public int TotalCharsGenerated { get; set; }

        public long TotalMilliseconds { get; set; }

        public Dictionary<Run, int> TotalCountOfRuns { get; }

        public List<(int Count, int Advance)> TotalAdvances { get; }
    }
}