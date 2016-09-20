using System;
using System.Text;

namespace RepetitionDetection.CharGenerators
{
    public class RandomNotLastCharGenerator : ICharGenerator
    {
        private readonly StringBuilder text;
        private readonly int alphabetSize;

        public int AlphabetSize { get { return alphabetSize; } }

        public override string ToString()
        {
            return "RandomNotLastCharGenerator";
        }

        public RandomNotLastCharGenerator(StringBuilder text, int alphabetSize)
        {
            this.text = text;
            this.alphabetSize = alphabetSize;
        }

        public char Generate()
        {
            if (text.Length == 0)
                return (char)('a' + RandomNumberGenerator.Generate(0, alphabetSize));
            var rand = RandomNumberGenerator.Generate(0, alphabetSize - 1);
            var lastChar = text[text.Length - 1] - 'a';
            return (char) ('a' + rand + (rand >= lastChar ? 1 : 0));
        }
    }
}
