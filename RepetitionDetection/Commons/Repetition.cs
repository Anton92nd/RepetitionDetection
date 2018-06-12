using System;

namespace RepetitionDetection.Commons
{
    public struct Repetition : IEquatable<Repetition>
    {
        public int LeftPosition { get; }
        public int Period { get; }

        public Repetition(int leftPosition, int period)
        {
            LeftPosition = leftPosition;
            Period = period;
        }

        public override bool Equals(object obj) => obj is Repetition r && Equals(r);

        public bool Equals(Repetition other) => LeftPosition == other.LeftPosition && Period == other.Period;

        public override int GetHashCode() => LeftPosition * 997 + Period.GetHashCode();

        public override string ToString() => $"Left position: {LeftPosition}, Period: {Period}";

        public static bool operator ==(Repetition first, Repetition second) => first.Equals(second);

        public static bool operator !=(Repetition first, Repetition second) => !(first == second);
    }
}