using System.Text;
using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;

namespace RepetitionDetection.Tests
{
    [TestFixture]
    public class SmallRepetitionDetectorTests
    {
        [TestCase(7, 4, "abacaba", -1, 4, true)]
        [TestCase(3, 2, "aba", -1, 2, true)]
        [TestCase(7, 4, "abacabac", -1, 4, false)]
        [TestCase(3, 2, "xxxxaceorsuvaceo", 3, 8, true)]
        [TestCase(3, 2, "xxxxaceorsuvaceor", 3, 8, false)]
        public void Test(int num,  int denom, string text, int expectedLeftPosition, int expectedPeriod, bool detectEqual)
        {
            var e = new RationalNumber(num, denom);
            var sb = new StringBuilder(text);

            var detector = new SmallRepetitionDetector(sb, e, text.Length, detectEqual);
            Repetition repetition;

            Assert.That(detector.TryDetect(out repetition), Is.True);
            Assert.That(repetition, Is.EqualTo(new Repetition(expectedLeftPosition, expectedPeriod)));
        }
    }
}
