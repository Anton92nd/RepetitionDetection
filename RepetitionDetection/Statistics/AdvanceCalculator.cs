using System.Collections.Generic;
using System.Linq;

namespace RepetitionDetection.Statistics
{
    public class AdvanceCalculator
    {
        public AdvanceCalculator()
        {
            counters = new List<PositionStatistics>();
        }

        public void Advance(int length, int delta, bool afterAdvance)
        {
            EnsureLength(length);
            counters[length].VisitCount++;
            counters[length].LengthDeltaSum += delta;
            if (afterAdvance)
                counters[length].AfterAdvanceCount++;
            if (delta > 0)
            {
                counters[length].AdvanceCount++;
                if (afterAdvance)
                    counters[length].AdvanceAfterAdvanceCount++;
            }
        }

        private void EnsureLength(int length)
        {
            if (length >= counters.Count)
                counters.AddRange(Enumerable.Range(0, length - counters.Count + 1).Select(x => new PositionStatistics()));
        }

        public void Clear()
        {
            counters.Clear();
        }

        public IReadOnlyCollection<PositionStatistics> Counters => counters.AsReadOnly();

        private readonly List<PositionStatistics> counters;
    }
}