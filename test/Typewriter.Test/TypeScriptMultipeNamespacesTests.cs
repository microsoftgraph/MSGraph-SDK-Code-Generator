using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Typewriter.Test
{
    [TestClass]
    public class TypeScriptMultipeNamespacesTests
    {
        [TestMethod]
        public void Test()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.TypeScript);
        }
    }
}
