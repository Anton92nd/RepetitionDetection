using System;

namespace RepetitionDetection.CriticalFactorization
{
    public class PrefixFactorization : IEquatable<PrefixFactorization>
    {
        public PrefixFactorization(int prefixLength, int criticalPosition)
        {
            PrefixLength = prefixLength;
            CriticalPosition = criticalPosition;
        }

        public int PrefixLength { get; private set; }
        public int CriticalPosition { get; private set; }

        public bool Equals(PrefixFactorization other)
        {
            return PrefixLength == other.PrefixLength && CriticalPosition == other.CriticalPosition;
        }

        public override string ToString()
        {
            return string.Format("Prefix length: {0}, critical position: {1}", PrefixLength, CriticalPosition);
        }
    }
}