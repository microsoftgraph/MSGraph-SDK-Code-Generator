using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Typewriter.Test
{
    [TestClass]
    public class Given_a_valid_metadata_file_to_metadata_preprocessor
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
            MetadataPreprocessor.SetXMetadata(testXMetadata);
        }

        [TestMethod]
        public void It_removes_the_HasStream_attribute()
        {
            var entityToProcess = "onenotePage";

            bool hasStreamBefore =  MetadataPreprocessor.GetXMetadata().Descendants()
                    .Where(x => x.Name.LocalName == "EntityType")
                    .Where(x => x.Attribute("Name").Value.Equals(entityToProcess))
                    .Where(x => x.Attribute("HasStream").Value.Equals("true")).Any();

            Assert.IsTrue(hasStreamBefore, "Expected: HasStream is present. Actual: HasStream was not found.");

            MetadataPreprocessor.RemoveHasStream(entityToProcess);

            bool hasStreamAfter = MetadataPreprocessor.GetXMetadata().Descendants()
                    .Where(x => x.Name.LocalName == "EntityType")
                    .Where(x => x.Attribute("Name").Value.Equals(entityToProcess))
                    .Where(x => x.Attribute("HasStream") != null).Any();

            Assert.IsFalse(hasStreamAfter, "Expected: The HasStream aatribute is not present. Actual: HasStream is present.");
        }

        [TestMethod]
        public void It_adds_the_ContainsTarget_attribute()
        {
            var navPropTypeToProcess = "plannerPlan";

            bool doesntContainTargetBefore = MetadataPreprocessor.GetXMetadata().Descendants()
                    .Where(x => x.Name.LocalName == "NavigationProperty")
                    .Where(x => x.Attribute("ContainsTarget") == null || x.Attribute("ContainsTarget").Value.Equals("false"))
                    .Where(x => x.Attribute("Type").Value == $"Collection(microsoft.graph.{navPropTypeToProcess})")
                    .Any();

            Assert.IsTrue(doesntContainTargetBefore, "Expected: ContainsTarget is false. Actual: ContainsTarget is true");

            MetadataPreprocessor.AddContainsTarget(navPropTypeToProcess);

            bool doesContainTargetAfter =  MetadataPreprocessor.GetXMetadata().Descendants()
                    .Where(x => x.Name.LocalName == "NavigationProperty")
                    .Where(x => x.Attribute("ContainsTarget") != null)
                    .Where(x => x.Attribute("ContainsTarget").Value == "true")
                    .Where(x => x.Attribute("Type").Value == $"Collection(microsoft.graph.{navPropTypeToProcess})")
                    .Any();

            Assert.IsTrue(doesContainTargetAfter, "Expected: ContainsTarget is true. Actual: ContainsTarget is false");
        }

        [TestMethod]
        public void It_removes_capability_annotations()
        {
            bool hasCapabilityAnnotationsBefore = MetadataPreprocessor.GetXMetadata().Descendants()
                    .Where(x => (string)x.Name.LocalName == "Annotation")
                    .Where(x => x.Attribute("Term").Value.StartsWith("Org.OData.Capabilities")).Any();

            MetadataPreprocessor.RemoveCapabilityAnnotations();

            bool hasCapabilityAnnotationsAfter = MetadataPreprocessor.GetXMetadata().Descendants()
                    .Where(x => (string)x.Name.LocalName == "Annotation")
                    .Where(x => x.Attribute("Term").Value.StartsWith("Org.OData.Capabilities")).Any();

            Assert.IsTrue(hasCapabilityAnnotationsBefore, "Expected: find capability annotations. Actual: found none."); // because the test data has capa annotations.
            Assert.IsFalse(hasCapabilityAnnotationsAfter, "Expected: false, there should be no elements returned. Actual: there are capability annotations."); // 
        }

        [TestMethod]
        public void It_adds_long_description_to_thumbnail()
        {
            XElement thumbnailComplexTypeBefore = MetadataPreprocessor.GetXMetadata().Descendants()
                .Where(x => (string)x.Name.LocalName == "ComplexType")
                .Where(x => x.Attribute("Name").Value == "thumbnail")
                .First();

            bool foundAnnotationBefore = thumbnailComplexTypeBefore.Descendants("{http://docs.oasis-open.org/odata/ns/edm}Annotation").Any();

            Assert.IsNotNull(thumbnailComplexTypeBefore, "Expected: thumbnailComplexType is not null as the metadata contains this element. Actual: this element was not found in the metadata.");
            Assert.IsFalse(foundAnnotationBefore, "Expected: no annotation set before the addlong description rule. Actual: it has already been added");

            MetadataPreprocessor.AddLongDescriptionToThumbnail();

            XElement thumbnailComplexTypeAfter = MetadataPreprocessor.GetXMetadata().Descendants()
                .Where(x => (string)x.Name.LocalName == "ComplexType")
                .Where(x => x.Attribute("Name").Value == "thumbnail")
                .First();

            bool foundAnnotationAfter = thumbnailComplexTypeAfter.Descendants("{http://docs.oasis-open.org/odata/ns/edm}Annotation")
                .Where(x => x.Attribute("String").Value.Equals("navigable")).Any();

            Assert.IsTrue(foundAnnotationAfter, "Expected: thumbnailComplexType set with an annotation. Actual: annotation wasn't found.");
        }

        [TestMethod]
        public void It_reorders_elements()
        {
            /* The element to reorder from the testMetadata file.
            <Action Name="forward" IsBound="true">
                <Parameter Name="bindingParameter" Type="microsoft.graph.onenotePage" />
                <Parameter Name="ToRecipients" Type="Collection(microsoft.graph.recipient)" Nullable="false" />
                <Parameter Name="Comment" Type="Edm.String" Unicode="false" />
            </Action>
             */


            // Arrange - Specify the element
            var targetMetadataDefType = MetadataDefinitionType.Action;
            var targetMetadataDefName = "forward";
            var newElementOrder = new List<string>() { "bindingParameter", "Comment", "ToRecipients" };
            var bindingParameterType = "microsoft.graph.onenotePage";

            // Act
            MetadataPreprocessor.ReorderElements(targetMetadataDefType, targetMetadataDefName, newElementOrder, bindingParameterType);

            var results = MetadataPreprocessor.GetXMetadata().Descendants()
                                                             .Where(x => x.Name.LocalName == targetMetadataDefType.ToString())
                                                             .Where(x => x.Attribute("Name").Value == targetMetadataDefName) // Returns all Action elements named forward.
                                                             .Where(el => el.Descendants().FirstOrDefault(x => x.Attribute("Type").Value == bindingParameterType) != null);

            // TODO: complete test
            results.Select(a => a.Attribute("Name").Value).OrderByDescending(e => e.ToString())
                                                          .SequenceEqual(newElementOrder);

        }
    }
}
