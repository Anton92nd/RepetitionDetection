using System.Collections.Generic;
using System.Text;

namespace RepetitionDetection.Catching
{
    public class Catcher
    {
        public bool Removed { get; set; }
        private readonly string str;
        private readonly StringBuilder stringBuilder;
        private readonly Stack<CatcherState> stateStack;

        public Catcher(StringBuilder stringBuilder, int i, int j)
        {
            stateStack = new Stack<CatcherState>();
            this.stringBuilder = stringBuilder;
            var hLow = (i - j + 1)/2;
            var hHigh = hLow + (i - j + 1)%2;
            str = stringBuilder.ToString(i, hHigh);
            Removed = false;
            stateStack.Push(InitialState());
        }
        
        private CatcherState InitialState()
        {
            return null;
        }
    }
}
