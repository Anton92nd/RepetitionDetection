using RepetitionDetection.Logging;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace GraphicalInterface
{
    public class InitialSettings
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }
        public bool DetectEqualToExponent { get; set; }
        public int AlphabetSize { get; set; }
        public int GeneratedStringLength { get; set; }
        public int RunsCount { get; set; }
        public int CharGeneratorIndex { get; set; }
        public int RepetitionRemovingStrategyIndex { get; set; }
        public IRemoveStrategy RemoveStrategy { get; set; }
        public SaveData SaveData { get; set; }
    }
}
