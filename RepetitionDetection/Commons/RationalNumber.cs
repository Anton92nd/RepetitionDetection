using System;
using System.Diagnostics.Contracts;

namespace RepetitionDetection.Commons
{
    public struct RationalNumber
    {
        public RationalNumber(int numerator, int denominator = 1)
        {
            if (denominator == 0)
                throw new InvalidProgramStateException("Denominator can't be equal to zero");
            if (denominator < 0)
            {
                denominator *= -1;
                numerator *= -1;
            }
            var gcd = Gcd(Math.Abs(numerator), denominator);
            Num = numerator / gcd;
            Denom = denominator / gcd;
        }

        public static RationalNumber operator *(RationalNumber a, RationalNumber b)
        {
            return new RationalNumber(a.Num*b.Num, a.Denom*b.Denom);
        }

        public static RationalNumber operator *(RationalNumber a, int b)
        {
            return new RationalNumber(a.Num * b, a.Denom);
        }

        public static RationalNumber operator /(RationalNumber a, RationalNumber b)
        {
            if (b.Num == 0)
                throw new InvalidProgramStateException("Can't divide by zero");
            return new RationalNumber(a.Num * b.Denom, a.Denom * b.Num);
        }

        public static RationalNumber operator /(RationalNumber a, int b)
        {
            if (b == 0)
                throw new InvalidProgramStateException("Can't divide by zero");
            return new RationalNumber(a.Num, a.Denom * b);
        }

        public static RationalNumber operator +(RationalNumber a, RationalNumber b)
        {
            var denom = Lcm(a.Denom, b.Denom);
            var k1 = denom/a.Denom;
            var k2 = denom/b.Denom;
            return new RationalNumber(a.Num * k1 + b.Num * k2, denom);
        }

        public static RationalNumber operator +(RationalNumber a, int b)
        {
            return a + new RationalNumber(b);
        }

        public static RationalNumber operator -(RationalNumber a, RationalNumber b)
        {
            var denom = Lcm(a.Denom, b.Denom);
            var k1 = denom / a.Denom;
            var k2 = denom / b.Denom;
            return new RationalNumber(a.Num * k1 - b.Num * k2, denom);
        }

        public static RationalNumber operator -(RationalNumber a, int b)
        {
            return a - new RationalNumber(b);
        }

        public static bool operator <(RationalNumber a, RationalNumber b)
        {
            var lcm = Lcm(a.Denom, b.Denom);
            var k1 = lcm/a.Denom;
            var k2 = lcm/b.Denom;
            return a.Num*k1 < b.Num*k2;
        }

        public static bool operator <=(RationalNumber a, RationalNumber b)
        {
            var lcm = Lcm(a.Denom, b.Denom);
            var k1 = lcm / a.Denom;
            var k2 = lcm / b.Denom;
            return a.Num * k1 <= b.Num * k2;
        }

        public static bool operator >(RationalNumber a, RationalNumber b)
        {
            return b < a;
        }

        public static bool operator >=(RationalNumber a, RationalNumber b)
        {
            return b <= a;
        }

        [Pure]
        public int Ceil()
        {
            return (Num + Denom - 1)/Denom;
        }

        [Pure]
        public int Floor()
        {
            return Num/Denom;
        }

        private static int Gcd(int a, int b)
        {
            while (a != 0)
            {
                var a1 = a;
                a = b%a;
                b = a1;
            }
            return b;
        }

        private static int Lcm(int a, int b)
        {
            return a/Gcd(a, b)*b;
        }

        public override string ToString()
        {
            return String.Format("{0}/{1}", Num, Denom);
        }

        public readonly int Num;
        public readonly int Denom;
    }
}
