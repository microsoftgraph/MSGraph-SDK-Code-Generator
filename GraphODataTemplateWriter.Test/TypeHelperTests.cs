using Microsoft.Graph.ODataTemplateWriter.CodeHelpers.CSharp;
using Microsoft.Graph.ODataTemplateWriter.CodeHelpers.TypeScript;
using Microsoft.Graph.ODataTemplateWriter.CodeHelpers.PHP;
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
        /// Test that GetSanitizedLongDescription provides long descriptions without uncommented lines of text.
        /// </summary>
        [TestMethod]
        public void Long_Description_Doesnt_Contain_Uncommented_NewLine_For_CSharp()
        {
            testProperty.LongDescription = actualInputString;
            string sanitizedString = TypeHelperCSharp.GetSanitizedLongDescription(testProperty); // Explicitly bind to extension method.

            Assert.AreEqual(expectedOutputString, sanitizedString, "GetSanitizedLongDescription is not handling escaped CRLF.");
        }

        /// <summary>
        /// Test that GetSanitizedLongDescription provides descriptions without uncommented lines of text.
        /// </summary>
        [TestMethod]
        public void Description_Doesnt_Contain_Uncommented_NewLine_For_CSharp()
        {
            testProperty.Description = actualInputString;
            string sanitizedString = TypeHelperCSharp.GetSanitizedLongDescription(testProperty);

            Assert.AreEqual(expectedOutputString, sanitizedString, "GetSanitizedLongDescription is not handling escaped CRLF.");
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

        /// <summary>
        /// Test that GetSanitizedLongDescription provides long descriptions without uncommented lines of text for PHP.
        /// </summary>
        [TestMethod]
        public void Long_Description_Doesnt_Contain_Uncommented_NewLine_For_PHP()
        {
            testProperty.LongDescription = actualInputString;
            string sanitizedString = TypeHelperPHP.GetSanitizedLongDescription(testProperty); // Explicitly bind to extension method.

            Assert.AreEqual(expectedOutputString, sanitizedString, "GetSanitizedLongDescription is not handling escaped CRLF.");
        }

        /// <summary>
        /// Test that GetSanitizedLongDescription provides descriptions without uncommented lines of text for PHP.
        /// </summary>
        [TestMethod]
        public void Description_Doesnt_Contain_Uncommented_NewLine_For_PHP()
        {
            testProperty.Description = actualInputString;
            string sanitizedString = TypeHelperPHP.GetSanitizedLongDescription(testProperty);

            Assert.AreEqual(expectedOutputString, sanitizedString, "GetSanitizedLongDescription is not handling escaped CRLF.");
        }

        [TestMethod]
        public void Namespace_Shouldnt_Contain_Whitespace_For_CSharp()
        {
            var testNamespace = new OdcmNamespace("Microsoft.OutlookServices");

            var namespaceName = TypeHelperCSharp.GetNamespaceName(testNamespace);

            Assert.AreEqual(namespaceName, "Microsoft.OutlookServices");
        }

        [TestMethod]
        public void Namespace_Should_PascalCase_For_CSharp()
        {
            var testNamespace = new OdcmNamespace("microsoft.graph");

            var namespaceName = TypeHelperCSharp.GetNamespaceName(testNamespace);

            Assert.AreEqual(namespaceName, "Microsoft.Graph");
        }

        [TestMethod]
        public void PHPMainNamespace_Generated_For_V1()
        {
            var testNamespace = "microsoft.graph";
            const string expectedPHPNamespace = "Microsoft\\Graph";

            var actualPHPNamespace = TypeHelperPHP.GetPHPNamespace(testNamespace);
            Assert.AreEqual(expectedPHPNamespace, actualPHPNamespace);
        }

        [TestMethod]
        public void PHPMainNamespace_Generated_For_Beta()
        {
            var testNamespace = "microsoft.graph";
            const string expectedPHPNamespace = "Beta\\Microsoft\\Graph";

            var actualPHPNamespace = TypeHelperPHP.GetPHPNamespace(testNamespace, "Beta");
            Assert.AreEqual(expectedPHPNamespace, actualPHPNamespace);
        }

        [TestMethod]
        public void PHPSubNamespace_Generated_For_V1()
        {
            var testNamespace = "microsoft.graph.callRecords";
            const string expectedPHPNamespace = "Microsoft\\Graph\\CallRecords";

            var actualPHPNamespace = TypeHelperPHP.GetPHPNamespace(testNamespace);
            Assert.AreEqual(expectedPHPNamespace, actualPHPNamespace);
        }

        [TestMethod]
        public void PHPSubNamespace_Generated_For_Beta()
        {
            var testNamespace = "microsoft.graph.callRecords";
            const string expectedPHPNamespace = "Beta\\Microsoft\\Graph\\CallRecords";

            var actualPHPNamespace = TypeHelperPHP.GetPHPNamespace(testNamespace, "Beta");
            Assert.AreEqual(expectedPHPNamespace, actualPHPNamespace);
        }
    }
}
