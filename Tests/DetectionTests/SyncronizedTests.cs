using System.Text;
using NUnit.Framework;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;
using RepetitionDetection.TextGeneration;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace Tests.DetectionTests
{
    [TestFixture]
    public class SyncronizedTests
    {
        [Test]
        public void TestDetectors()
        {
            var text = new StringBuilder();
            var e = new RationalNumber(2);
            const bool detectEqual = true;

            var detector1 = new RepetitionDetector(text, e, detectEqual);
            var detector2 = new SillyDetector(text, e, detectEqual);
            var removeStrategy = new RemoveBorderStrategy();
            var generator = new CleverCharGenerator(text, 4, e, detectEqual);

            SyncronizedRandomWordGenerator.Generate(detector1, detector2, 1000, removeStrategy, generator);
        }
    }
}
