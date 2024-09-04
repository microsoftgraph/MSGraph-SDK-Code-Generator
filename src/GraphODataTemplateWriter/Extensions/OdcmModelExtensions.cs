﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.Extensions
{
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Vipr.Core.CodeModel;
    using System.Text.RegularExpressions;

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
        public static bool IsStreamedEntity(this OdcmClass odcmClass) {
            if(odcmClass is OdcmMediaClass) return true;
            else if (odcmClass?.Base == null) return false;
            else return IsStreamedEntity(odcmClass.Base);
        }
        public static IEnumerable<OdcmClass> GetMediaEntityTypes(this OdcmModel model)
        {
            var namespaces = GetOdcmNamespaces(model);
            return namespaces.SelectMany(@namespace => @namespace.Classes.Where(x => x.IsStreamedEntity()));
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
                // use the FullName to match the types as there could be multiple types of the same name but in different namespaces
                // e.g microsoft.graph.group and microsoft.graph.termstore.group
                var entityType = entityTypes.FirstOrDefault(entity => entity.FullName == referencePropertyType.FullName);

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
        /// This currently unintentionally applies to the following templates when the disambiguation condition is met.
        /// This is not an issue as there is already a wild card import for the namespace that we should address first.
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
                importStatement = $"\nimport {thisNamespace}.models.{thisTypeName};";
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
        /// 4. If none of the above pertain. Try to call <see cref="IsPropertyChainedToContainedServiceNavigationProperty"/> to ascertain if this is a metadata error as
        ///    the relationship could be more than one level deep. (The nav property we are looking for could be a navigation property of another navigation property :) ).
        /// 5. If none of the above pertain to the navigation property, it should be treated as a metadata error.
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
                            .FirstOrDefault()
                            ?.Properties
                            ?.Where(property => property.GetType() == typeof(OdcmSingleton)) //Get the list of singletons defined by the service
                            ?.Where(singleton => singleton
                                .Type
                                .AsOdcmClass()
                                ?.Properties
                                //Find navigation properties on the singleton that are self-contained (implicit EntitySets) that match the type
                                //we are searching for
                                .Where(prop => prop.ContainsTarget == true && prop.Type.Name == odcmProperty.Type.Name)
                                .FirstOrDefault() != null
                             ) ?? new OdcmProperty[] { }
                         ).FirstOrDefault();

                    if (implicitProperty != null)
                        return implicitProperty;
                }
                //If we are unable to find a valid EntitySet for the property, treat this
                //as an exception so the service has an opportunity to correct this in the metadata
                throw new Exception("Found no valid EntitySet for the given property.");

            }
            catch (Exception e)
            {
                logger.Error("The navigation property \"{0}\" on class \"{1}\" does not specify it is self-contained nor is it defined in an explicit or implicit EntitySet", odcmProperty.Name.ToString(), odcmProperty.Class.FullName.ToString());
                logger.Error(e);
                return null;
            }
        }

        /// <summary>
        /// This method takes a specific property and determines whether its could be referenced in the navigation property of another type.
        /// The navigation property must also come from chain where the containTarget=true from the root Service class to prove that we can reference it.
        /// Ideally we should call this function if GetServiceCollectionNavigationPropertyForPropertyType returns null
        /// </summary>
        /// <param name="odcmProperty">The property to check</param>
        /// <param name="model">The model to use</param>
        /// <returns></returns>
        public static bool IsPropertyChainedToContainedServiceNavigationProperty(this OdcmProperty odcmProperty, OdcmModel model)
        {
            // keep track of the types we've traversed to avoid circular references
            var navigatedTypes = new HashSet<string>();
            var matchingRootProperty = GetOdcmNamespaces(model)
                .SelectMany(
                    @namespace =>
                        @namespace
                            .Classes
                            .FirstOrDefault(odcmClass => odcmClass.Kind == OdcmClassKind.Service)           // find the root service class
                            ?.Properties
                            ?.Where(serviceProperty => serviceProperty.GetType() == typeof(OdcmSingleton))  // find the singleton properties
                            ?.Where(singleton =>
                                        singleton.
                                                Type.AsOdcmClass()
                                                ?.Properties
                                                ?.Where(property => property.ContainsTarget) // find singleton type properties that are self contained 
                                                ?.Any(property => property.IsPropertyTypeChainedContainedNavigationProperty( //  keep following the containsTarget=True to find if we can use a reference
                                                                                odcmProperty,
                                                                                navigatedTypes,
                                                                                property.IsCollection
                                                                                    ? $"{singleton.Name}/{property.Name}/{{id}}"
                                                                                    : $"{singleton.Name}/{property.Name}")
                                                ) ?? false
                            ) ?? Array.Empty<OdcmProperty>()
                    ).FirstOrDefault();

            return matchingRootProperty != null;
        }

        /// <summary>
        /// This method takes in a property and looks through to see if the given testProperty could be contained in it through its contained navigation properties or
        /// its nested contained navigation properties.
        /// </summary>
        /// <param name="odcmRootProperty">The property to start the search from</param>
        /// <param name="testProperty">The property to find in the from the root type</param>
        /// <param name="navigatedTypes">The types already navigated to prevent circular references</param>
        /// <param name="route">The route followed to get here</param>
        /// <returns></returns>
        private static bool IsPropertyTypeChainedContainedNavigationProperty(this OdcmProperty odcmRootProperty, OdcmProperty testProperty, HashSet<string> navigatedTypes, string route)
        {
            // check if the property already matches the type.
            if (odcmRootProperty.Type.FullName.Equals(testProperty.Type.FullName, StringComparison.OrdinalIgnoreCase))
            {
                logger.Info("Property \"{0}\" matches self contained navigation property \"{1}\" of type \"{2}\"", testProperty.Name, odcmRootProperty.Name, odcmRootProperty.Type.FullName);
                logger.Info("Possible route from service class is: {0}{1}{1}",route,Environment.NewLine);
                return true;
            }

            // check if the base class is referenced instead.
            if (odcmRootProperty.AsOdcmClass()?.IsAncestorOfType(testProperty.AsOdcmClass() ?? null) ?? false)
            {
                logger.Info("Property \"{0}\" of type \"{1}\" matches self contained navigation property \"{2}\" of Base type \"{3}\"", testProperty.Name, testProperty.Type.FullName, odcmRootProperty.Name, odcmRootProperty.Type.FullName);
                logger.Info("Possible route from service class is: {0}{1}{1}", route, Environment.NewLine);
                return true;
            }

            // its not this type so lets add it to the exclusion list
            navigatedTypes.Add(odcmRootProperty.Type.FullName);

            // The current type does not match. So, we get the list of contained nav properties from the given type and recursively subject it to the same test
            var matchingProperty = odcmRootProperty.Type.AsOdcmClass()?
                                        .Properties
                                        .Where(prop => prop.ContainsTarget)// the nave properties with containsTarget
                                        .Where(prop => !navigatedTypes.Contains(prop.Type.FullName)) // prevent any cycle business
                                        .FirstOrDefault(prop => prop.IsPropertyTypeChainedContainedNavigationProperty(testProperty, navigatedTypes, prop.IsCollection ? $"{route}/{prop.Name}/{{id}}" : $"{route}/{prop.Name}")
            );

            return matchingProperty != null;
        }

        /// <summary>
        /// This method tries to determine if a certain class is an ancestor of the other class.
        /// </summary>
        /// <param name="odcmClass"></param>
        /// <param name="testClass"></param>
        /// <returns></returns>
        private static bool IsAncestorOfType(this OdcmClass odcmClass, OdcmClass testClass)
        {
            if (testClass?.Base == null || testClass == null)
            {
                return false; // no base type. end of the road
            }

            // check if the base is a match. Otherwise keep going up by trying the test class' base
            return odcmClass.FullName.Equals(testClass.Base.FullName, StringComparison.OrdinalIgnoreCase) ||
                   odcmClass.IsAncestorOfType(testClass.Base);
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

        /// <summary>
        /// Indicates whether class has any properties that are enums
        /// </summary>
        /// <param name="complex">The ComplexType that we want to query whether its base type is the referenced type for any property in any entity.</param>
        /// <returns></returns>
        public static bool HasEnumProperties(this OdcmClass complex)
        {
            return complex.Properties.Any(property => property.Type.AsOdcmEnum() != null);
        }

        /// <summary>
        /// Indicates whether the entity's base type is referenced as the type of another property.
        /// TODO: remove filters on "entity" once we have annotation support.
        /// </summary>
        /// <param name="odcmClass">The OdcmClass to inspect.</param>
        /// <returns>A value of true indicates that OdcmClass's base type is referenced as the type
        /// of another property in a different entity or complex type.</returns>
        public static bool IsBaseReferencedAsPropertyType(this OdcmClass odcmClass)
        {
            if (odcmClass.Base == null)
            {
                return false;
            }
            else
            {
                var isReferencedInAction = odcmClass.Namespace.Classes
                    .SelectMany(c => c.Methods)  
                    .Where( m => !m.IsFunction) // only get the Actions
                    .Any(m => m.Parameters
                        .Any( param => param.Type.Name.Equals(odcmClass.Base.Name, StringComparison.OrdinalIgnoreCase) 
                                  && !"entity".Equals(param.Type.Name, StringComparison.OrdinalIgnoreCase)));

                var isReferencedInClass = odcmClass.Namespace.Types
                    .OfType<OdcmClass>()
                    .Any(someType => someType.Properties
                        .Any(x => x.Type.Name.Equals(odcmClass.Base.Name, StringComparison.OrdinalIgnoreCase)
                                  && !"entity".Equals(x.Type.Name, StringComparison.OrdinalIgnoreCase)));

                return (isReferencedInAction || isReferencedInClass);
            }
        }

        /// <summary>
        /// Indicates whether any type in the entity's or complexType's base type chain is referenced 
        /// by any property in any other entity or complex type. This method recursively inspects
        /// the base type chain for any instance of the base type that is referenced by any other 
        /// type.
        /// </summary>
        /// <param name="entityOrComplexType">The OdcmClass to inspect.</param>
        /// <returns>A value of true indicates that the base type is referenced; other, false.</returns>
        public static bool IsBaseTypeReferenced(this OdcmClass entityOrComplexType)
        {
            return entityOrComplexType.IsBaseReferencedAsPropertyType() || (entityOrComplexType.Base?.IsBaseTypeReferenced() ?? false);
        }

        public static IEnumerable<OdcmProperty> NavigationProperties(this OdcmClass odcmClass, bool includeBaseProperties = false)
        {
            if (includeBaseProperties && odcmClass.Base != null)
                return odcmClass.Base.NavigationProperties(includeBaseProperties)
                            .Union(odcmClass.Properties.Where(prop => prop.IsNavigation()));
            else
                return odcmClass.Properties.Where(prop => prop.IsNavigation());
        }

        public static bool IsNavigation(this OdcmProperty property)
        {
            return property.IsLink;
        }

        private static Regex castOverloadsFilter = new Regex(@"As[A-Z]");
        public static bool IsReference(this OdcmProperty property)
        {
            var propertyClass = property.Class.AsOdcmClass();

            return propertyClass.Kind != OdcmClassKind.Service && property.IsLink && !property.ContainsTarget && !castOverloadsFilter.IsMatch(property.Name);
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
            var methods = new List<OdcmMethod>
            {
                odcmMethod
            };
            methods.AddRange(odcmMethod.Overloads);
            return methods;
        }
        private static readonly OdcmMethodEqualityComparer methodComparer = new OdcmMethodEqualityComparer();
        public static List<OdcmMethod> WithDistinctOverloads(this OdcmMethod odcmMethod)
        {
            var methods = odcmMethod.WithOverloads();

            return methods.Distinct(methodComparer).ToList();
        }

        private static readonly OdcmParameterEqualityComparer paramComparer = new OdcmParameterEqualityComparer();
        /// <summary>
        /// Deduplicates the parameter list for an overloaded method. 
        /// </summary>
        /// <param name="odcmMethod">Method with potential overloads and duplicate parameters across overloads.</param>
        /// <returns>A deduplicated list of OdcmParameter.</returns>
        public static List<OdcmParameter> WithDistinctParameters(this OdcmMethod odcmMethod)
        {
            var distinctMethods = odcmMethod.WithDistinctOverloads();

            var parameters = new List<OdcmParameter>();

            foreach (var method in distinctMethods)
            {
                parameters.AddRange(method.Parameters);
            }

            return parameters.Distinct(paramComparer).ToList();
        }
        /// <summary>
        /// Deduplicates the parameter list for a set of methods. 
        /// </summary>
        /// <param name="odcmMethods">Methods with potential overloads and duplicate parameters across overloads.</param>
        /// <returns>A deduplicated list of OdcmParameter.</returns>
        public static List<OdcmParameter> WithDistinctParameters(this IEnumerable<OdcmMethod> odcmMethods)
        {
            return odcmMethods?.SelectMany(x => x.Parameters)?.Distinct(paramComparer)?.ToList();
        }

        /// Returns a List containing the supplied class' methods plus their overloads
        public static IEnumerable<OdcmMethod> MethodsAndOverloads(this OdcmClass odcmClass)
        {
            return odcmClass
                    ?.Methods
                    ?.SelectMany(x => x.WithOverloads())
                    ?.Union(odcmClass?.Base?.MethodsAndOverloads() ?? Enumerable.Empty<OdcmMethod>()) ?? Enumerable.Empty<OdcmMethod>();
        }
        private static readonly OdcmMethodEqualityComparer methodNameAndParametersCountComparer = new OdcmMethodEqualityComparer {
            CompareParameters = false,
            CompareParametersCount = false,
            CompareHasParameters = true
        };
        private static readonly OdcmMethodEqualityComparer methodNameAndParametersCountAndIgnoreCollectionBindingComparer = new OdcmMethodEqualityComparer {
            CompareParameters = false,
            CompareParametersCount = false,
            CompareHasParameters = true,
            CompareBoundToCollection = false
        };
        public static IEnumerable<OdcmMethod> MethodsAndOverloadsWithDistinctName(this OdcmClass odcmClass)
        {
            return odcmClass
                    ?.Methods
                    ?.SelectMany(x => x.WithOverloads())
                    ?.Union(odcmClass?.Base?.MethodsAndOverloadsWithDistinctName() ?? Enumerable.Empty<OdcmMethod>())
                    ?.Distinct(methodNameAndParametersCountComparer) ?? Enumerable.Empty<OdcmMethod>();
        }
        public static IEnumerable<OdcmMethod> WithOverloadsOfDistinctName(this OdcmMethod m)
        {
            return m?.WithOverloads()?.Distinct(methodNameAndParametersCountComparer) ?? Enumerable.Empty<OdcmMethod>();
        }
        
        public static IEnumerable<OdcmMethod> WithOverloadsOfDistinctNameIgnoringCollectionBinding(this OdcmMethod m)
        {
            return m?.WithOverloads()?.Distinct(methodNameAndParametersCountAndIgnoreCollectionBindingComparer) ?? Enumerable.Empty<OdcmMethod>();
        }

        /// <summary>
        /// Use this method to get a collection of navigation properties on the return type 
        /// of a composable function. 
        /// </summary>
        /// <param name="odcmMethod">The OdcmMethod to target.</param>
        /// <returns>An ordered (by name) list of navigation properties bound 
        /// to the return type. Can be an empty list.</returns>
        public static List<OdcmProperty> GetComposableFunctionReturnTypeNavigations(this OdcmMethod odcmMethod)
        {
            if (!odcmMethod.IsComposable)
                throw new InvalidOperationException("This extension method is intended " +
                                                    "to only be called on a composable function.");

            return (odcmMethod.ReturnType as OdcmClass)?.Properties
                                                       .Where(p => p.IsLink)
                                                       .OrderBy(p => p.Name)
                                                       .ToList() ?? new List<OdcmProperty>(); // default to empty list
        }

        /// <summary>
        /// Use this method to get a collection of methods on the return type 
        /// of a composable function. This will include the methods and overloads.
        /// </summary>
        /// <param name="odcmMethod">The OdcmMethod to target.</param>
        /// <returns>An ordered (by name) list of methods bound to the return 
        /// type. Can be an empty list.</returns>
        public static List<OdcmMethod> GetComposableFunctionReturnTypeMethods(this OdcmMethod odcmMethod)
        {
            if (!odcmMethod.IsComposable)
                throw new InvalidOperationException("This extension method is intended " +
                                                    "to only be called on a composable function.");

            return odcmMethod.ReturnType.AsOdcmClass().MethodsAndOverloads()
                                                       .OrderBy(m => m.Name)
                                                       .ToList();
        }
    }
}
