using NUnit.Framework;

namespace Typewriter.Test
{
    [TestFixture]
    public class JavaMultipleNamespacesTests
    {
        [Test]
        public void Test()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.Java);
        }
    }
}
