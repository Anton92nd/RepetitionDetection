﻿using System;
using JetBrains.Annotations;
using RepetitionDetection.Commons;
using RepetitionDetection.MaximalSuffixes;

namespace RepetitionDetection.CriticalFactorization
{
    public class PrefixFactorizer
    {
        public PrefixFactorizer([NotNull] string word)
        {
            maximalSuffixCalculatorForLess = new MaximalSuffixCalculator(word, new CharLessComparer());
            maximalSuffixCalculatorForGreater = new MaximalSuffixCalculator(word, new CharGreaterComparer());
        }

        public void Factorize(int prefixLength)
        {
            if (prefixLength <= 1)
                throw new InvalidProgramStateException("Prefix length must be greater than 1");
            PrefixLength = prefixLength;
            maximalSuffixCalculatorForLess.Calculate(prefixLength);
            maximalSuffixCalculatorForGreater.Calculate(prefixLength);
        }

        public int CriticalPosition => Math.Max(1,
            Math.Max(maximalSuffixCalculatorForLess.MaximalSuffixPosition,
                maximalSuffixCalculatorForGreater.MaximalSuffixPosition));

        public int PrefixLength { get; private set; }

        [NotNull] private readonly MaximalSuffixCalculator maximalSuffixCalculatorForGreater;

        [NotNull] private readonly MaximalSuffixCalculator maximalSuffixCalculatorForLess;
    }
}