// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Vipr.Core.CodeModel;
    using NLog;
    using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;

    public static class OdcmModelExtensions
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static bool IsCollection(this OdcmProperty odcmProperty)
        {
            return odcmProperty.IsCollection;
        }

        public static IEnumerable<OdcmNamespace> GetOdcmNamespaces(this OdcmModel model)
        {
            IEnumerable<OdcmNamespace> namespacesFound;
            var filtered = model.Namespaces
                .Where(x => !x.Name.Equals("Edm", StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            if (filtered.Count < 1)
            {
                namespacesFound = null;
            }
            else
            {
                namespacesFound = filtered;
            }

            return namespacesFound;
        }

        public static IEnumerable<OdcmClass> GetComplexTypes(this OdcmModel model)
        {
            var namespaces = GetOdcmNamespaces(model);
            return namespaces.SelectMany(@namespace => @namespace.Classes.Where(x => x is OdcmComplexClass && x.CanonicalName().ToLowerInvariant() != "microsoft.graph.json"));
        }

        public static IEnumerable<OdcmClass> GetEntityTypes(this OdcmModel model)
        {
            var namespaces = GetOdcmNamespaces(model);
            return namespaces.SelectMany(@namespace => @namespace.Classes.Where(x => x is OdcmEntityClass || x is OdcmMediaClass));
        }

        public static IEnumerable<OdcmClass> GetMediaEntityTypes(this OdcmModel model)
        {
            var namespaces = GetOdcmNamespaces(model);
            return namespaces.SelectMany(@namespace => @namespace.Classes.Where(x => x is OdcmMediaClass));
        }

        public static IEnumerable<OdcmProperty> GetProperties(this OdcmModel model)
        {
            return model.GetProperties(typeName: null, longDescriptionMatches: null);
        }

        public static IEnumerable<OdcmProperty> GetProperties(this OdcmModel model, string typeName = null, string longDescriptionMatches = null)
        {
            var properties = model.GetEntityTypes().SelectMany(entityTypes => entityTypes.Properties)
                                  .Union(model.EntityContainer.Properties)
                                  .Union(model.GetComplexTypes().SelectMany(complexType => complexType.Properties));
            return FilterProperties(properties, typeName, longDescriptionMatches);
        }

        public static IEnumerable<OdcmProperty> GetStreamProperties(this OdcmModel model)
        {
            return model.GetProperties(typeName: "Stream", longDescriptionMatches: null);
        }

        public static IEnumerable<OdcmClass> GetEntityReferenceTypes(this OdcmModel model)
        {
            // We don't want to do Where() on entity types since that will iterate every entity type to see if it's a reference.
            // Instead, take the properties that we know are references and not collections and grab the appropriate entity type
            // for each, returning those.
            var entityTypes = model.GetEntityTypes();

            // Removed the !IsCollection condition as that was removing expected entity references from the collection.
            var referencePropertyTypes = model.GetProperties().Where(prop => prop.IsReference()).Select(prop => prop.Projection.Type).Distinct();

            var referenceEntityTypes = new List<OdcmClass>();
            foreach (var referencePropertyType in referencePropertyTypes)
            {
                var entityType = entityTypes.FirstOrDefault(entity => entity.Name == referencePropertyType.Name);

                if (entityType != null)
                {
                    referenceEntityTypes.Add(entityType);
                }
            }

            return referenceEntityTypes;
        }

        public static IEnumerable<OdcmProperty> FilterProperties(IEnumerable<OdcmProperty> properties, string typeName = null, string longDescriptionMatches = null)
        {
            var allProperties = properties;
            if (typeName != null)
            {
                allProperties = allProperties.Where(prop => prop.Projection.Type.Name.Equals(typeName));
            }
            if (longDescriptionMatches != null)
            {
                allProperties = allProperties.Where(prop => prop.LongDescriptionContains(longDescriptionMatches));
            }
            return allProperties;
        }

        public static IEnumerable<OdcmProperty> GetProperties(this OdcmClass entity, string typeName = null, string longDescriptionMatches = null)
        {
            return FilterProperties(entity.Properties, typeName, longDescriptionMatches);
        }

        public static IEnumerable<OdcmProperty> GetProperties(this OdcmComplexClass complexClass, string typeName = null, string longDescriptionMatches = null)
        {
            return FilterProperties(complexClass.Properties, typeName, longDescriptionMatches);
        }

        public static IEnumerable<OdcmEnum> GetEnumTypes(this OdcmModel model)
        {
            var namespaces = GetOdcmNamespaces(model);
            return namespaces.SelectMany(@namespace => @namespace.Types.OfType<OdcmEnum>());
        }

        public static IEnumerable<OdcmMethod> GetMethods(this OdcmModel model)
        {
            return model.GetEntityTypes().SelectMany(entityType => entityType.Methods);
        }

        /// <summary>
        /// This extension method determines whether the current type needs to be disambiguated.
        /// If the current type needs dismabiguation, we then provide a fully qualified 
        /// import statement for the model.
        /// This currently operates only on entities whose name ends in "Request". We do this
        /// because every entity results in a request object. For example, assume we have the following
        /// two entities: timeOff and timeOffRequest. The timeOff entity will trigger the generation
        /// of classes named model.timeOff and request.timeOffRequest. The timeOffRequest entity
        /// will trigger the generation of a model.timeOffRequest and request.timeOffRequestRequest.
        /// The request.timeOffRequest and model.timeOffRequest classes will result in a name collision in
        /// a few files.
        /// Assumptions: 1) host.CurrentType is an OdcmProperty.
        /// </summary>
        /// <param name="host">The T4Host that orchestrates applying templates to the OdcmModel.</param>
        /// <returns>A boolean value that indicates whether the current type needs to be disambiguated.</returns>
        public static bool DoesCurrentTypeNeedDisambiguation(this CustomT4Host host)
        {   
            // At this point this is only applicable to OdcmProperty.
            // Challenging this assumption will require a lot more investigation.
            if (!(host.CurrentType is OdcmProperty))
                return false; 

            // We only support "Request" dismabiguation at this point. Check whether the
            // current type ends in "Request".
            var requestSuffix = "Request";
            var currentTypeName = (host.CurrentType as OdcmProperty).Type.Name;
            int index = currentTypeName.IndexOf(requestSuffix);
            if (index == -1 || !currentTypeName.EndsWith(requestSuffix))
                return false; // Doesn't need disambiguation

            // If it does end in "Request", let's capture the base name to check if an entity of that name 
            // exists in the schema.
            string entityNameToCheckForCollision = currentTypeName.Remove(index, requestSuffix.Length);

            // Search across namespaces, looking only at EntityType, to determine whether this type requires 
            // disambiguation. This needs to be supported across namespaces.
            var classes = host.CurrentModel.Namespaces.SelectMany(n => n.Classes);
            var shouldDisambiguate = classes.Where(entity => entity.Kind == OdcmClassKind.Entity
                                                       && entity.Name == entityNameToCheckForCollision).Any();

            return shouldDisambiguate;
        }

        /// <summary>
        /// An extension method to get an import statement for the fully qualified name of the current type.
        /// Assumptions: 1) host.CurrentType is an OdcmProperty. 2) the generated namespace of the current type 
        /// is in models.generated output namespace (in the generated file, not in the metadata).
        /// This method should support multiple namespaces.
        /// This currently (6/2020) applies to the following templates:
        ///   BaseEntityCollectionRequest.java.tt
        ///   IBaseEntityCollectionRequest.java.tt
        ///   IBaseEntityCollectionPage.java.tt
        /// This currently unintentionally applies to the following templates when the disambiguation condition is met.
        /// This is not an issue as there is already a wild card import for the namespace that we should address first.
        ///   IBaseEntityCollectionRequestBuilder.java.tt
        ///   BaseEntityCollectionRequestBuilder.java.tt
        /// </summary>
        /// <param name="host">The T4Host that orchestrates applying templates to the OdcmModel.</param>
        /// <returns>A string that represents the import statement of the fully qualified name of the current type.</returns>
        public static string GetFullyQualifiedImportStatementForModel(this CustomT4Host host)
        {
            // By default, we don't need to disambiguate the model in the generated code file.
            // This will be the general case.
            var importStatement = "";

            // Check whether we need to disambiguate the current type for generation of the model in the code file.
            var shouldDisambiguate = host.DoesCurrentTypeNeedDisambiguation();

            if (shouldDisambiguate)
            {
                // Form the import statement to disambiguate the model in the generated code file.
                var thisType = (host.CurrentType as OdcmProperty).Projection.Type;
                var thisNamespace = thisType.Namespace.Name.AddPrefix();
                var thisTypeName = thisType.Name.ToUpperFirstChar();
                importStatement = $"\nimport {thisNamespace}.models.extensions.{thisTypeName};";
            }

            return importStatement;
        }

        /// <summary>
        /// Get the service collection navigation property for the given property type
        /// 
        /// We have revised the LINQ statement to support the following OData rules 
        /// regarding containment:
        /// 
        /// 1. If a navigation property specifies "ContainsTarget='true'", it is self-contained. 
        ///    Generate a direct path to the item (ie "parent/child").
        /// 2. If a navigation property does not specify ContainsTarget but there is a defined EntitySet 
        ///    of the given type, it is a reference relationship. Generate a reference path to the item (ie "item/$ref").
        /// 3. If a navigation property does not have a defined EntitySet but there is a Singleton which has 
        ///    a self-contained reference to the given type, we can make a relationship to the implied EntitySet of 
        ///    the singleton. Generate a reference path to the item (ie "singleton/item/$ref").
        /// 4. If none of the above pertain to the navigation property, it should be treated as a metadata error.
        /// </summary>
        public static OdcmProperty GetServiceCollectionNavigationPropertyForPropertyType(this OdcmProperty odcmProperty, OdcmModel model)
        {
            // Try to find the first collection navigation property for the specified type directly on the service
            // class object. If an entity is used in a reference property a navigation collection
            // on the client for that type is required.
            try
            {
                var explicitProperty = GetOdcmNamespaces(model)
                    .SelectMany(
                        @namespace =>
                        @namespace
                        .Classes
                        .Where(odcmClass => odcmClass.Kind == OdcmClassKind.Service)
                        .SelectMany(service => (service as OdcmServiceClass).NavigationProperties())
                        .Where(property => property.IsCollection && property.Projection.Type.FullName.Equals(odcmProperty.Projection.Type.FullName))
                    ).FirstOrDefault();

                if (explicitProperty != null)
                    return explicitProperty;

                // Check the singletons for a matching implicit EntitySet
                else
                {
                    var implicitProperty = GetOdcmNamespaces(model)
                        .SelectMany(
                            @namespace =>
                            @namespace
                            .Classes
                            .Where(odcmClass => odcmClass.Kind == OdcmClassKind.Service)
                            .First()
                            .Properties
                            .Where(property => property.GetType() == typeof(OdcmSingleton)) //Get the list of singletons defined by the service
                            .Where(singleton => singleton
                                .Type
                                .AsOdcmClass()
                                .Properties
                                //Find navigation properties on the singleton that are self-contained (implicit EntitySets) that match the type
                                //we are searching for
                                .Where(prop => prop.ContainsTarget == true && prop.Type.Name == odcmProperty.Type.Name)
                                .FirstOrDefault() != null
                             )
                         ).FirstOrDefault();

                    if (implicitProperty != null)
                        return implicitProperty;
                }
                //If we are unable to find a valid EntitySet for the property, treat this
                //as an exception so the service has an opportunity to correct this in the metadata
                throw new Exception("Found no valid EntitySet for the given property.");

            } catch (Exception e)
            {
                logger.Error("The navigation property \"{0}\" on class \"{1}\" does not specify it is self-contained nor is it defined in an explicit or implicit EntitySet", odcmProperty.Name.ToString(), odcmProperty.Class.FullName.ToString());
                logger.Error(e);
                return null;
            }
        }

        public static string GetImplicitPropertyName(this OdcmProperty property, OdcmSingleton singleton)
        {
            var implicitPropertyName = property.Name;
            // Default behavior
            if (singleton.NavigationPropertyBindings.Count() > 0)
            {
                var target = singleton.NavigationPropertyBindings.Where(kv => kv.Key.EndsWith(property.Name)).Select(kvp => kvp.Value).FirstOrDefault();
                if (target != null)
                {
                    implicitPropertyName = target;
                }
            }
            return implicitPropertyName;
        }

        /// <summary>
        /// Indicates whether the abstract base class of a descendant is referenced as the type of a structural property.
        /// This has significance to scenarios where client needs to set the @odata.type property because the type cannot
        /// be inferred since the reference is the to abstract base class and the actual class used by the client is
        /// one of its descendants. 
        /// </summary>
        /// <param name="complex">The ComplexType that we want to query whether its base type is the referenced type for any property in any entity.</param>
        /// <returns></returns>
        public static bool IsBaseAbstractAndReferencedAsPropertyType(this OdcmClass complex)
        {
            if (complex.Base == null)
            {
                return false;
            }
            else
            {
                return complex.Namespace.Types.Select(someType => (someType as OdcmEntityClass))
                               .Where(someType => someType is OdcmEntityClass)
                               .Where(someType => (someType as OdcmEntityClass).Properties
                                    .Any(x => x.Type.Name == complex.Base.Name && complex.Base.IsAbstract))
                               .Any();
            }
        }

        public static IEnumerable<OdcmProperty> NavigationProperties(this OdcmClass odcmClass)
        {
            return odcmClass.Properties.Where(prop => prop.IsNavigation());
        }

        public static bool IsNavigation(this OdcmProperty property)
        {
            return property.IsLink;
        }

        public static bool IsReference(this OdcmProperty property)
        {
            var propertyClass = property.Class.AsOdcmClass();

            return propertyClass.Kind != OdcmClassKind.Service && property.IsLink && !property.ContainsTarget;
        }

        public static bool HasActions(this OdcmClass odcmClass)
        {
            return odcmClass.Methods.Any();
        }

        public static IEnumerable<OdcmMethod> Actions(this OdcmClass odcmClass)
        {
            return odcmClass.Methods;
        }

        public static bool IsAction(this OdcmMethod method)
        {
            return !method.IsFunction;
        }

        public static string GetNamespace(this OdcmModel model)
        {
            var @namespace = model.Namespaces
                .Find(x =>
                    string.Equals(x.Name, ConfigurationService.Settings.PrimaryNamespaceName, StringComparison.InvariantCultureIgnoreCase));
            @namespace = @namespace ?? GetOdcmNamespaces(model).First();
            return @namespace.Name;
        }

        public static OdcmClass AsOdcmClass(this OdcmObject odcmObject)
        {
            return odcmObject as OdcmClass;
        }

        public static OdcmEnum AsOdcmEnum(this OdcmObject odcmObject)
        {
            return odcmObject as OdcmEnum;
        }

        public static OdcmProperty AsOdcmProperty(this OdcmObject odcmObject)
        {
            return odcmObject as OdcmProperty;
        }

        public static OdcmMethod AsOdcmMethod(this OdcmObject odcmObject)
        {
            return odcmObject as OdcmMethod;
        }

        public static OdcmClass BaseClass(this OdcmObject odcmObject)
        {
            if (odcmObject.AsOdcmProperty() != null && odcmObject.AsOdcmProperty().Projection.Type.AsOdcmClass() != null)
            {
                var baseClass = odcmObject.AsOdcmProperty().Projection.Type.AsOdcmClass().Base;
                if (baseClass != null && !baseClass.IsAbstract)
                {
                    return baseClass;
                }
            }
            else if (odcmObject.AsOdcmClass() != null && odcmObject.AsOdcmClass().Base != null)
            {
                if (!odcmObject.AsOdcmClass().Base.IsAbstract)
                {
                    return odcmObject.AsOdcmClass().Base;
                }
            }
            return null;
        }

        public static bool HasDerived(this OdcmObject odcmObject)
        {
            if (odcmObject.AsOdcmClass() != null)
            {
                return odcmObject.AsOdcmClass().Derived.Any();
            }
            else if (odcmObject.AsOdcmProperty() != null && odcmObject.AsOdcmProperty().Projection.Type.AsOdcmClass() != null)
            {
                return odcmObject.AsOdcmProperty().Projection.Type.AsOdcmClass().Derived.Any();
            }
            else if (odcmObject.AsOdcmMethod() != null && odcmObject.AsOdcmMethod().ReturnType.AsOdcmClass() != null)
            {
                return odcmObject.AsOdcmMethod().ReturnType.AsOdcmClass().Derived.Any();
            }
            return false;
        }

        public static string AddPrefix(this OdcmNamespace @namespace)
        {
            return @namespace.ToString().AddPrefix();
        }

        public static string AddPrefix(this string @namespace)
        {
            if (string.IsNullOrEmpty(ConfigurationService.Settings.NamespaceOverride))
            {
                var name = string.Format("{0}.{1}", ConfigurationService.Settings.NamespacePrefix, @namespace).ToLower();

                // special case com.edm happens when we reach here from a property and property is an edm type, e.g. Stream.
                return name == "com.edm" ? "com.microsoft.graph" : name;
            }
            return ConfigurationService.Settings.NamespaceOverride;
        }

        public static string ODataPackageNamespace(this OdcmModel model)
        {
            var @namespace = model.Namespaces
                .Find(x =>
                    string.Equals(x.Name, ConfigurationService.Settings.PrimaryNamespaceName, StringComparison.InvariantCultureIgnoreCase));
            @namespace = @namespace ?? GetOdcmNamespaces(model).First();
            var package = string.Format("{0}.{1}", @namespace, "fetchers");
            return package.ToLower();
        }

        public static string GetEntityContainer(this OdcmModel model)
        {
            return model.EntityContainer.Name;
        }

        public static bool LongDescriptionContains(this OdcmObject odcmObject, string descriptionValue)
        {
            var descriptionParts = odcmObject.GetLongDescriptionSegments();
            return descriptionParts != null && descriptionParts.Contains(descriptionValue);
        }

        public static bool LongDescriptionStartsWith(this OdcmObject odcmObject, string descriptionValue)
        {
            var descriptionParts = odcmObject.GetLongDescriptionSegments();
            return descriptionParts != null && descriptionParts.Any(value => value.StartsWith(descriptionValue));
        }

        public static string[] GetLongDescriptionSegments(this OdcmObject odcmObject)
        {
            if (odcmObject.LongDescription != null)
            {
                return odcmObject.LongDescription.Split(';');
            }

            return null;
        }

        /// Returns a List containing the supplied method plus its overloads
        public static List<OdcmMethod> WithOverloads(this OdcmMethod odcmMethod)
        {
            var methods = new List<OdcmMethod>();
            methods.Add(odcmMethod);
            methods.AddRange(odcmMethod.Overloads);
            return methods;
        }

        /// Returns a List containing the supplied class' methods plus their overloads
        public static List<OdcmMethod> MethodsAndOverloads(this OdcmClass odcmClass)
        {
            var allMethods = new List<OdcmMethod>();
            foreach (var method in odcmClass.Methods)
            {
                allMethods.AddRange(method.WithOverloads());
            }
            return allMethods;
        }

    }

}
