using NUnit.Framework;
using RepetitionDetection.MSCalculator;

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
            var data = MSCalculatorData.Default;
            maximalSuffixCalculator.Calculate(string.Empty, data);
            Assert.That(data.MSPosition, Is.EqualTo(0));
        }

        [Test]
        public void TestOneLetter()
        {
            var data = MSCalculatorData.Default;
            maximalSuffixCalculator.Calculate("a", data);
            Assert.That(data.MSPosition, Is.EqualTo(0));
        }

        protected MaximalSuffixCalculator maximalSuffixCalculator;
    }
}
