﻿using RepetitionDetection.Commons;

namespace RepetitionDetection.TextGeneration.RemoveStrategies
{
    public class RemovePeriodMultipleStrategy : IRemoveStrategy
    {
        private readonly int periodsCount;

        public RemovePeriodMultipleStrategy(int periodsCount = 1)
        {
            this.periodsCount = periodsCount;
        }

        public int GetCharsToDelete(int textLength, Repetition repetition)
        {
            return repetition.Period*periodsCount;
        }

        public override string ToString()
        {
            return string.Format("Remove {0} period(s) strategy", periodsCount);
        }
    }
}
