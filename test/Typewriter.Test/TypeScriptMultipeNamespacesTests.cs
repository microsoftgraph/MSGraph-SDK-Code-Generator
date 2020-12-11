using NUnit.Framework;

namespace Typewriter.Test
{
    [TestFixture]
    public class TypeScriptMultipeNamespacesTests
    {
        [Test]
        public void Test()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.TypeScript);
        }

        [Test]
        public void TestBeta()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.TypeScript, isBeta: true);
        }
    }
}
