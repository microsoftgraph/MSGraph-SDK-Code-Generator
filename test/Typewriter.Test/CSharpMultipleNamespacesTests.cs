using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Typewriter.Test
{
    [TestClass]
    public class CSharpMultipleNamespacesTests
    {
        [TestMethod]
        public void Test()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.CSharp);
        }
    }
}
