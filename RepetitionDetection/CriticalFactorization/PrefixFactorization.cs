using System;

namespace RepetitionDetection.CriticalFactorization
{
    public class PrefixFactorization : IEquatable<PrefixFactorization>
    {
        public PrefixFactorization(int prefixLength, int criticalPosition, int suffixPeriod)
        {
            PrefixLength = prefixLength;
            CriticalPosition = criticalPosition;
            SuffixPeriod = suffixPeriod;
        }

        public int PrefixLength { get; private set; }
        public int CriticalPosition { get; private set; }
        public int SuffixPeriod { get; private set; }

        public bool Equals(PrefixFactorization other)
        {
            return PrefixLength == other.PrefixLength &&
                   CriticalPosition == other.CriticalPosition &&
                   SuffixPeriod == other.SuffixPeriod;
        }

        public override string ToString()
        {
            return string.Format("Prefix length: {0}, critical position: {1}, suffix period: {2}", PrefixLength, CriticalPosition, SuffixPeriod);
        }
    }
}