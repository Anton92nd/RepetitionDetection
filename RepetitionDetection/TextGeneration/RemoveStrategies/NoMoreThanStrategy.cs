using System;
using RepetitionDetection.Commons;

namespace RepetitionDetection.TextGeneration.RemoveStrategies
{
    public class NoMoreThanStrategy : IRemoveStrategy
    {
        public NoMoreThanStrategy(IRemoveStrategy baseStrategy, int maxCharactersToRemove)
        {
            BaseStrategy = baseStrategy;
            MaxCharactersToRemove = maxCharactersToRemove;
        }

        public int GetCharsToDelete(int textLength, Repetition repetition)
        {
            return Math.Min(MaxCharactersToRemove, BaseStrategy.GetCharsToDelete(textLength, repetition));
        }

        public override string ToString()
        {
            return $"NoMoreThanStrategy({MaxCharactersToRemove}, {BaseStrategy})";
        }

        public IRemoveStrategy BaseStrategy { get; set; }
        public int MaxCharactersToRemove { get; set; }
    }
}