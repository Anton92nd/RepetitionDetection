using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.Catching;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Detection
{
    public class LargeRepetitionDetector : Detector
    {
        public LargeRepetitionDetector([NotNull] StringBuilder text, RationalNumber e, bool detectEqual) : base(text, e,
            detectEqual)
        {
            catchers = new Dictionary<CatcherInterval, Catcher>();
        }

        public override bool TryDetect(out Repetition repetition)
        {
            repetition = new Repetition(0, 0);

            UpdateCatchers(create : true);
            DeleteObsoleteCatchers();

            var result = false;
            foreach (var pair in catchers)
                if (pair.Value.IsActive() && pair.Value.TryCatch(out var rep))
                {
                    result = true;
                    repetition = rep;
                }
            return result;
        }

        private void UpdateCatchers(bool create)
        {
            var n = Text.Length;
            for (var deg2 = 1; deg2 * S <= n && n % deg2 == 0; deg2 *= 2)
            {
                UpdateCatcher(new CatcherInterval(n - 1 - deg2 * S, n - 1 - deg2 * (S - 1)), create);
                if (n % (deg2 * 2) == 0)
                {
                    var l = n - 1 - deg2 * 2 * S;
                    var m = n - 1 - deg2 * 2 * S + deg2;
                    var r = n - 1 - deg2 * 2 * (S - 1);
                    UpdateCatcher(new CatcherInterval(l, m), !create);
                    UpdateCatcher(new CatcherInterval(m, r), !create);
                }
            }
        }

        private void UpdateCatcher(CatcherInterval interval, bool create)
        {
            if (create)
                CreateCatcher(interval);
            else
                RemoveCatcher(interval);
        }

        public override void Backtrack()
        {
            foreach (var catcher in catchers.Values)
                if (catcher.IsActive())
                    catcher.Backtrack();

            UpdateCatchers(create : false);
            DeleteObsoleteCatchers();
        }

        public override void Reset()
        {
            catchers.Clear();
            Text.Clear();
        }

        private void DeleteObsoleteCatchers()
        {
            var catchersToDelete = catchers
                .Where(pair => pair.Value.IsObsolete())
                .Select(pair => pair.Key)
                .ToArray();

            foreach (var catcher in catchersToDelete)
                catchers.Remove(catcher);
        }

        private void RemoveCatcher(CatcherInterval interval)
        {
            if (interval.L < -1)
                return;
            if (!catchers.TryGetValue(interval, out var catcher))
                throw new InvalidProgramStateException($"Can't find catcher for interval: {interval}");
            catcher.RemoveTime = Text.Length;
        }

        private void CreateCatcher(CatcherInterval interval)
        {
            if (interval.L < -1)
                return;
            var n = interval.L + 1 + S * interval.Length;
            var i = interval.R;
            var j = Math.Max(i, n - 1 - (new RationalNumber(S) / E * interval.Length).Ceil());
            if (!catchers.TryGetValue(interval, out var catcher))
            {
                catcher = new Catcher(Text, i, j, E, DetectEqual, interval.Length);
                catcher.WarmUp(j + 2, Text.Length);
                catchers[interval] = catcher;
            }
            catcher.RemoveTime = -1;
        }

        private readonly Dictionary<CatcherInterval, Catcher> catchers;
    }
}