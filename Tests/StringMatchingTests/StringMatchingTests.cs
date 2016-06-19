using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RepetitionDetection.StringMatching;

namespace Tests.StringMatchingTests
{
    [TestFixture]
    public class StringMatchingTests
    {
        [TestCase("abacabadabacaba", "aba", new []{2, 6, 10, 14})]
        [TestCase("aaaaa", "a", new []{ 0, 1, 2, 3, 4})]
        [TestCase("ababababa", "aba", new []{2, 4, 6, 8})]
        [TestCase("babaaababaaabaaaababaaa", "aababaaa", new []{11, 22})]
        [TestCase("aaaaabcbcb", "aaaabc", new []{6})]
        public void Test(string text, string pattern, int[] expectedOccurences)
        {
            var sb = new StringBuilder();
            var algorithm = new StringMatchingAlgorithm(sb, pattern, 0);

            var occurences = new List<int>();
            for (var i = 0; i < text.Length; ++i)
            {
                sb.Append(text[i]);
                if (algorithm.CheckForMatch(i + 1))
                {
                    occurences.Add(i);
                }
            }
            Assert.That(occurences, Is.EquivalentTo(expectedOccurences));
        }

        [Test]
        public void TestWithBackTrack()
        {
            var sb = new StringBuilder("abacab");
            const string pattern = "aba";
            var algo = new StringMatchingAlgorithm(sb, pattern, 0);
            for (var i = 0; i < sb.Length; i++)
            {
                algo.CheckForMatch(i + 1);
            }
            var abacabState = algo.State;
            sb.Append('a');
            Assert.That(algo.CheckForMatch(sb.Length), Is.True);
            sb.Remove(sb.Length - 1, 1);

            algo.SetState(abacabState);
            sb.Append('a');
            Assert.That(algo.CheckForMatch(sb.Length), Is.True);
            sb.Remove(sb.Length - 1, 1);

            algo.SetState(abacabState);
            sb.Append('c');
            Assert.That(algo.CheckForMatch(sb.Length), Is.False);
        }
    }
}
