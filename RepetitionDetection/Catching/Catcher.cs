using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using RepetitionDetection.Commons;
using RepetitionDetection.StringMatching;

namespace RepetitionDetection.Catching
{
    public class Catcher
    {
        public int I { get; private set; }
        public int J { get; private set; }
        public bool Removed { get; set; }
        private readonly string pattern;
        private readonly StringBuilder text;
        private readonly RationalNumber e;
        private readonly Stack<CatcherState> stateStack;
        private readonly StringMatchingAlgorithm stringMatchingAlgorithm;
        private readonly RationalNumber h;

        public Catcher(StringBuilder text, int i, int j, RationalNumber e)
        {
            I = i;
            J = j;
            stateStack = new Stack<CatcherState>();
            this.text = text;
            this.e = e;
            h = new RationalNumber(i - j + 1, 2);
            pattern = text.ToString(i, h.Ceil());
            Removed = false;
            stringMatchingAlgorithm = new StringMatchingAlgorithm(text, pattern, i + 1);
            stateStack.Push(new CatcherState(ImmutableArray<Repetition>.Empty, stringMatchingAlgorithm.State));
        }

        public bool TryCatch()
        {
            var result = false;
            var newRepetitions = new List<Repetition>();
            if (stringMatchingAlgorithm.CheckForMatch())
            {
                newRepetitions.Add(Update(new Repetition(I - 1, text.Length - h.Ceil() + 1 - I)));
            }
            foreach (var repetition in stateStack.Peek().Repetitions)
            {
                if (text[text.Length - 1] != text[text.Length - 1 - repetition.Period])
                    continue;
                var newRepetition = Update(repetition);
                if (text.Length - newRepetition.LeftPosition >= (e*newRepetition.Period).Ceil())
                    result = true;
                newRepetitions.Add(newRepetition);
            }
            stateStack.Push(new CatcherState(newRepetitions.ToImmutableArray(), stringMatchingAlgorithm.State));
            return result;
        }

        private Repetition Update(Repetition repetition)
        {
            var lp = repetition.LeftPosition;
            var r = ((e - 1) * repetition.Period / h.Floor()).Ceil();
            while (lp > 0 && r > 0 && text[lp] == text[lp + repetition.Period])
            {
                lp--;
                r--;
            }
            return new Repetition(lp, repetition.Period);
        }

        public void Backtrack()
        {
            if (stateStack.Count > 1)
            {
                stateStack.Pop();
                stringMatchingAlgorithm.SetState(stateStack.Peek().StringMatchingState);
            }
        }
    }
}
