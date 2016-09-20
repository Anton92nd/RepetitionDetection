using System;

namespace RepetitionDetection.CharGenerators
{
    public class RandomCharGenerator : ICharGenerator
    {
        private readonly int alphabetSize;

        public int AlphabetSize { get { return alphabetSize; } }

        public override string ToString()
        {
            return "RandomCharGenerator";
        }

        public RandomCharGenerator(int alphabetSize)
        {
            this.alphabetSize = alphabetSize;
        }

        public char Generate()
        {
            return (char)RandomNumberGenerator.Generate('a', 'a' + alphabetSize);
        }
    }
}
