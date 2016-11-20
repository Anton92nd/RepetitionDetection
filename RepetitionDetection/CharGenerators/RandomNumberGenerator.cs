using System.Linq;
using System.Security.Cryptography;

namespace RepetitionDetection.CharGenerators
{
    public static class RandomNumberGenerator
    {
        public static int Generate(int minValue, int maxValue)
        {
            return minValue + Generate(maxValue - minValue);
        }

        public static int Generate(int maxValue)
        {
            return GenerateInt()%maxValue;
        }

        private static int GenerateInt()
        {
            var intBytes = new byte[4];
            rngCsp.GetBytes(intBytes);
            intBytes[0] |= 1 << 7;
            intBytes[0] ^= 1 << 7;
            return intBytes.Aggregate(0, (current, @byte) => (current << 8) + @byte);
        }

        private static readonly RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
    }
}
