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

            var detector1 = new RepetitionDetector(text, e, true);
            var detector2 = new SillyDetector(text, e, true);
            var removeStrategy = new RemoveBorderStrategy();
            var generator = new RandomNotLastCharGenerator(text, 4);

            SyncronizedRandomWordGenerator.Generate(detector1, detector2, 1000, removeStrategy, generator);
        }
    }
}
