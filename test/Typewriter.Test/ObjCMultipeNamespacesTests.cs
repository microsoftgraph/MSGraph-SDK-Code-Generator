using NUnit.Framework;

namespace Typewriter.Test
{
    [TestFixture]
    public class ObjCMultipeNamespacesTests
    {
        [Test, RunInApplicationDomain]
        public void Test()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.ObjC);
        }
    }
}
