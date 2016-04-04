using NUnit.Framework;

namespace RepetitionDetection.Tests
{
    [TestFixture]
    public abstract class MaximalSuffixCalculatorTestBase
    {
        [SetUp]
        public abstract void SetUp();

        [Test]
        public void TestEmpty()
        {
            Assert.That(maximalSuffixCalculator.GetPosition(), Is.EqualTo(0));
        }

        [Test]
        public void TestOneLetter()
        {
            maximalSuffixCalculator.Append('a');
            Assert.That(maximalSuffixCalculator.GetPosition(), Is.EqualTo(0));
        }

        protected MaximalSuffixCalculator maximalSuffixCalculator;
    }
}
