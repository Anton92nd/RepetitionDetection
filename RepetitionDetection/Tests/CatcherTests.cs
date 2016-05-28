using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RepetitionDetection.Catching;
using RepetitionDetection.Commons;

namespace RepetitionDetection.Tests
{
    [TestFixture]
    public class CatcherTests
    {
        [Test]
        public void Test1()
        {
            var text = "xxxxaceorsuvaceo";
            var e = new RationalNumber(3, 2);
            var sb = new StringBuilder();
            sb.Append(text.Substring(0, 8));

            var catcher = new Catcher(sb, 5, 6, e);
            var detections = new List<int>();
            for (var i = 8; i < text.Length; ++i)
            {
                sb.Append(text[i]);
                if (catcher.TryCatch())
                {
                    detections.Add(i);
                }
            }
            Assert.That(detections, Is.EquivalentTo(new []{15}));
        }

        [Test]
        public void Test2()
        {
            var text = "xxxvaceorsuvace";
            var e = new RationalNumber(3, 2);
            var sb = new StringBuilder();
            sb.Append(text.Substring(0, 8));

            var catcher = new Catcher(sb, 5, 6, e);
            var detections = new List<int>();
            for (var i = 8; i < text.Length; ++i)
            {
                sb.Append(text[i]);
                if (catcher.TryCatch())
                {
                    detections.Add(i);
                }
            }
            Assert.That(detections, Is.EquivalentTo(new[] { 14 }));
        }

        [Test]
        public void TestWithBackTrack()
        {
            var text = "xxxxaceorsuvaceo";
            var e = new RationalNumber(3, 2);
            var sb = new StringBuilder();
            sb.Append(text.Substring(0, 8));

            var catcher = new Catcher(sb, 5, 6, e);
            var detections = new List<int>();
            for (var i = 8; i < text.Length; ++i)
            {
                sb.Append(text[i]);
                if (catcher.TryCatch())
                {
                    detections.Add(i);
                }
            }           
            catcher.Backtrack();
            sb.Remove(sb.Length - 1, 1);
            sb.Append('c');
            Assert.That(catcher.TryCatch(), Is.False);
            sb.Remove(sb.Length - 1, 1);
            sb.Append('o');
            catcher.Backtrack();
            Assert.That(catcher.TryCatch(), Is.True);
        }
    }
}
