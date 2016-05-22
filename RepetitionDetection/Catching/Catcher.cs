using System.Collections.Generic;
using System.Text;
using RepetitionDetection.StringMatching;

namespace RepetitionDetection.Catching
{
    public class Catcher
    {
        public bool Removed { get; set; }
        private readonly string pattern;
        private readonly StringBuilder text;
        private readonly Stack<CatcherState> stateStack;
        private readonly StringMatchingAlgorithm stringMatchingAlgorithm;

        public Catcher(StringBuilder text, int i, int j)
        {
            stateStack = new Stack<CatcherState>();
            this.text = text;
            var hFloor = (i - j + 1)/2;
            var hCeil = hFloor + (i - j + 1)%2;
            pattern = text.ToString(i, hCeil);
            Removed = false;
            stringMatchingAlgorithm = new StringMatchingAlgorithm(text, pattern);
            stateStack.Push(InitialState());
        }
        
        private CatcherState InitialState()
        {
            return null;
        }
    }
}
