﻿using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Typewriter.Test
{
    /// <summary>
    /// End to end tests that check the results of running Typewriter from the CLI.
    /// IMPORTANT: Typewriter MUST be built before as the templates need to be compiled before running the tests.
    /// </summary>
    [TestFixture]
    public class Given_a_valid_metadata_file_to_Typewriter
    {
        public string testMetadata;
        // The second segment is generated from the namespace in the target metadata file.
        public string generatedOutputUrl = @$"{Path.DirectorySeparatorChar}com{Path.DirectorySeparatorChar}microsoft{Path.DirectorySeparatorChar}graph";

        /// <summary>
        /// Load metadata from file into a string so we can validate MetadataPreprocessor.
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            testMetadata = Typewriter.Test.Properties.Resources.dirtyMetadata;
        }

        [Test]
        public void It_generates_a_typings_file()
        {
            const string outputDirectory = "output"; 

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "TypeScript"
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}Microsoft-graph.d.ts");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected {fileInfo.FullName}. File was not found.");
        }

        [Test]
        public void It_generates_PHP_models_with_a_property()
        {
            const string testNamespace = "Beta";
            const string outputDirectory = "output";
            
            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "PHP",
                Properties = new List<string>() { $"php.namespacePrefix:{testNamespace}" },
                GenerationMode = GenerationMode.Files,
                EndpointVersion = "beta"
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + @$"{Path.DirectorySeparatorChar}com{Path.DirectorySeparatorChar}Beta{Path.DirectorySeparatorChar}Microsoft{Path.DirectorySeparatorChar}Graph{Path.DirectorySeparatorChar}Model{Path.DirectorySeparatorChar}Entity.php");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            // Check that the namespace applied at the CLI was added to the document.
            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);
            bool isExpectedNamespaceSet = false;
            foreach (var line in lines)
            {
                if (line.Contains($"namespace {testNamespace}"))
                {
                    isExpectedNamespaceSet = true;
                    break;
                }
            } 
            ClassicAssert.IsTrue(isExpectedNamespaceSet, $"The expected namespace, {testNamespace}, was not set in the generated test file.");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void It_generates_Java_models_with_disambiguated_import()
        {
            const string outputDirectory = "outputJava";

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "Java",
                GenerationMode = GenerationMode.Files
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}requests{Path.DirectorySeparatorChar}TimeOffRequestCollectionRequest.java");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            // Check that the namespace applied at the CLI was added to the document.
            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);
            bool isExpectedImportStatementFound = false;
            string expected = "import com.microsoft.graph.models.TimeOffRequest;";
            foreach (var line in lines)
            {
                if (line.Contains(expected))
                {
                    isExpectedImportStatementFound = true;
                    break;
                }
            }
            ClassicAssert.IsTrue(isExpectedImportStatementFound, $"The expected statement was not found. Expected: {expected}");
        }

        [Test]
        public void It_generates_dotNet_client_with_default_beta_baseUrl()
        {
            const string outputDirectory = "output";

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "CSharp",
                GenerationMode = GenerationMode.Files,
                EndpointVersion = "beta"
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}requests{Path.DirectorySeparatorChar}GraphServiceClient.cs");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            // Check that the beta endpoint was set as the default endpoint. Otherwise it uses v1.0.
            // https://github.com/microsoftgraph/msgraph-sdk-dotnet/blob/dev/src/Microsoft.Graph/Requests/Generated/GraphServiceClient.cs#L25
            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);
            bool hasFoundBetaUrl = false;
            string betaUrl = "https://graph.microsoft.com/beta";
            foreach (var line in lines)
            {
                if (line.Contains(betaUrl))
                {
                    hasFoundBetaUrl = true;
                    break;
                }
            }
            ClassicAssert.IsTrue(hasFoundBetaUrl, $"The expected default base URL, {betaUrl}, was not set in the generated test file.");
        }


        [Test]
        public void It_generates_dotNet_client_with_commented_out_code_comments()
        {
            const string outputDirectory = "output";

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "CSharp",
                GenerationMode = GenerationMode.Files
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}model{Path.DirectorySeparatorChar}OnenotePage.cs");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            // Check that the test string is found in the output file. Converting "&#xD;&#xA; Test token string" to "/// Test token string" is what we are testing.
            // We are making sure that the documentation annotation is handled correctly for this scenario.
            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);
            bool hasTestString = false;
            string testString = @"/// Test token string";
            foreach (var line in lines)
            {
                if (line.Contains(testString))
                {
                    hasTestString = true;
                    break;
                }
            }
            ClassicAssert.IsTrue(hasTestString, $"The expected test token string, '{testString}', was not set in the generated test file. We are not correctly handling the \r\n coming from the annotations.");
        }

        [Test]
        public void It_does_not_generates_dotNet_odata_type_initialization_for_complex_types_with_no_inheritance()
        {
            const string outputDirectory = "output";

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "CSharp",
                GenerationMode = GenerationMode.Files
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}model{Path.DirectorySeparatorChar}Thumbnail.cs");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);
            bool hasTestString = false;
            string testString = "this.ODataType = \"microsoft.graph.thumbnail\";";
            bool hasCstorString = false;
            string testCstorString = "public Thumbnail()";
            foreach (var line in lines)
            {
                if (line.Contains(testString))
                {
                    hasTestString = true;
                }
                if (line.Contains(testCstorString))
                {
                    hasCstorString = true;
                }
            }

            ClassicAssert.IsFalse(hasTestString, $"The expected test token string, '{testString}', was set in the generated test file. We didn't properly generate the setter code in the cstor.");
            ClassicAssert.IsFalse(hasCstorString, $"The expected test token cstor string, '{testCstorString}', was set in the generated test file. We didn't properly generate the cstor code.");
        }

        [Test]
        public void It_doesnt_generate_odatatype_initialization_for_abstract_complextypes()
        {
            const string outputDirectory = "output";

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "CSharp",
                GenerationMode = GenerationMode.Files
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}model{Path.DirectorySeparatorChar}EmptyComplexType.cs");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);
            bool hasTestString = false;
            string testString = "this.ODataType = \"microsoft.graph.emptyComplexType\";";
            foreach (var line in lines)
            {
                if (line.Contains(testString))
                {
                    hasTestString = true;
                    break;
                }
            }

            ClassicAssert.IsFalse(hasTestString, $"The unexpected test token string, '{testString}', was set in the generated test file. We incorrectly generated the setter code in the cstor.");
        }


        [Test]
        public void It_doesnt_generate_dotNet_odatatype_initialization_for_entitytypes()
        {
            const string outputDirectory = "output";

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "CSharp",
                GenerationMode = GenerationMode.Files
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}model{Path.DirectorySeparatorChar}TestType.cs");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);
            bool hasTestString = false;
            string testString = "this.ODataType = \"microsoft.graph.testType\";";
            foreach (var line in lines)
            {
                if (line.Contains(testString))
                {
                    hasTestString = true;
                    break;
                }
            }
            ClassicAssert.False(hasTestString, $"The expected test token string, '{testString}', was not set in the generated test file. We didn't properly generate the cstor code.");
        }

        [Test]
        public void It_generates_dotNet_odatatype_initialization_for_entitytypes_with_base_referenced()
        {
            const string outputDirectory = "output";

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "CSharp",
                GenerationMode = GenerationMode.Files
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}model{Path.DirectorySeparatorChar}TestType4.cs");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);
            bool hasTestString = false;
            string testString = "this.ODataType = \"microsoft.graph.testType4\";";
            foreach (var line in lines)
            {
                if (line.Contains(testString))
                {
                    hasTestString = true;
                    break;
                }
            }
            ClassicAssert.True(hasTestString, $"The expected test token string, '{testString}', was not set in the generated test file. We didn't properly generate the cstor code.");
        }

        [Test]
        public void It_doesnt_generate_odatatype_initialization_for_abstract_entitytypes()
        {
            const string outputDirectory = "output";

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "CSharp",
                GenerationMode = GenerationMode.Files
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}model{Path.DirectorySeparatorChar}Entity.cs");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);
            bool hasTestString = false;
            bool hasTestODataInitString = false;
            string testString = "protected internal Entity()";
            string testODataInitString = "this.ODataType = \"microsoft.graph.entity\"";
            foreach (var line in lines)
            {
                if (line.Contains(testString))
                {
                    hasTestString = true;
                }
                if (line.Contains(testODataInitString))
                {
                    hasTestODataInitString = true;
                }
            }
            ClassicAssert.IsTrue(hasTestString, $"The expected test token string, '{testString}', was not set in the generated test file. We didn't properly generate the cstor code.");
            ClassicAssert.IsFalse(hasTestODataInitString, $"The unexpected test token string, '{testODataInitString}', was set in the generated test file. We didn't properly generate the cstor code.");
        }

        [Test]
        public void It_creates_disambiguated_abstract_base_complextype_models()
        {
            const string outputDirectory = "output";

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "CSharp",
                GenerationMode = GenerationMode.Files
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}model{Path.DirectorySeparatorChar}EmptyBaseComplexTypeRequest.cs");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);
            bool hasTestString = false;
            string testString = "public partial class EmptyBaseComplexTypeRequestObject";
            string constructorString = "protected internal EmptyBaseComplexTypeRequestObject";

            foreach (var line in lines)
            {
                if (line.Contains(testString))
                {
                    hasTestString = true;
                    continue;
                }
                if (line.Contains(constructorString))
                {
                    hasTestString = true;
                    continue;
                }
            }

            ClassicAssert.IsTrue(hasTestString, $"The expected test token string, '{testString}', was not set in the generated test file. We didn't properly generate the type declaration code.");
        }

        [Test]
        public void It_creates_disambiguated_complextype_models()
        {
            const string outputDirectory = "output";

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "CSharp",
                GenerationMode = GenerationMode.Files
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}model{Path.DirectorySeparatorChar}DerivedComplexTypeRequest.cs");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);
            bool hasTestTypeDeclaration = false;
            string testTypeDeclaration = "public partial class DerivedComplexTypeRequestObject : EmptyBaseComplexTypeRequestObject";
            bool hasTestTypeCstor = false;
            string testTypeCstor = "public DerivedComplexTypeRequestObject";
            bool hasTestOdataType = false;
            string testOdataType = "this.ODataType = \"microsoft.graph.derivedComplexTypeRequest\"";

            foreach (var line in lines)
            {
                // We only need to check once.
                if (line.Contains(testTypeDeclaration) && !hasTestTypeDeclaration)
                {
                    hasTestTypeDeclaration = true;
                    continue;
                }
                if (line.Contains(testTypeCstor) && !hasTestTypeCstor)
                {
                    hasTestTypeCstor = true;
                    continue;
                }
                if (line.Contains(testOdataType) && !hasTestOdataType)
                {
                    hasTestOdataType = true;
                    break; // This is the last expected line.
                }
            }

            ClassicAssert.IsTrue(hasTestTypeDeclaration, $"The expected test token string, '{testTypeDeclaration}', was not set in the generated test file. We didn't properly generate the type declaration code.");
            ClassicAssert.IsTrue(hasTestTypeCstor, $"The expected test token string, '{testTypeCstor}', was not set in the generated test file. We didn't properly generate the cstor code.");
            ClassicAssert.IsTrue(hasTestOdataType, $"The expected test token string, '{testOdataType}', was not set in the generated test file. We didn't properly generate the initialized odata.type code.");
        }

        [Test]
        public void It_creates_disambiguated_MethodRequestBuilder_parameters()
        {
            const string outputDirectory = "output";

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "CSharp",
                GenerationMode = GenerationMode.Files
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}requests{Path.DirectorySeparatorChar}TestTypeQueryRequestBuilder.cs");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);
            bool hasTestParameter = false;
            string testParameter = "IEnumerable<DerivedComplexTypeRequestObject> requests)";
            

            foreach (var line in lines)
            {
                // We only need to check once.
                if (line.Contains(testParameter))
                {
                    hasTestParameter = true;
                    break;
                }
            }

            ClassicAssert.IsTrue(hasTestParameter, $"The expected test token string, '{testParameter}', was not set in the generated test file. We didn't properly generate the parameter.");
        }

        [Test]
        public void It_creates_disambiguated_EntityRequestBuilder_parameters()
        {
            const string outputDirectory = "output";

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "CSharp",
                GenerationMode = GenerationMode.Files
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}requests{Path.DirectorySeparatorChar}TestTypeRequestBuilder.cs");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);

            bool hasTestParameter = false;
            string testParameter = "IEnumerable<DerivedComplexTypeRequestObject> requests)";
            
            foreach (var line in lines)
            {
                // We only need to check once.
                if (line.Contains(testParameter))
                {
                    hasTestParameter = true;
                    break;
                }
            }

            ClassicAssert.IsTrue(hasTestParameter, $"The expected test token string, '{testParameter}', was not set in the generated test file. We didn't properly generate the parameter.");
        }

        [Test]
        public void It_creates_disambiguated_IEntityRequestBuilder_parameters()
        {
            const string outputDirectory = "output";

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "CSharp",
                GenerationMode = GenerationMode.Files
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}requests{Path.DirectorySeparatorChar}ITestTypeRequestBuilder.cs");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);

            bool hasTestParameter = false;
            string testParameter = "IEnumerable<DerivedComplexTypeRequestObject> requests)";

            foreach (var line in lines)
            {
                // We only need to check once.
                if (line.Contains(testParameter))
                {
                    hasTestParameter = true;
                    break;
                }
            }

            ClassicAssert.IsTrue(hasTestParameter, $"The expected test token string, '{testParameter}', was not set in the generated test file. We didn't properly generate the parameter.");
        }

        [Test]
        public void It_transforms_metadata()
        {
            const string outputDirectory = "output";

            Options options = new Options()
            {
                Output = outputDirectory,
                GenerationMode = GenerationMode.Transform,
                Transform = "https://raw.githubusercontent.com/microsoftgraph/msgraph-metadata/master/transforms/csdl/preprocess_csdl.xsl"

            };

            Generator.Transform(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + @$"{Path.DirectorySeparatorChar}cleanMetadata.xml");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);

            bool hasThumbnailAnnotationBeenAdded = false; // Expect true
            bool hasHasStreamAttribute = false; // Expect false
            bool hasContainsTargetBeenSet = false; // Expect true

            // Check the document for these values.
            foreach (var line in lines)
            {
                if (line.Contains(@"<Annotation Term=""Org.OData.Core.V1.LongDescription"" String=""navigable"" />"))
                {
                    hasThumbnailAnnotationBeenAdded = true;
                }
                if (line.Contains("HasStream"))
                {
                    hasHasStreamAttribute = true;
                }
                if (line.Contains(@"<NavigationProperty Name=""plans"" Type=""Collection(microsoft.graph.plannerPlan)"" ContainsTarget=""true"" />"))
                {
                    hasContainsTargetBeenSet = true;
                }
            }

            ClassicAssert.IsTrue(hasThumbnailAnnotationBeenAdded, $"The expected LongDescription annotation wasn't set in the transformed cleaned metadata.");
            ClassicAssert.IsFalse(hasHasStreamAttribute, $"The HasStream attribute was't removed from the metadata.");
            ClassicAssert.IsTrue(hasContainsTargetBeenSet, $"The expected ContainsTarget attribute wasn't set in the transformed cleaned metadata.");
        }

        [Test]
        [Ignore("Current xslt transform adds global capability annotations")]
        [TestCase(true, false, false)]
        [TestCase(false, true, false)]
        [TestCase(true, false, true)]
        [TestCase(false, true, true)]
        public void It_transforms_metadata_and_keeps_annotations_and_includes_errors(bool shouldRemoveCapabilityAnnotation, bool shouldFindCapabilityAnnotation, bool addinnererrordescription)
        {
            const string outputDirectory = "output";
            const string outputBaseFileName = "cleanMetadata";

            Options options = new Options()
            {
                Output = outputDirectory,
                GenerationMode = GenerationMode.Transform,
                Transform = "https://raw.githubusercontent.com/microsoftgraph/msgraph-metadata/master/transforms/csdl/preprocess_csdl.xsl",
                RemoveAnnotations = shouldRemoveCapabilityAnnotation,
                AddInnerErrorDescription = addinnererrordescription,
            };

            Generator.Transform(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + @$"{Path.DirectorySeparatorChar}{outputBaseFileName}.xml");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);

            bool hasCapabilityAnnotations = lines.Any(x => x.Contains("Org.OData.Capabilities")); // Expect false

            ClassicAssert.AreEqual(shouldFindCapabilityAnnotation, hasCapabilityAnnotations, $"Expected to find capability annotations: {shouldFindCapabilityAnnotation}. Actually found capability annotations: {hasCapabilityAnnotations}");
        }

        [Test]
        [TestCase("TestType2FunctionMethodWithStringRequest.cs", "var response = await this.SendAsync<ODataMethodStringResponse>(null, cancellationToken);")]
        [TestCase("TestType2FunctionMethodWithBooleanRequest.cs", "var response = await this.SendAsync<ODataMethodBooleanResponse>(null, cancellationToken);")]
        [TestCase("TestType2FunctionMethodWithInt32Request.cs", "var response = await this.SendAsync<ODataMethodIntResponse>(null, cancellationToken);")]
        [TestCase("TestType3ActionMethodWithInt64Request.cs", "var response = await this.SendAsync<ODataMethodLongResponse>(null, cancellationToken);")]
        public void It_creates_method_request_with_OData_return_type(string outputFileName, string testParameter)
        {
            const string outputDirectory = "output";

            Options optionsCSharp = new Options()
            {
                Output = outputDirectory,
                Language = "CSharp",
                GenerationMode = GenerationMode.Files
            };

            Generator.GenerateFiles(testMetadata, optionsCSharp);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}requests{Path.DirectorySeparatorChar}" + outputFileName);
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);
            bool hasTestParameter = false;

            foreach (var line in lines)
            {
                // We only need to check once.
                if (line.Contains(testParameter))
                {
                    hasTestParameter = true;
                    break;
                }
            }

            ClassicAssert.IsTrue(hasTestParameter, $"The expected test token string, '{testParameter}', was not set in the generated test file. We didn't properly generate the SendAsync method.");
        }
    }
}
