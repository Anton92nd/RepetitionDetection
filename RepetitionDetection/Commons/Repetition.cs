using System;

namespace RepetitionDetection.Commons
{
    public struct Repetition : IEquatable<Repetition>
    {
        public readonly int LeftPosition;
        public readonly int Period;

        public Repetition(int leftPosition, int period)
        {
            LeftPosition = leftPosition;
            Period = period;
        }

        public override bool Equals(object obj)
        {
            return obj is Repetition r && Equals(r);
        }

        public bool Equals(Repetition other)
        {
            return LeftPosition == other.LeftPosition && Period == other.Period;
        }

        public override int GetHashCode()
        {
            return LeftPosition * 997 + Period.GetHashCode();
        }

        public override string ToString()
        {
            return $"Left position: {LeftPosition}, Period: {Period}";
        }

        public static bool operator ==(Repetition first, Repetition second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Repetition first, Repetition second)
        {
            return !(first == second);
        }
    }
}