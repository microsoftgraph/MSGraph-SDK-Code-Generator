// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

using Microsoft.Graph.ODataTemplateWriter.Settings;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Typewriter.Test
{
    [TestFixture]
    public class Given_a_valid_metadata_file_to_metadata_preprocessor
    {
        public string testMetadata;
        public XDocument testXMetadata;

        /// <summary>
        /// Load metadata from file into a string so we can validate MetadataPreprocessor.
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            testMetadata = Typewriter.Test.Properties.Resources.dirtyMetadata;
            testXMetadata = XDocument.Parse(testMetadata);
            MetadataPreprocessor.SetXMetadata(testXMetadata);
            ConfigurationService.ResetSettings();
        }

        [Test]
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

        [Test]
        public void It_adds_the_ContainsTarget_attribute()
        {
            var navPropTypeToProcess = "plannerPlan";

            bool doesntContainTargetBefore = MetadataPreprocessor.GetXMetadata().Descendants()
                    .Where(x => x.Name.LocalName == "NavigationProperty")
                    .Where(x => x.Attribute("ContainsTarget") == null || 
                                x.Attribute("ContainsTarget").Value.Equals("false"))
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

        [Test]
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

        [Test]
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

        /// <summary>
        /// Tests that we reorder parameters according to an input listof parameters.
        /// </summary>
        [Test]
        public void It_reorders_parameters_in_an_action()
        {
            /* The element to reorder from the resources/dirtymetadata.xml file.
            <Action Name="forward" IsBound="true">
                <Parameter Name="bindingParameter" Type="microsoft.graph.onenotePage" />
                <Parameter Name="ToRecipients" Type="Collection(microsoft.graph.recipient)" Nullable="false" />
                <Parameter Name="Comment" Type="Edm.String" Unicode="false" />
            </Action>
             */

            // Specify the metadata definition to reorder and the new element order specification.
            var targetMetadataDefType = MetadataDefinitionType.Action;
            var targetMetadataDefName = "forward";
            var newParameterOrder = new List<string>() { "bindingParameter",
                                                         "Comment",
                                                         "ToRecipients" };
            var bindingParameterType = "microsoft.graph.onenotePage";

            // Check whether an element exists in the metadata that matches our reordered element list before we reorder.
            var isTargetDefinitionInMetadataBefore = MetadataPreprocessor.GetXMetadata().Descendants()
                    .Where(x => x.Name.LocalName == targetMetadataDefType.ToString())
                    .Where(x => x.Attribute("Name").Value == targetMetadataDefName) // Returns all Action elements named forward.
                    .Where(el => el.Descendants().FirstOrDefault(x => x.Attribute("Type").Value == bindingParameterType) != null)
                    .Where(el => el.Elements().Select(a => a.Attribute("Name").Value)
                        .SequenceEqual(newParameterOrder)).Any();

            // Make a call to reorder the parameters for the target action in the metadata loaded into memory. 
            MetadataPreprocessor.ReorderElements(targetMetadataDefType, 
                                                 targetMetadataDefName, 
                                                 newParameterOrder, 
                                                 bindingParameterType);

            // Query the updated metadata for the results that match the reordered element.
            var results = MetadataPreprocessor.GetXMetadata().Descendants()
                    .Where(x => x.Name.LocalName == targetMetadataDefType.ToString())
                    .Where(x => x.Attribute("Name").Value == targetMetadataDefName) // Returns all Action elements named forward.
                    .Where(el => el.Descendants().FirstOrDefault(x => x.Attribute("Type").Value == bindingParameterType) != null)
                    .Where(el => el.Elements().Select(a => a.Attribute("Name").Value)
                        .SequenceEqual(newParameterOrder));

            Assert.IsFalse(isTargetDefinitionInMetadataBefore);
            // Added multiple elements with the same binding parameter - we want to make sure there is only one in the results.
            Assert.IsTrue(results.Count() == 1, $"Expected: A single result item. Actual: found {results.Count()} items.");
            Assert.AreEqual(newParameterOrder.Count(),
                results.FirstOrDefault().Elements().Count(),
                "The reordered element list doesn't match the count of elements in the input new order list.");
            Assert.IsTrue(results.FirstOrDefault()
                                 .Elements()
                                 .Select(a => a.Attribute("Name").Value)
                                 .SequenceEqual(newParameterOrder),
                          "The element list was not reordered as expected.");
        }

        /// <summary>
        /// Tests that we reorder parameters according to an input element name list.
        /// </summary>
        [Test]
        public void It_reorders_elements_in_a_complextype()
        {
            /* The element to reorder from the resources/dirtymetadata.xml file.
              <ComplexType Name="thumbnail">
                <Property Name="content" Type="Edm.Stream" />
                <Property Name="height" Type="Edm.Int32" />
                <Property Name="sourceItemId" Type="Edm.String" />
                <Property Name="url" Type="Edm.String" />
                <Property Name="width" Type="Edm.Int32" />
              </ComplexType>
             */

            // Specify the metadata definition to reorder and the new element order specification.
            var targetMetadataDefType = MetadataDefinitionType.ComplexType;
            var targetMetadataDefName = "thumbnail";
            var newParameterOrder = new List<string>() { "width",
                                                         "url",
                                                         "sourceItemId",
                                                         "height",
                                                         "content" };

            // Check whether an element exists in the metadata that
            // matches our reordered element list before we reorder.
            var isTargetDefinitionInMetadataBefore = MetadataPreprocessor.GetXMetadata().Descendants()
                    .Where(x => x.Name.LocalName == targetMetadataDefType.ToString())
                    .Where(x => x.Attribute("Name").Value == targetMetadataDefName) 
                    .Where(el => el.Elements().Select(a => a.Attribute("Name").Value)
                        .SequenceEqual(newParameterOrder)).Any();

            // Make a call to reorder the parameters for the target 
            // complex type in the metadata loaded into memory. 
            MetadataPreprocessor.ReorderElements(targetMetadataDefType, 
                                                 targetMetadataDefName, 
                                                 newParameterOrder);

            // Query the updated metadata for the results that match the reordered element.
            var results = MetadataPreprocessor.GetXMetadata().Descendants()
                    .Where(x => x.Name.LocalName == targetMetadataDefType.ToString())
                    .Where(x => x.Attribute("Name").Value == targetMetadataDefName) 
                    .Where(el => el.Elements().Select(a => a.Attribute("Name").Value)
                        .SequenceEqual(newParameterOrder));

            Assert.IsFalse(isTargetDefinitionInMetadataBefore);
            Assert.IsTrue(results.Count() == 1, $"Expected: A single result item. Actual: found {results.Count()} items.");
            Assert.AreEqual(newParameterOrder.Count(),
                            results.FirstOrDefault().Elements().Count(),
                            "The reordered element list doesn't match the count of elements in the input new order list.");
            Assert.IsTrue(results.FirstOrDefault().Elements().Select(a => a.Attribute("Name").Value).SequenceEqual(newParameterOrder), 
                          "The element list was not reordered as expected.");
        }

        [Test]
        public void It_does_not_reorder_when_element_list_does_not_match_in_a_complextype()
        {
            /* The element to attempt to reorder from the resources/dirtymetadata.xml file. 
             * The element list, newParameterOrder does not match the thumbnail type 
             * in the metadata (missing 'content' element) so we expect that the 
             * reorder attempt fails.
              <ComplexType Name="thumbnail">
                <Property Name="content" Type="Edm.Stream" />
                <Property Name="height" Type="Edm.Int32" />
                <Property Name="sourceItemId" Type="Edm.String" />
                <Property Name="url" Type="Edm.String" />
                <Property Name="width" Type="Edm.Int32" />
              </ComplexType>
             */

            // Specify the metadata definition to reorder and the new 
            // element order specification.
            var targetMetadataDefType = MetadataDefinitionType.ComplexType;
            var targetMetadataDefName = "thumbnail";
            var newParameterOrder = new List<string>() { "width",
                                                         "url",
                                                         "sourceItemId",
                                                         "height" };

            // Make a call to reorder the parameters for the target 
            // complex type in the metadata loaded into memory. 
            MetadataPreprocessor.ReorderElements(targetMetadataDefType, 
                                                 targetMetadataDefName, 
                                                 newParameterOrder);

            // Query the updated metadata for the results that match the reordered element.
            var results = MetadataPreprocessor.GetXMetadata().Descendants()
                    .Where(x => x.Name.LocalName == targetMetadataDefType.ToString())
                    .Where(x => x.Attribute("Name").Value == targetMetadataDefName)
                    .Where(el => el.Elements().Select(a => a.Attribute("Name").Value)
                        .SequenceEqual(newParameterOrder));

            Assert.IsTrue(results.Count() == 0, 
                          $"Expected: Zero results. Actual: found {results.Count()} items.");
        }
    }
}
