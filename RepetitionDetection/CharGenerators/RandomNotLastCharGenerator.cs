using System.Text;

namespace RepetitionDetection.CharGenerators
{
    public class RandomNotLastCharGenerator : ICharGenerator
    {
        public RandomNotLastCharGenerator(StringBuilder text, int alphabetSize)
        {
            this.text = text;
            AlphabetSize = alphabetSize;
        }

        public int AlphabetSize { get; }

        public char Generate()
        {
            if (text.Length == 0)
                return (char) ('a' + RandomNumberGenerator.Generate(0, AlphabetSize));
            var rand = RandomNumberGenerator.Generate(0, AlphabetSize - 1);
            var lastChar = text[text.Length - 1] - 'a';
            return (char) ('a' + rand + (rand >= lastChar ? 1 : 0));
        }

        private readonly StringBuilder text;

        public override string ToString()
        {
            return "RandomNotLastCharGenerator";
        }
    }
}