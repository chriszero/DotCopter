using DotCopter.Commons.Utilities;
using NUnit.Framework;

namespace DotCopter.Tests
{
    [TestFixture]
    public class ScaleTests
    {
        [Test]
        public void TestQuadratics()
        {
            Scale scale = new Scale(-1500F, 0.0000008F, 0F, 0F, 0F);
            Assert.AreEqual(scale.Calculate(1000),-100);
        }
        
        [Test,Ignore]
        public void YouShouldReallyWriteMoreTests()
        {
        }
    }
}
