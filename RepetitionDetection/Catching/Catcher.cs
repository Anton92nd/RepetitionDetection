using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using RepetitionDetection.Commons;
using RepetitionDetection.StringMatching;

namespace RepetitionDetection.Catching
{
    public class Catcher
    {
        public Catcher([NotNull] StringBuilder text, int i, int j, RationalNumber e, bool detectEqual, int timeToLive)
        {
            I = i;
            J = j;
            stateStack = new Stack<CatcherState>();
            this.text = text;
            this.e = e;
            this.detectEqual = detectEqual;
            this.timeToLive = timeToLive;
            RemoveTime = -1;
            h = new RationalNumber(j - i + 1, 2);
            var pattern = text.ToString(i, h.Ceil());
            stringMatchingAlgorithm = new StringMatchingAlgorithm(text, pattern, i + 1);
        }

        public void WarmUp(int fromLength, int toLength)
        {
            var repetitions = new List<Repetition>();
            stateStack.Clear();
            for (var textLength = fromLength; textLength < toLength; ++textLength)
            {
                repetitions = UpdateRepetitions(repetitions, textLength);
                if (stringMatchingAlgorithm.CheckForMatch(textLength))
                    repetitions.Add(UpdateRepetition(new Repetition(I - 1, textLength - h.Ceil() - I)));
                stateStack.Push(new CatcherState(repetitions, stringMatchingAlgorithm.State));
            }
            if (stateStack.Count == 0)
                stateStack.Push(new CatcherState(repetitions, stringMatchingAlgorithm.State));
        }

        public bool TryCatch(out Repetition foundRepetition)
        {
            var newRepetitions = UpdateRepetitions(stateStack.Peek().Repetitions, text.Length);
            if (stringMatchingAlgorithm.CheckForMatch(text.Length))
                newRepetitions.Add(UpdateRepetition(new Repetition(I - 1, text.Length - h.Ceil() - I)));
            foundRepetition = newRepetitions.FirstOrDefault(FoundRepetition);
            stateStack.Push(new CatcherState(newRepetitions, stringMatchingAlgorithm.State));
            return foundRepetition.Period > 0;
        }

        private bool FoundRepetition(Repetition repetition)
        {
            var exponent = new RationalNumber(text.Length - (repetition.LeftPosition + 1), repetition.Period);
            return detectEqual ? exponent >= e : exponent > e;
        }

        private List<Repetition> UpdateRepetitions(List<Repetition> repetitions, int textLength)
        {
            return repetitions
                .Where(rep => rep.Period >= textLength || text[textLength - 1] == text[textLength - 1 - rep.Period])
                .Select(UpdateRepetition)
                .ToList();
        }

        private Repetition UpdateRepetition(Repetition repetition)
        {
            var lp = repetition.LeftPosition;
            var r = ((e - 1) * repetition.Period / Math.Max(h.Floor(), 1)).Ceil();
            while (lp >= 0 && r > 0 && text[lp] == text[lp + repetition.Period])
            {
                lp--;
                r--;
            }
            return new Repetition(lp, repetition.Period);
        }

        public void Backtrack()
        {
            if (stateStack.Count > 1)
                stateStack.Pop();
            stringMatchingAlgorithm.State = stateStack.Peek().StringMatchingState;
        }

        public bool IsActive()
        {
            return RemoveTime < 0 || text.Length < RemoveTime;
        }

        public bool IsObsolete()
        {
            return text.Length <= J + 1 || RemoveTime >= 0 && Math.Abs(RemoveTime - text.Length) > timeToLive;
        }

        public override string ToString()
        {
            return $"Catcher {{I={I},J={J}}}";
        }

        private readonly bool detectEqual;

        private readonly RationalNumber e;

        private readonly RationalNumber h;

        private readonly int I, J;

        [NotNull] private readonly Stack<CatcherState> stateStack;

        [NotNull] private readonly StringMatchingAlgorithm stringMatchingAlgorithm;

        [NotNull] private readonly StringBuilder text;

        private readonly int timeToLive;

        public int RemoveTime;
    }
}