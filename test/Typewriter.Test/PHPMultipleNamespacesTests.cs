using NUnit.Framework;

namespace Typewriter.Test
{
    [TestFixture]
    public class PHPMultipleNamespacesTests
    {
        [Test, RunInApplicationDomain]
        public void Test()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.PHP);
        }

        [Test, RunInApplicationDomain]
        public void TestBeta()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.PHP, isPhpBeta: true);
        }
    }
}
