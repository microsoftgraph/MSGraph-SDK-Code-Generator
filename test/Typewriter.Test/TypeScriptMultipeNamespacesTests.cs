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
    }
}
