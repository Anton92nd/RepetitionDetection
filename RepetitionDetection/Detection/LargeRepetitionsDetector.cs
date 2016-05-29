using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.Catching;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Detection
{
    public class LargeRepetitionsDetector : IDetector
    {
        public LargeRepetitionsDetector([NotNull] StringBuilder text, RationalNumber e, bool detectEqual)
        {
            if (text.Length > 0)
            {
                throw new InvalidUsageException("Text must be empty when creating LargeRepetitionsDetector");
            }
            this.text = text;
            this.e = e;
            this.detectEqual = detectEqual;
            s = (e/(e - 1)).Ceil();
            catchers = new Dictionary<CatcherInterval, Catcher>();
        }

        public bool TryDetect(out Repetition repetition)
        {
            repetition = new Repetition(0, 0);
            var n = text.Length;
            if (n < s)
                return false;
            
            for (var deg2 = 1; deg2*s <= n && n % deg2 == 0; deg2 *= 2)
            {
                CreateCatcher(deg2, n);
                if (n%(deg2*2) == 0)
                {
                    var l = n - 1 - deg2*2*s;
                    var m = n - 1 - deg2*2*s + deg2;
                    var r = n - 1 - deg2*2*(s - 1);
                    RemoveCatcher(new CatcherInterval(l, m));
                    RemoveCatcher(new CatcherInterval(m, r));
                }
            }

            foreach (var pair in catchers)
            {
                if (pair.Value.IsActive())
                {
                    if (pair.Value.TryCatch(out repetition))
                        return true;
                }
            }

            UpdateCatchers();

            return false;
        }

        public void BackTrack()
        {
            var n = text.Length + 1;
            if (n < s)
                return;

            foreach (var catcher in catchers.Values)
            {
                if (catcher.IsActive())
                    catcher.Backtrack();
            }

            UpdateCatchers();
        }

        private void UpdateCatchers()
        {
            var catchersToDelete = catchers
                .Where(pair => pair.Value.ShouldBeDeleted())
                .Select(pair => pair.Key);

            foreach (var catcher in catchersToDelete)
            {
                catchers.Remove(catcher);
            }
        }

        private void RemoveCatcher(CatcherInterval interval)
        {
            Catcher catcher;
            if (!catchers.TryGetValue(interval, out catcher))
                throw new InvalidProgramStateException(string.Format("Can't find catcher for interval: {0}", interval));
            catcher.DeletionTime = text.Length;
        }

        private void CreateCatcher(int deg2, int n)
        {
            var interval = new CatcherInterval(n - 1 - deg2 * s, n - 1 - deg2 * (s - 1));            

            if (!catchers.ContainsKey(interval))
            {
                var i = interval.R;
                var j = Math.Max(i, n - 1 - (new RationalNumber(s) / e * deg2).Ceil());
                var catcher = new Catcher(text, i, j, e, detectEqual, deg2);
                catcher.CreationTime = n;
                catcher.DeletionTime = -1;
                catchers[interval] = catcher;
            }
        }

        private readonly Dictionary<CatcherInterval, Catcher> catchers;

        [NotNull]
        private readonly StringBuilder text;
        private readonly RationalNumber e;
        private readonly bool detectEqual;
        private readonly int s;
    }
}
