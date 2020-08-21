using NUnit.Framework;

namespace Typewriter.Test
{
    [TestFixture]
    public class CSharpMultipleNamespacesTests
    {
        [Test]
        public void Test()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.CSharp);
        }
    }
}
