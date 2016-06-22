using System;

namespace RepetitionDetection.CharGenerators
{
    public class RandomCharGenerator : ICharGenerator
    {
        private readonly int alphabetSize;
        private readonly Random random;

        public int AlphabetSize { get { return alphabetSize; } }

        public override string ToString()
        {
            return "RandomCharGenerator";
        }

        public RandomCharGenerator(int alphabetSize)
        {
            this.alphabetSize = alphabetSize;
            random = new Random();
        }

        public char Generate()
        {
            return (char)random.Next('a', 'a' + alphabetSize);
        }
    }
}
