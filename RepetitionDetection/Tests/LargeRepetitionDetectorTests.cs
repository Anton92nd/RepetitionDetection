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

        [Test]
        public void TestWithBacktrack()
        {
            var e = new RationalNumber(3, 2);
            var text = "wxyzaceorsuvaceo";
            var sb = new StringBuilder();

            var detector = new LargeRepetitionsDetector(sb, e, true);
            var repetitions = new List<Repetition>();

            Repetition repetition;
            for (var i = 0; i < text.Length; ++i)
            {
                
                sb.Append(text[i]);
                if (detector.TryDetect(out repetition))
                {
                    repetitions.Add(repetition);
                }
            }

            Assert.That(repetitions, Is.EquivalentTo(new[]
            {
                new Repetition(3, 8), 
            }));

            detector.BackTrack();
            sb.Remove(sb.Length - 1, 1);
            sb.Append('p');
            Assert.That(detector.TryDetect(out repetition), Is.False);

            detector.BackTrack();
            sb.Remove(sb.Length - 1, 1);
            sb.Append('o');
            Assert.That(detector.TryDetect(out repetition), Is.True);
            Assert.That(repetition, Is.EqualTo(new Repetition(3, 8)));
        }
    }
}
