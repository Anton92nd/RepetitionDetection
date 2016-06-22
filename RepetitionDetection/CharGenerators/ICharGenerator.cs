namespace RepetitionDetection.CharGenerators
{
    public interface ICharGenerator
    {
        char Generate();
        int AlphabetSize { get; }
    }
}