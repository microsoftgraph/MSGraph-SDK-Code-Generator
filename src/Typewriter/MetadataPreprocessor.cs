// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Typewriter
{
    /// <summary>
    /// Contains a set of rules for altering CSDL metadata. These rules contain known hacks,
    /// fixes, and workarounds for issues in the metadata. Why the metadata has these issues 
    /// is a long story.
    /// </summary>

    internal class MetadataPreprocessor
    {
        private static Logger Logger => LogManager.GetLogger("MetadataPreprocessor");
        private static XDocument xMetadata;


        internal static XDocument GetXMetadata()
        {
            return xMetadata;
        }

        // Added for tests.
        internal static void SetXMetadata(XDocument metadata)
        {
            xMetadata = metadata;
        }

        /// <summary>
        /// Cleans metadata to match the assumptions in the generator and templates.
        /// </summary>
        /// <param name="csdlContents">Metadata content.</param>
        /// <returns>A string of metadata that should work with the generator.</returns>
        internal static string CleanMetadata(string csdlContents)
        {
            if (csdlContents == "")
                throw new ArgumentException("The CSDL string is empty.");

            xMetadata = XDocument.Parse(csdlContents);

            // Rules to apply to the {csdlContents} metadata.
            RemoveCapabilityAnnotations();
            AddLongDescriptionToThumbnail();
            RemoveHasStream("onenotePage");
            RemoveHasStream("onenoteResource");
            AddContainsTarget("plannerBucket");
            AddContainsTarget("plannerTask");
            AddContainsTarget("plannerPlan");
            AddContainsTarget("plannerDelta");

            AddContainsTarget("teamsAppDefinition");
            AddContainsTarget("itemActivity");
            AddContainsTarget("labelPolicy");

            // RoleManagement singleton
            AddContainsTarget("unifiedRoleDefinition");

            

            // Intune
            AddContainsTarget("windows81TrustedRootCertificate");
            AddContainsTarget("iosTrustedRootCertificate");
            AddContainsTarget("mobileContainedApp");
            AddContainsTarget("managedDeviceCertificateState");
            AddContainsTarget("deviceManagementSettingInstance");

            AddContainsTarget("onPremisesAgentGroup");
            AddContainsTarget("onPremisesAgent");
            AddContainsTarget("publishedResource");

            AddContainsTarget("appVulnerabilityManagedDevice");
            AddContainsTarget("appVulnerabilityMobileApp");

            // TODO: Reorder elements. inspect metadata so tht we capture all changes.


            return xMetadata.ToString();
        }

        /// <summary>
        /// Removes the HasStream attribute from an entity. 
        /// We do this since the metadata does't properly describe the stream nature of this resources.
        /// Examples of this are the onenotePage and onenoteResource entities.
        /// </summary>
        /// <param name="entityTypeName">The value of an EntityType/Name attribute that will have the HasStream attribute removed.</param>
        internal static void RemoveHasStream(string entityTypeName)
        {
            try
            {
                xMetadata.Descendants()
                    .Where(x => x.Name.LocalName == "EntityType")
                    .Where(x => x.Attribute("Name").Value.Equals(entityTypeName))
                    .ToList().ForEach(x => x.Attribute("HasStream").Remove());

                Logger.Info("RemoveHasStream rule was applied so that we removed the HasStream attribute from the {0} entityType.", entityTypeName);
            }
            catch
            {
                Logger.Warn("RemoveHasStream rule was not applied so we could not remove the HasStream attribute from the {0} entityType.", entityTypeName);
            }
        }

        /// <summary>
        /// Add ContainsTarget="true" to all navigationProperties whose type is a collection of a given entityType.
        /// This means that the navigation is to an entity and not to an entity reference.
        /// This also means that the contained entity is part of entity set of the parentEntity; an implied entity set.
        /// </summary>
        /// <param name="entityTypeName">The type of entity to self-contain</param>
        internal static void AddContainsTarget(string entityTypeName)
        {
            var list = xMetadata.Descendants()
                .Where(x => x.Name.LocalName == "NavigationProperty")
                .Where(x => x.Attribute("ContainsTarget") == null || x.Attribute("ContainsTarget").Value.Equals("false"))
                .Where(x => x.Attribute("Type").Value == "Collection(microsoft.graph." + entityTypeName + ")")
                .ToList();

            if (list.Count == 0)
            {
                Logger.Warn("AddContainsTarget rule was not applied. No entity type named {0} found with missing navigation property containment.", entityTypeName);
            }
            else
            {
                list.ForEach(x =>
                {
                    x.SetAttributeValue("ContainsTarget", "true");

                    var parentEntityName = x.Parent.Attribute("Name").Value;
                    var navigationPropertyName = x.Attribute("Name").Value;

                    Logger.Info("AddContainsTarget rule applied so that ContainsTarget=true was set on the {0} entity's {1} navigation property.", parentEntityName, navigationPropertyName);
                });
            }
        }

        /// <summary>
        /// Remove all capability annotations since the metadata doesn't describe 
        /// them properly and the generator doesn't process them properly.
        /// </summary>
        internal static void RemoveCapabilityAnnotations()
        {
            xMetadata.Descendants()
                    .Where(x => (string)x.Name.LocalName == "Annotation")
                    .Where(x => x.Attribute("Term").Value.StartsWith("Org.OData.Capabilities"))
                    .Remove();

            Logger.Info("RemoveCapabilityAnnotations rule was applied so that capability annotations are removed from the metadata.");
        }

        /// <summary>
        /// Adds a long description annotation to the thumbnail complex type. This annotation is a
        /// generation hint for the generator.
        /// </summary>
        internal static void AddLongDescriptionToThumbnail()
        {
            try
            {
                // Thumbnail hack - add LongDescription annotation
                XElement thumbnailComplexType = xMetadata.Descendants()
                    .Where(x => (string)x.Name.LocalName == "ComplexType")
                    .Where(x => x.Attribute("Name").Value == "thumbnail")
                    .First();

                if (thumbnailComplexType != null)
                {
                    // need to specify namespace so default xmlns="" isn't added that breaks VIPR
                    XElement thumbnailAnnotation = new XElement(thumbnailComplexType.Name.Namespace + "Annotation");

                    thumbnailAnnotation.Add(new XAttribute("Term", "Org.OData.Core.V1.LongDescription"));
                    thumbnailAnnotation.Add(new XAttribute("String", "navigable"));
                    thumbnailComplexType.Add(thumbnailAnnotation);

                    Logger.Info("AddLongDescriptionToThumbnail rule was applied to the thumbnail complex type.");
                }
                else
                {
                    Logger.Error("AddLongDescriptionToThumbnail rule was not applied to the thumbnail complex type because the type wasn't found.");
                }
            }
            catch (InvalidOperationException)
            {
                Logger.Error("AddLongDescriptionToThumbnail rule was not applied to the thumbnail complex type because the type wasn't found.");
            }
        }

        /// <summary>
        /// Reorders a Microsoft Graph metadata element's child elements. 
        /// Note: if we have to query and alter the metadata ofter, we may want to add a System.Action parameter to perform the query.
        /// </summary>
        /// <param name="metadataDefinitionType"></param>
        /// <param name="targetGlobalElementName">The name of the element to target for reordering its child elements.</param>
        /// <param name="newElementOrder">An ordered list of strings that represents the new order for the 
        /// target element's child elements. Each entry string represents the name of the ordered element.</param>
        /// <param name="bindingParameterType">Specifies the type of the entity that is bound by the function identified 
        /// by targetGlobalElementName. Only applies to Actions and Functions.</param>
        internal static void ReorderElements(MetadataDefinitionType metadataDefinitionType, 
                                             string targetGlobalElementName, 
                                             List<string> newElementOrder,
                                             string bindingParameterType = "")
        {
            // Actions or Functions require a binding element.
            if (String.IsNullOrEmpty(bindingParameterType) && 
                metadataDefinitionType == MetadataDefinitionType.Action || 
                metadataDefinitionType == MetadataDefinitionType.Function)
            {
                throw new ArgumentNullException(nameof(bindingParameterType), "The binding parameter type must be set in case an Action" +
                    " or Function with the same name and parameter list.");
            }
            
            // Validate that the specified new element order is meaningful.
            if (newElementOrder.Count < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(newElementOrder), "ReorderElements: expected 2 or more elements to reorder.");
            }

            // Sort the newElementOrder so we can compare the sequence to the potential overloads returned for the
            // the targetGlobalElementName. We should only ever find a single match.
            var newElementsAlphaOrdered = newElementOrder.OrderByDescending(x => x.ToString());

            try
            {
                // Get the target element that has the target type (i.e. Action, EntityType), with the target Name (i.e. forward)
                // where it has the same element list as the new element list 
                // where its binding parameter (the first element) has a Type attribute that matches the given 
                // bindingParameterType in the case of Action or Function.

                var results = xMetadata.Descendants()
                    .Where(x => x.Name.LocalName == metadataDefinitionType.ToString())
                    .Where(x => x.Attribute("Name").Value == targetGlobalElementName)
                    .Where(el => el.Elements().Select(a => a.Attribute("Name").Value)
                                              .OrderByDescending(e => e.ToString())
                                              .SequenceEqual(newElementsAlphaOrdered));

                XElement targetElement = null;

                // Reordering elements by element Name attributes. Useful for non Action or Function.
                if (String.IsNullOrEmpty(bindingParameterType))
                {
                    targetElement = results.FirstOrDefault();
                }
                else // We are reordering an Action or Function and must match the bindingParameterType.
                {
                    targetElement = results.Where(e => e.Elements().Any(a => a.Attribute("Type").Value == bindingParameterType)).FirstOrDefault();
                }

                // There wasn't a match. We need to check our inputs.
                if (targetElement is null)
                    throw new ArgumentException($"ReorderElements: Didn't find a {metadataDefinitionType.ToString()} " +
                                                $"named {targetGlobalElementName} that matched the elements in {nameof(newElementOrder)}");

                // Reorder the elements
                List<XElement> newPropertyList = new List<System.Xml.Linq.XElement>();
                var propertyList = targetElement.Elements().ToList();
                foreach (string propertyName in newElementOrder)
                {
                    var index = propertyList.FindIndex(x => x.Attribute("Name").Value == propertyName);
                    newPropertyList.Add(propertyList[index]);

                }
                
                // Update the metadata
                targetElement.Elements().Remove();
                targetElement.Add(newPropertyList);
            }
            catch (Exception ex)
            {
                Logger.Error($"ReorderElements exception caught.\r\nException message: {ex.Message}");
            }
        }
    }
}