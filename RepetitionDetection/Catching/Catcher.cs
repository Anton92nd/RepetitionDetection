using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            TimeToLive = timeToLive;
            h = new RationalNumber(j - i + 1, 2);
            pattern = text.ToString(i, h.Ceil());
            stringMatchingAlgorithm = new StringMatchingAlgorithm(text, pattern, i + 1);
        }

        public void WarmUp(int fromLength, int toLength)
        {
            var repetitions = new List<Repetition>();
            for (var textLength = fromLength; textLength < toLength; ++textLength)
            {
                repetitions = UpdateRepetitions(repetitions, textLength);
                if (stringMatchingAlgorithm.CheckForMatch(textLength))
                {
                    repetitions.Add(Update(new Repetition(I - 1, text.Length - h.Ceil() - I)));
                }
            }
            stateStack.Clear();
            stateStack.Push(new CatcherState(repetitions.ToImmutableArray(), stringMatchingAlgorithm.State));
        }

        public bool TryCatch(out Repetition foundRepetition)
        {
            var newRepetitions = UpdateRepetitions(stateStack.Peek().Repetitions, text.Length);
            if (stringMatchingAlgorithm.CheckForMatch(text.Length))
            {
                newRepetitions.Add(Update(new Repetition(I - 1, text.Length - h.Ceil() - I)));
            }
            foundRepetition = newRepetitions.FirstOrDefault(FoundRepetition);
            stateStack.Push(new CatcherState(newRepetitions.ToImmutableArray(), stringMatchingAlgorithm.State));
            return foundRepetition.Period > 0;
        }

        private bool FoundRepetition(Repetition repetition)
        {
            return text.Length - (repetition.LeftPosition + 1) - (detectEqual ? 0 : 1) >= (e*repetition.Period).Ceil();
        }

        private List<Repetition> UpdateRepetitions(IEnumerable<Repetition> repetitions, int textLength)
        {
            return repetitions
                .Where(rep => rep.Period >= textLength || text[textLength - 1] == text[textLength - 1 - rep.Period])
                .Select(Update)
                .ToList();
        }

        private Repetition Update(Repetition repetition)
        {
            var lp = repetition.LeftPosition;
            var r = ((e - 1) * repetition.Period / Math.Max(h.Floor(), 1)).Ceil();
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
            }
            stringMatchingAlgorithm.SetState(stateStack.Peek().StringMatchingState);
        }

        public bool IsActive()
        {
            return CreationTime <= text.Length && (DeletionTime < 0 || text.Length < DeletionTime);
        }

        public bool ShouldBeDeleted()
        {
            return (DeletionTime >= 0 && text.Length - DeletionTime > TimeToLive) ||
                   CreationTime - text.Length > TimeToLive;
        }

        public override string ToString()
        {
            return string.Format("Catcher {{I={0},J={1}}}", I, J);
        }

        private readonly int I, J;

        public int CreationTime { get; set; }
        public int DeletionTime { get; set; }

        [NotNull]
        private readonly string pattern;

        [NotNull]
        private readonly StringBuilder text;

        private readonly RationalNumber e;
        private readonly bool detectEqual;
        public readonly int TimeToLive;

        [NotNull]
        private readonly Stack<CatcherState> stateStack;

        [NotNull]
        private readonly StringMatchingAlgorithm stringMatchingAlgorithm;

        private readonly RationalNumber h;
    }
}
