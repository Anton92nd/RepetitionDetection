using System.Text;
using NUnit.Framework;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Commons;

namespace Tests.TextGenerationTests
{
    [TestFixture]
    public class CleverCharGeneratorTests
    {
        [Test]
        public void TestBoundary()
        {
            var sb = new StringBuilder();
            for (var i = 5; i <= 10; i++)
            {
                var charGenerator = new CleverCharGenerator(sb, i, new RationalNumber(i, i - 1), false);
                Assert.That(charGenerator.LastSymbols, Is.EqualTo(i - 2));
            }
        }

        [TestCase(3, 7, 4, false, 1)]
        [TestCase(4, 7, 5, false, 2)]
        [TestCase(2, 2, 1, true, 1)]
        [TestCase(2, 3, 1, true, 0)]
        [TestCase(4, 3, 2, true, 2)]
        public void TestOther(int alphabetSize, int numerator, int denominator, bool detectEqual, int expectedLastSymols)
        {
            var charGenerator = new CleverCharGenerator(new StringBuilder(), alphabetSize, new RationalNumber(numerator, denominator), detectEqual);
            Assert.That(charGenerator.LastSymbols, Is.EqualTo(expectedLastSymols));
        }
    }
}
