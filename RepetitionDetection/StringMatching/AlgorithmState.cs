namespace RepetitionDetection.StringMatching
{
    public class AlgorithmState
    {
        public int PositionInText { get; private set; }
        public int MatchedSymbolsCount { get; private set; }

        public AlgorithmState(int positionInText, int matchedSymbolsCount)
        {
            PositionInText = positionInText;
            MatchedSymbolsCount = matchedSymbolsCount;
        }
    }
}
