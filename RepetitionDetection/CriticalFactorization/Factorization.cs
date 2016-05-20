using System;

namespace RepetitionDetection.CriticalFactorization
{
    public class Factorization : IEquatable<Factorization>
    {
        public Factorization(string pattern, PrefixFactorization prefixFactorization,
            PrefixFactorization patternFactorization)
        {
            Pattern = pattern;
            PrefixFactorization = prefixFactorization;
            PatternFactorization = patternFactorization;
        }

        public string Pattern { get; private set; }
        public PrefixFactorization PrefixFactorization { get; private set; }
        public PrefixFactorization PatternFactorization { get; private set; }

        public bool Equals(Factorization other)
        {
            return Pattern.Equals(other.Pattern) && PrefixFactorization.Equals(other.PrefixFactorization) &&
                   PatternFactorization.Equals(other.PatternFactorization);
        }

        public override string ToString()
        {
            return string.Format("Pattern: {0}\nPrefix factorization: {1}\nPattern factorization: {2}",
                Pattern, PrefixFactorization, PatternFactorization);
        }
    }
}