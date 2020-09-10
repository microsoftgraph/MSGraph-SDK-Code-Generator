using NUnit.Framework;

namespace Typewriter.Test
{
    [TestFixture]
    public class TypeScriptMultipeNamespacesTests
    {
        [Test, RunInApplicationDomain]
        public void Test()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.TypeScript);
        }

        [Test, RunInApplicationDomain]
        public void TestBeta()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.TypeScript, isBeta: true);
        }
    }
}
