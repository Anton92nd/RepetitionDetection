using System;

namespace RepetitionDetection.Commons
{
    public struct Run : IComparable<Run>
    {
        public int Length { get; }
        public int Period { get; }
        public int Border => Length - Period;

        public Run(int length, int period)
        {
            Length = length;
            Period = period;
        }

        public bool Equals(Run other) => Length == other.Length && Period == other.Period;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is Run run && Equals(run);
        }

        public override int GetHashCode()
        {
            unchecked { return (Length * 397) ^ Period; }
        }

        public override string ToString() => $"({Period}, {Border})";

        public int CompareTo(Run other)
        {
            var lengthComparison = Length.CompareTo(other.Length);
            if (lengthComparison != 0)
                return lengthComparison;
            return Period.CompareTo(other.Period);
        }
    }
}