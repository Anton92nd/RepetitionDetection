namespace RepetitionDetection.StringMatching
{
    public struct AlgorithmState
    {
        public readonly int PositionInText;
        public readonly int MatchedSymbolsCount;

        public AlgorithmState(int positionInText, int matchedSymbolsCount)
        {
            PositionInText = positionInText;
            MatchedSymbolsCount = matchedSymbolsCount;
        }

        public override string ToString()
        {
            return string.Format("Position: {0}, Matched: {1}", PositionInText, MatchedSymbolsCount);
        }
    }
}
