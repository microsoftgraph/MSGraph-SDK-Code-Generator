using Microsoft.Graph.ODataTemplateWriter.CodeHelpers.TypeScript;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vipr.Core.CodeModel;


namespace GraphODataTemplateWriter.Test
{
    [TestClass]
    public class TypeHelperTests
    {
        OdcmProperty testProperty;
        string actualInputString = "\r\n Test string";
        string expectedOutputString = "\r\n/// Test string";

        [TestInitialize]
        public void Initialize()
        {
            testProperty = new OdcmProperty("testPropertyName");
        }

        /// <summary>
        /// Test that GetSanitizedLongDescription provides long descriptions without uncommented lines of text for Typescript.
        /// </summary>
        [TestMethod]
        public void Long_Description_Doesnt_Contain_Uncommented_NewLine_For_Typescript()
        {
            testProperty.LongDescription = actualInputString;
            string sanitizedString = TypeHelperTypeScript.GetSanitizedLongDescription(testProperty); // Explicitly bind to extension method.

            Assert.AreEqual(expectedOutputString, sanitizedString, "GetSanitizedLongDescription is not handling escaped CRLF.");
        }

        /// <summary>
        /// Test that GetSanitizedLongDescription provides descriptions without uncommented lines of text for Typescript.
        /// </summary>
        [TestMethod]
        public void Description_Doesnt_Contain_Uncommented_NewLine_For_Typescript()
        {
            testProperty.Description = actualInputString;
            string sanitizedString = TypeHelperTypeScript.GetSanitizedLongDescription(testProperty);

            Assert.AreEqual(expectedOutputString, sanitizedString, "GetSanitizedLongDescription is not handling escaped CRLF.");
        }
    }
}
