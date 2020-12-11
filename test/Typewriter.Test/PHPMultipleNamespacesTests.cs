using Microsoft.Graph.ODataTemplateWriter.Settings;
using NUnit.Framework;

namespace Typewriter.Test
{
    [TestFixture]
    public class PHPMultipleNamespacesTests
    {
        [SetUp]
        public void Setup()
        {
            ConfigurationService.ResetSettings();
        }
        [Test]
        public void Test()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.PHP);
        }

        [Test]
        public void TestBeta()
        {
            MultipleNamespacesTestRunner.Run(TestLanguage.PHP, isBeta: true);
        }
    }
}
