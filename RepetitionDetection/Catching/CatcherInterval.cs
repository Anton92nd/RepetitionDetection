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

        public override int GetHashCode()
        {
            return L.GetHashCode() ^ R.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("L: {0}, R: {1}", L, R);
        }

        public readonly int L, R;
    }
}
