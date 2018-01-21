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

        public bool Equals(Repetition other)
        {
            return LeftPosition == other.LeftPosition && Period == other.Period;
        }

        public override string ToString()
        {
            return $"Left position: {LeftPosition}, Period: {Period}";
        }
    }
}