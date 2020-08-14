using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Typewriter.Test
{
    [TestClass]
    public class PHPMultipleNamespacesTests
    {
        [TestMethod]
        public void Test()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.PHP);
        }
    }
}
