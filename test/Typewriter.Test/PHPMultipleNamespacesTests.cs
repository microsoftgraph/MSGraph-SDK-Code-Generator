using NUnit.Framework;

namespace Typewriter.Test
{
    [TestFixture]
    public class PHPMultipleNamespacesTests
    {
        [Test]
        public void Test()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.PHP);
        }

        [Test]
        public void TestBeta()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.PHP, isBeta: true);
        }
    }
}
