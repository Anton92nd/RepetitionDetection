using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RepetitionDetection.Catching;
using RepetitionDetection.Commons;

namespace Tests
{
    [TestFixture]
    public class CatcherTests
    {
        [Test]
        public void TestEqual1()
        {
            var text = "xxxxaceorsuvaceo";
            var e = new RationalNumber(3, 2);
            var sb = new StringBuilder();
            sb.Append(text.Substring(0, 9));

            var catcher = new Catcher(sb, 5, 6, e, true, 200);
            catcher.WarmUp(8, 9);
            var repetitions = new List<Repetition>();
            for (var i = 9; i < text.Length; ++i)
            {
                sb.Append(text[i]);
                Repetition rep;
                if (catcher.TryCatch(out rep))
                {
                    repetitions.Add(rep);
                }
            }
            Assert.That(repetitions, Is.EquivalentTo(new []
            {
                new Repetition(3, 8), 
            }));
        }

        [Test]
        public void TestEqual2()
        {
            var text = "xxxvaceorsuvace";
            var e = new RationalNumber(3, 2);
            var sb = new StringBuilder();
            sb.Append(text.Substring(0, 9));

            var catcher = new Catcher(sb, 5, 6, e, true, 200);
            catcher.WarmUp(8, 9);
            var repetitions = new List<Repetition>();
            for (var i = 9; i < text.Length; ++i)
            {
                sb.Append(text[i]);
                Repetition rep;
                if (catcher.TryCatch(out rep))
                {
                    repetitions.Add(rep);
                }
            }
            Assert.That(repetitions, Is.EquivalentTo(new[]
            {
                new Repetition(2, 8)
            }));
        }

        [Test]
        public void TestLarger1()
        {
            var text = "xxxxaceorsuvaceor";
            var e = new RationalNumber(3, 2);
            var sb = new StringBuilder();
            sb.Append(text.Substring(0, 9));

            var catcher = new Catcher(sb, 5, 6, e, false, 200);
            catcher.WarmUp(8, 9);
            var repetitions = new List<Repetition>();
            for (var i = 9; i < text.Length; ++i)
            {
                sb.Append(text[i]);
                Repetition rep;
                if (catcher.TryCatch(out rep))
                {
                    repetitions.Add(rep);
                }
            }
            Assert.That(repetitions, Is.EquivalentTo(new[]
            {
                new Repetition(3, 8), 
            }));
        }

        [Test]
        public void TestLarger2()
        {
            var text = "xxxvaceorsuvaceo";
            var e = new RationalNumber(3, 2);
            var sb = new StringBuilder();
            sb.Append(text.Substring(0, 9));

            var catcher = new Catcher(sb, 5, 6, e, false, 200);
            catcher.WarmUp(8, 9);
            var repetitions = new List<Repetition>();
            for (var i = 9; i < text.Length; ++i)
            {
                sb.Append(text[i]);
                Repetition rep;
                if (catcher.TryCatch(out rep))
                {
                    repetitions.Add(rep);
                }
            }
            Assert.That(repetitions, Is.EquivalentTo(new[]
            {
                new Repetition(2, 8)
            }));
        }

        [Test]
        public void TestEqualWithBackTrack()
        {
            var text = "xxxxaceorsuvaceo";
            var e = new RationalNumber(3, 2);
            var sb = new StringBuilder();
            sb.Append(text.Substring(0, 9));

            var catcher = new Catcher(sb, 5, 6, e, true, 200);
            catcher.WarmUp(8, 9);
            Repetition rep;
            for (var i = 9; i < text.Length; ++i)
            {
                sb.Append(text[i]);
                catcher.TryCatch(out rep);
            }           
            catcher.Backtrack();
            sb.Remove(sb.Length - 1, 1);
            sb.Append('c');
            Assert.That(catcher.TryCatch(out rep), Is.False);
            sb.Remove(sb.Length - 1, 1);
            sb.Append('o');
            catcher.Backtrack();
            Assert.That(catcher.TryCatch(out rep), Is.True);
            Assert.That(rep, Is.EqualTo(new Repetition(3, 8)));
        }
    }
}
