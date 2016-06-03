using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.Catching;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Detection
{
    public class LargeRepetitionDetector : IDetector
    {
        public LargeRepetitionDetector([NotNull] StringBuilder text, RationalNumber e, bool detectEqual)
        {
            if (text.Length > 0)
            {
                throw new InvalidUsageException("Text must be empty when creating LargeRepetitionDetector");
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

            UpdateCatchers(reverse:false);
            DeleteCatchers();

            var result = false;
            foreach (var pair in catchers)
            {
                if (pair.Value.IsActive())
                {
                    Repetition rep;
                    if (pair.Value.TryCatch(out rep))
                    {
                        result = true;
                        repetition = rep;
                    }
                }
            }
            return result;
        }

        private void UpdateCatchers(bool reverse)
        {
            var n = text.Length;
            for (var deg2 = 1; deg2*s <= n && n%deg2 == 0; deg2 *= 2)
            {
                UpdateCatcher(new CatcherInterval(n - 1 - deg2 * s, n - 1 - deg2 * (s - 1)), reverse);
                if (n%(deg2*2) == 0)
                {
                    var l = n - 1 - deg2*2*s;
                    var m = n - 1 - deg2*2*s + deg2;
                    var r = n - 1 - deg2*2*(s - 1);
                    UpdateCatcher(new CatcherInterval(l, m), !reverse);
                    UpdateCatcher(new CatcherInterval(m, r), !reverse);
                }
            }
        }

        private void UpdateCatcher(CatcherInterval interval, bool reverse)
        {
            if (!reverse)
            {
                CreateCatcher(interval);
            }
            else
            {
                RemoveCatcher(interval);
            }
        }

        public void BackTrack()
        {
            foreach (var catcher in catchers.Values)
            {
                if (catcher.IsActive())
                    catcher.Backtrack();
            }

            UpdateCatchers(reverse:true);
            DeleteCatchers();
        }

        private void DeleteCatchers()
        {
            var catchersToDelete = catchers
                .Where(pair => pair.Value.ShouldBeDeleted())
                .Select(pair => pair.Key)
                .ToArray();

            foreach (var catcher in catchersToDelete)
            {
                catchers.Remove(catcher);
            }
        }

        private void RemoveCatcher(CatcherInterval interval)
        {
            if (interval.L < -1)
                return;
            Catcher catcher;
            if (!catchers.TryGetValue(interval, out catcher))
            {
                throw new InvalidProgramStateException(string.Format("Can't find catcher for interval: {0}", interval));
            }
            catcher.RemoveTime = text.Length;
        }

        private void CreateCatcher(CatcherInterval interval)
        {
            if (interval.L < -1)
                return;
            var n = interval.L + 1 + s * interval.Length;
            Catcher catcher;
            var i = interval.R;
            var j = Math.Max(i, n - 1 - (new RationalNumber(s) / e * interval.Length).Ceil());
            if (!catchers.TryGetValue(interval, out catcher))
            {
                catcher = new Catcher(text, i, j, e, detectEqual, interval.Length);
                catcher.WarmUp(j + 2, text.Length);
                catchers[interval] = catcher;
            }
            catcher.RemoveTime = -1;
        }

        private readonly Dictionary<CatcherInterval, Catcher> catchers;

        [NotNull]
        private readonly StringBuilder text;
        private readonly RationalNumber e;
        private readonly bool detectEqual;
        private readonly int s;
    }
}
