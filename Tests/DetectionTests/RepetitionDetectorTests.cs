using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;

namespace Tests.DetectionTests
{
    [TestFixture]
    public class RepetitionDetectorTests
    {
        [TestCase("wxyzaceorsuvaceo", 3, 2, 3, 8, true)]
        [TestCase("xxxxaceorsuvaceo", 3, 2, -1, 1, true)]
        [TestCase("acbacb", 7, 4, -1, 3, true)]
        [TestCase("abacbabcabacbcabcbacbca", 7, 4, 8, 8, true)]
        [TestCase("dbacabaca", 2, 1, 0, 4, true)]
        [TestCase("badab", 2, 1, 0, 0, true)]
        public void Test(string text, int num, int denom, int lp, int p, bool detectEqual)
        {
            var e = new RationalNumber(num, denom);
            var sb = new StringBuilder();

            var detector = new RepetitionDetector(sb, e, detectEqual);
            var repetition = new Repetition(0, 0);

            foreach (var @char in text)
            {
                sb.Append(@char);
                if (detector.TryDetect(out repetition))
                {
                    break;
                }
            }

            Assert.That(repetition, Is.EqualTo(new Repetition(lp, p)));
        }

        [Test]
        public void TestWithBacktrack()
        {
            var e = new RationalNumber(3, 2);
            const string text = "wxyzaceorsuvaceo";
            var sb = new StringBuilder();

            var detector = new LargeRepetitionDetector(sb, e, true);
            var repetition = new Repetition(0, 0);

            foreach (var @char in text)
            {
                sb.Append(@char);
                if (detector.TryDetect(out repetition))
                {
                    break;
                }
            }

            Assert.That(repetition, Is.EqualTo(new Repetition(3, 8)));

            detector.Backtrack();
            sb.Remove(sb.Length - 1, 1);
            sb.Append('p');
            Assert.That(detector.TryDetect(out repetition), Is.False);

            detector.Backtrack();
            sb.Remove(sb.Length - 1, 1);
            sb.Append('o');
            Assert.That(detector.TryDetect(out repetition), Is.True);
            Assert.That(repetition, Is.EqualTo(new Repetition(3, 8)));
        }

        [Test]
        public void TestBacktrack()
        {
            const string text = "acbaca";
            var sb = new StringBuilder();
            var detector = new RepetitionDetector(sb, new RationalNumber(7, 4), true);
            Repetition rep;
            foreach (var c in text)
            {
                sb.Append(c);
                detector.TryDetect(out rep);
            }
            while (sb.Length > 3)
            {
                detector.Backtrack();
                sb.Remove(sb.Length - 1, 1);
            }
            var reps = new List<Repetition>();
            foreach (var c in "cab")
            {
                sb.Append(c);
                if (detector.TryDetect(out rep))
                    reps.Add(rep);
            }
            Assert.That(reps, Is.Empty);
        }
    }
}
