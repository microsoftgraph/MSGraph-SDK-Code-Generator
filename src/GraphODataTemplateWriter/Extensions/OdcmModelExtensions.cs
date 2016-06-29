// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Vipr.Core.CodeModel;
    using System.Text.RegularExpressions;
    public static class OdcmModelExtensions
    {
        private static readonly Regex CollectionRegex = new Regex(@"Collection\((?<typeDefinition>[^\)]+)\)");
        private static readonly Regex TypeNameRegex = new Regex(@"(?<typeNamespace>.*)\.(?<typeName>[^.]+)");

        public static bool IsCollection(this OdcmProperty odcmProperty)
        {
            return odcmProperty.IsCollection;
        }

        private static OdcmNamespace GetOdcmNamespace(OdcmModel model)
        {
            OdcmNamespace namespaceFound;
            var filtered = model.Namespaces.Where(x => !x.Name.Equals("Edm", StringComparison.InvariantCultureIgnoreCase))
                                           .ToList();
            if (filtered.Count() == 1)
            {
                namespaceFound = filtered.Single();
            }
            else
            {
                namespaceFound =
                    model.Namespaces.Find(x => String.Equals(x.Name, ConfigurationService.Settings.PrimaryNamespaceName,
                        StringComparison.InvariantCultureIgnoreCase));
            }

            if (namespaceFound == null)
            {
                throw new InvalidOperationException("Multiple namespaces defined in metadata and no matches." +
                                                    "\nPlease check 'PrimaryNamespace' Setting in 'config.json'");
            }
            return namespaceFound;
        }

        public static IEnumerable<OdcmClass> GetComplexTypes(this OdcmModel model)
        {
            var @namespace = GetOdcmNamespace(model);
            return @namespace.Classes.Where(x => x is OdcmComplexClass);
        }

        public static IEnumerable<OdcmClass> GetEntityTypes(this OdcmModel model)
        {
            var @namespace = GetOdcmNamespace(model);
            return @namespace.Classes.Where(x => x is OdcmEntityClass || x is OdcmMediaClass);
        }

        public static IEnumerable<OdcmClass> GetMediaEntityTypes(this OdcmModel model)
        {
            var @namespace = GetOdcmNamespace(model);
            return @namespace.Classes.Where(x => x is OdcmMediaClass);
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
            var referencePropertyTypes = model.GetProperties().Where(prop => prop.IsReference() && !prop.IsCollection).Select(prop => prop.Projection.Type).Distinct();

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


        public static IEnumerable<OdcmProperty> GetAdditionalPropertiesForMethodCollectionResponse(this OdcmMethod method, OdcmModel model)
        {
            if (ConfigurationService.Settings.AdditionalMethodProperties == null)
            {
                return null;
            }

            Dictionary<string, Dictionary<string, string>> currentMethodMappings = null;

            if (!ConfigurationService.Settings.AdditionalMethodProperties.TryGetValue(method.FullName, out currentMethodMappings))
            {
                return null;
            }

            Dictionary<string, string> returnTypeMappingsForMethod = null;

            if (!currentMethodMappings.TryGetValue("CollectionResponse", out returnTypeMappingsForMethod))
            {
                return null;
            }

            return OdcmModelExtensions.GetAdditionalProperties(returnTypeMappingsForMethod, model);
        }

        public static IEnumerable<OdcmProperty> GetAdditionalProperties(Dictionary<string, string> additionalPropertyMappings, OdcmModel model)
        {
            if (additionalPropertyMappings == null)
            {
                return null;
            }

            var properties = new List<OdcmProperty>();

            foreach (var configValue in additionalPropertyMappings)
            {
                var collectionMatch = CollectionRegex.Match(configValue.Value);

                string typeDefinition = null;

                if (collectionMatch.Success)
                {
                    typeDefinition = collectionMatch.Groups["typeDefinition"].Value;
                }
                else
                {
                    typeDefinition = configValue.Value;
                }

                var typeNameMatch = TypeNameRegex.Match(typeDefinition);

                if (!typeNameMatch.Success)
                {
                    throw new ArgumentException(string.Format("Additional property type for {0} must include namespace.", configValue.Key));
                }

                var typeNamespace = typeNameMatch.Groups["typeNamespace"].Value;
                var typeName = typeNameMatch.Groups["typeName"].Value;

                OdcmType odcmType = null;

                if (!model.TryResolveType<OdcmType>(typeName, typeNamespace, out odcmType))
                {
                    throw new ArgumentException(string.Format("Invalid type specified for additional property {0}.", configValue.Key));
                }

                var odcmProjection = new OdcmProjection { Type = odcmType };
                var odcmProperty = new OdcmProperty(configValue.Key) { Projection = odcmProjection, IsCollection = collectionMatch.Success };

                properties.Add(odcmProperty);
            }

            return properties;
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
            var @namespace = GetOdcmNamespace(model);
            return @namespace.Types.OfType<OdcmEnum>();
        }

        public static IEnumerable<OdcmMethod> GetAsyncMethods(this OdcmModel model)
        {
            return model.GetEntityTypes().SelectMany(entityType => entityType.Methods).Where(method => method.IsAsync());
        }

        public static IEnumerable<OdcmMethod> GetMethods(this OdcmModel model)
        {
            return model.GetEntityTypes().SelectMany(entityType => entityType.Methods);
        }

        public static OdcmProperty GetServiceCollectionNavigationPropertyForPropertyType(this OdcmProperty odcmProperty)
        {
            // Try to find the first collection navigation property for the specified type directly on the service
            // class object. Use First() instead of FirstOrDefault() so template generation would fail if not found
            // instead of silently continuing. If an entity is used in a reference property a navigation collection
            // on the client for that type is required. 
            return odcmProperty
                .Class
                .Namespace
                .Classes
                .Where(odcmClass => odcmClass.Kind == OdcmClassKind.Service)
                .SelectMany(service => (service as OdcmServiceClass).NavigationProperties())
                .First(property => property.IsCollection && property.Projection.Type.FullName.Equals(odcmProperty.Projection.Type.FullName));
        }

        public static bool IsAsync(this OdcmMethod method)
        {
            return ConfigurationService.Settings.AsyncMethods.Contains(method.FullName);
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
            return method.Verbs == OdcmAllowedVerbs.Post;
        }

        public static bool IsFunction(this OdcmMethod method)
        {
            return method.IsComposable; //TODO:REVIEW
        }

        public static string GetNamespace(this OdcmModel model)
        {
            var @namespace = GetOdcmNamespace(model);
            return @namespace.Name;
        }

        public static OdcmClass AsOdcmClass(this OdcmObject odcmObject)
        {
            return odcmObject as OdcmClass;
        }

        public static OdcmEnum AsOdcmEnum(this OdcmObject odcmObject)
        {
            OdcmEnum foo = odcmObject as OdcmEnum;
            var bar = foo.Members.LastOrDefault();
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

        public static string NamespaceName(this OdcmModel model)
        {
            if (string.IsNullOrEmpty(ConfigurationService.Settings.NamespaceOverride))
            {
                var @namespace = GetOdcmNamespace(model).Name;
                var name = string.Format("{0}.{1}", ConfigurationService.Settings.NamespacePrefix, @namespace);
                return name.ToLower();
            }
            return ConfigurationService.Settings.NamespaceOverride;
        }

        public static string ODataPackageNamespace(this OdcmModel model)
        {
            var @namespace = NamespaceName(model);
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

    }

}
