using System.Text;
using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;

namespace Tests.DetectionTests
{
    [TestFixture]
    public class LargeRepetitionDetectorTests
    {
        [TestCase("wxyzaceorsuvaceo", 3, 2, 3, 8, true)]
        [TestCase("wxyzaceorsuvaceor", 3, 2, 3, 8, false)]
        public void Test(string text, int num, int denom, int lp, int p, bool detectEqual)
        {
            var e = new RationalNumber(num, denom);
            var sb = new StringBuilder();

            var detector = new LargeRepetitionDetector(sb, e, detectEqual);
            var repetition = new Repetition(0, 0);

            foreach (var c in text)
            {
                sb.Append(c);
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

            foreach (var c in text)
            {
                sb.Append(c);
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
    }
}
