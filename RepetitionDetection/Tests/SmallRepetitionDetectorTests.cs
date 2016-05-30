using System.Text;
using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;

namespace RepetitionDetection.Tests
{
    [TestFixture]
    public class SmallRepetitionDetectorTests
    {
        [TestCase(7, 6, "aba", -1, 2, true, true)]
        [TestCase(7, 6, "abc", 0, 0, true, false)]
        [TestCase(7, 4, "abacacc", 4, 1, true, true)]
        [TestCase(7, 4, "abacacc", 0, 0, false, false)]
        public void Test(int num,  int denom, string text, int expectedLeftPosition, int expectedPeriod, bool detectEqual, bool result)
        {
            var e = new RationalNumber(num, denom);
            var sb = new StringBuilder();

            var detector = new SmallRepetitionDetector(sb, e, detectEqual);

            Repetition repetition = new Repetition(0, 0);
            var detected = false;
            for (var i = 0; i < text.Length; ++i)
            {
                sb.Append(text[i]);
                if (detector.TryDetect(out repetition))
                {
                    detected = true;
                    break;
                }
            }

            Assert.That(detected, Is.EqualTo(result));
            Assert.That(repetition, Is.EqualTo(new Repetition(expectedLeftPosition, expectedPeriod)));
        }
    }
}
