using NUnit.Framework;
using RepetitionDetection.CriticalFactorization;

namespace Tests
{
    [TestFixture]
    public class CriticalFactorizationTests
    {
        [TestCase("aaa", 1)]
        [TestCase("aba", 1)]
        [TestCase("abaaaba", 2)]
        [TestCase("aaaba", 3)]
        [TestCase("abcdefgh", 7)]
        [TestCase("abcabcdabc", 6)]
        public void PrefixFactorizerTest(string template, int expectedPosition)
        {
            var factorizer = new PrefixFactorizer(template);
            factorizer.Factorize(template.Length);
            Assert.That(factorizer.CriticalPosition, Is.EqualTo(expectedPosition));
        }

        [TestCase("aaa", 0, 0, 1)]       // (a, aa)
        [TestCase("aba", 0, 0, 1)]       // (a, ba)
        [TestCase("abaaaba", 3, 1, 2)]   // (a, ba) (ab, aaaba)
        [TestCase("aaaba", 3, 1, 3)]     // (a, aa) (aaa, ba)
        [TestCase("abcdefgh", 6, 3, 4)]  // (abc, def) (abcd, efgh)
        [TestCase("abcabcdabc", 6, 2, 6)]// (ab, cabc) (abcabc, dabc)
        [TestCase("aaaaab", 5, 1, 5)]    // (a, aaaa) (aaaaa, b)
        [TestCase("aaaaabc", 5, 1, 5)]   // (a, aaaa) (aaaaa, bc)
        [TestCase("aaabababaabaabbaabbbabbaaaababbababbaaaababaabaabbabaaabaaaabbaaabaaaaaaaaaabbabaababaabbbabbbababaabbbbaaaabbbbabbaabbbabbbbaabbaabababbbbbaababbbaaaaaaaaaaaabbaabaababaaaaabbbabbabbaabbaaaabbbaabaaaaaabbbaaaaabbbabbbbbbabbaaaaabaaabbaabaaaaaaabaababbaababbbabababbbbbabababbabaaabbaababbaaaabababbbbaaaabaabbabbababaaabaaaaaabbbabaaaabaaababbbbbabbbaaabbaaaaaaaaaaabbbaabbbbababbabbaabaaabaaababbbbbababaaaabbbaabbabbbbaabababbaabbaaaabaaababaabbabbbbbababaaabaabaabbbbbbbabbaaaaaabaabbbabbbabbaabaabbabaababbbbabbabbbaabbaaabaaabbbbaabaaababababbabababbabbbbbbabbbbbaabbabbbbbbbbaaaaabbaaabbaababbbbaabbaaabbabbabbbabbaaaaaaabbababbbabaaababbbbbbabbaaabaaaababaabababaaabaabbaabbaaabaaabaaabaaabbbbaaaaabaaababaabbaababbbabababbbbbbabbbbbabaabbaaabbbbbbbbbababbbbabbaababbaabbabbabaaabbbbaabbaaabaaabbabbbbbbabbbbbabaababbabbababbbbababbbbaaabababbaaaababbabaabbbabbaaaaaaaabaaaaababbbabaaabbaabaababaabbbbaaabbabbaaaabbbabbaaaabaaabaaabbbaabbbabbbaababbbaaabaabbbbabbaaaababbaababababbbabbababbbb", 
            934, 467, 574)]
        public void FactorizerTest(string pattern, int prefixLength, int prefixCriticalPosition, int patternCriticalPosition)
        {
            var factorizations = pattern.GetFactorizations();
            Assert.That(factorizations, Is.EqualTo(new Factorizations(pattern, prefixLength, prefixCriticalPosition, patternCriticalPosition)));
        }
    }
}
