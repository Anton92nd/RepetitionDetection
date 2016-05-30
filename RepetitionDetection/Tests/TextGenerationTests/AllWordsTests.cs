using NUnit.Framework;
using RepetitionDetection.Commons;
using RepetitionDetection.TextGeneration;

namespace RepetitionDetection.Tests.TextGenerationTests
{
    [TestFixture]
    public class AllWordsTests
    {
        public static void Main()
        {
            new AllWordsTests().Test1();
        }

        [Test]
        public void Test1()
        {
            var result = AllWordsGenerator.Generate(3, new RationalNumber(7, 5), true);
            Assert.That(result, Is.EquivalentTo(new string[]{}));
        }
    }
}
