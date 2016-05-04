using System.Text;
using NUnit.Framework;
using RepetitionDetection.MSCalculator;

namespace RepetitionDetection.Tests
{
    public abstract class MaximalSuffixCalculatorTestBase
    {
        public abstract void SetUp();

        [Test]
        public void TestEmpty()
        {
            var data = MSCalculatorData.Default;
            var result = maximalSuffixCalculator.Calculate(new StringBuilder(), data);
            Assert.That(result.MSPosition, Is.EqualTo(0));
        }

        [Test]
        public void TestOneLetter()
        {
            var sb = new StringBuilder("a");
            var data = MSCalculatorData.Default;
            var result = maximalSuffixCalculator.Calculate(sb, data);
            Assert.That(result.MSPosition, Is.EqualTo(0));
        }

        protected MaximalSuffixCalculator maximalSuffixCalculator;
    }
}
