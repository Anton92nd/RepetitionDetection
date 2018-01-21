using System;

namespace RepetitionDetection.Catching
{
    public struct CatcherInterval : IEquatable<CatcherInterval>
    {
        public CatcherInterval(int l, int r)
        {
            L = l;
            R = r;
        }

        public bool Equals(CatcherInterval other)
        {
            return L == other.L && R == other.R;
        }

        public int Length => R - L;

        public override int GetHashCode()
        {
            return L.GetHashCode() ^ R.GetHashCode();
        }

        public override string ToString()
        {
            return $"Interval ({L}, {R}]";
        }

        public readonly int L, R;
    }
}