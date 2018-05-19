using System.Collections.Generic;
using System.Linq;

namespace RepetitionDetection.Statistics
{
    public class AdvanceCalculator
    {
        public AdvanceCalculator()
        {
            counters = new List<(int, int)>();
        }

        public void Advance(int length)
        {
            EnsureLength(length);
            var count = counters[length];
            counters[length] = (count.Count + 1, count.Advance + 1);
        }

        public void Retract(int length)
        {
            EnsureLength(length);
            var count = counters[length];
            counters[length] = (count.Count + 1, count.Advance);
        }

        private void EnsureLength(int length)
        {
            if (length >= counters.Count)
                counters.AddRange(Enumerable.Repeat((0, 0), length - counters.Count + 1));
        }

        public void Clear()
        {
            counters.Clear();
        }

        public IReadOnlyCollection<(int Count, int Advance)> Counters => counters.AsReadOnly();

        private readonly List<(int Count, int Advance)> counters;
    }
}