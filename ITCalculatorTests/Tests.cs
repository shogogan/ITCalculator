
using NUnit.Framework;

namespace ITCalculatorTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            ValidatorUtils.isCpf("058.826.359-18");
        }
    }
}