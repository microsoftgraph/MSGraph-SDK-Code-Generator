using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using NLog;

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
    }
}