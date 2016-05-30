using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;

namespace RepetitionDetection.Tests
{
    [TestFixture]
    public class LargeRepetitionDetectorTests
    {
        [Test]
        public void Test()
        {
            var e = new RationalNumber(3, 2);
            var text = "wxyzaceorsuvaceo";
            var sb = new StringBuilder();

            var detector = new LargeRepetitionsDetector(sb, e, true);
            var repetitions = new List<Repetition>();

            for (var i = 0; i < text.Length; ++i)
            {
                Repetition repetition;
                sb.Append(text[i]);
                if (detector.TryDetect(out repetition))
                {
                    repetitions.Add(repetition);
                }
            }

            Assert.That(repetitions, Is.EquivalentTo(new []
            {
                new Repetition(3, 8), 
            }));
        }
    }
}
