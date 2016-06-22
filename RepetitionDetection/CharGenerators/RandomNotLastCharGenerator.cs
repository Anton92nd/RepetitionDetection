using System;
using System.Text;

namespace RepetitionDetection.CharGenerators
{
    public class RandomNotLastCharGenerator : ICharGenerator
    {
        private readonly StringBuilder text;
        private readonly int alphabetSize;
        private readonly Random random;

        public int AlphabetSize { get { return alphabetSize; } }

        public override string ToString()
        {
            return "RandomNotLastCharGenerator";
        }

        public RandomNotLastCharGenerator(StringBuilder text, int alphabetSize)
        {
            this.text = text;
            this.alphabetSize = alphabetSize;
            random = new Random();
        }

        public char Generate()
        {
            if (text.Length == 0)
                return (char) ('a' + random.Next(0, alphabetSize));
            var rand = random.Next(0, alphabetSize - 1);
            var lastChar = text[text.Length - 1] - 'a';
            return (char) ('a' + rand + (rand >= lastChar ? 1 : 0));
        }
    }
}
