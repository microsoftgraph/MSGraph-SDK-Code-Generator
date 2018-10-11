using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Typewriter;

namespace Typewriter.Test
{
    [TestClass]
    public class MetadataPreprocessorTests
    {
        public string testMetadata;
        public XDocument testXMetadata;

        /// <summary>
        /// Load metadata from file into a string so we can validate MetadataPreprocessor.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            testMetadata = Typewriter.Test.Properties.Resources.dirtyMetadata;
            testXMetadata = XDocument.Parse(testMetadata);
        }

        [TestMethod]
        public void RemoveCapabilityAnnotations()
        {
            MetadataPreprocessor processor = new MetadataPreprocessor();
        }
    }
}
