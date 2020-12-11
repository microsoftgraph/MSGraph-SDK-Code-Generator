using NUnit.Framework;

namespace Typewriter.Test
{
    [TestFixture]
    public class ObjCMultipeNamespacesTests
    {
        [Test]
        public void Test()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.ObjC);
        }
    }
}
