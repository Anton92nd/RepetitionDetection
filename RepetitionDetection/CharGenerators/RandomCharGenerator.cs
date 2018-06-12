namespace RepetitionDetection.CharGenerators
{
    public class RandomCharGenerator : ICharGenerator
    {
        public RandomCharGenerator(int alphabetSize)
        {
            AlphabetSize = alphabetSize;
        }

        public int AlphabetSize { get; }

        public char Generate() => (char) RandomNumberGenerator.Generate('a', 'a' + AlphabetSize);

        public override string ToString() => "RandomCharGenerator";
    }
}