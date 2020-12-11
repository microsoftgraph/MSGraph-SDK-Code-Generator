using Microsoft.Graph.ODataTemplateWriter.Settings;
using NUnit.Framework;

namespace Typewriter.Test
{
    [TestFixture]
    public class JavaMultipleNamespacesTests
    {
        [SetUp]
        public void Setup()
        {
            ConfigurationService.ResetSettings();
        }
        [Test]
        public void Test()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.Java);
        }
        [Test]
        public void TestBeta()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.Java, isBeta: true);
        }
    }
}
