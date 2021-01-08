// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Java
{
    using System.Collections.Generic;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Vipr.Core.CodeModel;
    using System;
    using NLog;
    using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;
    using System.Text;
    using System.Linq;
    using Microsoft.Graph.ODataTemplateWriter.CodeHelpers.CSharp;
    using System.Text.RegularExpressions;

    public static class TypeHelperJava
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public const string ReservedPrefix = "msgraph";
        public static Lazy<HashSet<string>> ReservedNames { get; private set; } = new Lazy<HashSet<string>>(() => new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
                    "abstract", "continue", "for", "new", "switch", "assert", "default", "if", "package", "synchronized", "boolean", "do", "goto", "private", "this", "break", "double", "implements", "protected", "throw", "byte", "else", "import", "public", "throws", "case", "enum", "instanceof", "return", "transient", "catch", "extends", "int", "short", "try", "char", "final", "interface", "static", "void", "class", "finally", "long", "strictfp", "volatile", "const", "float", "native", "super", "while", "true", "false", "null", "import"
                });

        public static string GetReservedPrefix(this OdcmType @type)
        {
            return ReservedPrefix;
        }

        public static string GetTypeString(this OdcmType @type)
        {
            // If isFlags = true, return an EnumSet instead of an enum. This will be 
            // serialized to and deserialized from a string
            if (String.Equals(@type.ToString(), "Vipr.Core.CodeModel.OdcmEnum") && @type.AsOdcmEnum().IsFlags)
            {
                return "EnumSet<" + @type.Name.ToUpperFirstChar() + ">";
            }

            switch (@type.Name)
            {
                case "Int16":
                case "Int32":
                    return "Integer";
                case "Int64":
                    return "Long";
                case "Guid":
                    return "java.util.UUID";
                case "DateTimeOffset":
                    return "java.util.Calendar";
                case "Date":
                    return "com.microsoft.graph.core.DateOnly";
                case "TimeOfDay":
                    return "com.microsoft.graph.core.TimeOfDay";
                case "Duration":
                    return "javax.xml.datatype.Duration";
                case "Json":
                    return "com.google.gson.JsonElement";
                case "Binary":
                    return "byte[]";
                case "Single":
                    return "Float";
                case "Decimal":
                    return "java.math.BigDecimal";
                default:
                    return @type.Name.ToUpperFirstChar();
            }
        }

        public static string GetTypeString(this OdcmParameter parameter)
        {
            return GetTypeString(parameter.Type);
        }

        public static string GetTypeString(this OdcmProperty property)
        {
            var propertyType = property.Projection.Type;
            var typeString = GetTypeString(propertyType);
            if (propertyType.Namespace != OdcmNamespace.Edm && ReservedNames.Value.Contains(typeString))
            {
                typeString = "com.microsoft.graph.models.extensions." + typeString;
            }
            return typeString;
        }

        public static bool IsComplex(this OdcmParameter property)
        {
            string t = property.GetTypeString();
            return !(t == "Integer" || t == "java.util.UUID" || t == "java.util.Calendar"
                  || t == "byte[]" || t == "String" || "long" == t || "Byte[]" == t
                  || t == "Short" || t == "com.microsoft.graph.model.DateOnly");
        }

        private static string[] additionalNullableTypes = new[] { "String", "java.util.Calendar", "Integer", "java.util.UUID", "Long", "Double" };
        public static bool IsNullable(this OdcmParameter property)
        {
            return property.IsComplex() || additionalNullableTypes.Contains(property.GetTypeString());
        }

        public static bool IsNullable(this OdcmProperty property)
        {
            return property.IsComplex() || additionalNullableTypes.Contains(property.GetTypeString());
        }

        public static string GetToLowerFirstCharName(this OdcmProperty property)
        {
            return property.Name.ToLowerFirstChar();
        }

        public static string SanitizePropertyName(this string property, OdcmObject odcmProperty = null)
        {
            if (ReservedNames.Value.Contains(property))
            {
                var result = ReservedPrefix + property.ToUpperFirstChar();
                logger.Info($"Property \"{property}\" is a reserved word in Java. Converting to \"{result}\"");
                return result;
            }
            else if (property == odcmProperty?.Name?.ToUpperFirstChar() && !(odcmProperty?.Name?.StartsWith("_") ?? false))
            {
                // Name the property by its type. Sanitize it in case the type is a reserved name.  
                return odcmProperty?.Projection?.Type?.Name?.ToUpperFirstChar()?.SanitizePropertyName(odcmProperty) ?? odcmProperty?.Name?.SanitizePropertyName();
            }
            else
                return property?.Replace("@", string.Empty)?.Replace(".", string.Empty);
        }

        public static string GetToLowerImport(this OdcmProperty property)
        {
            var type = property.Projection.Type;
            var index = type.Name.LastIndexOf('.');
            return type.Name.Substring(0, index).ToLower() + type.Name.Substring(index);
        }

        /// Choose an intermediate class type based on GETs/POSTs
        public static string GetMethodRequestBuilderSuperClass(this OdcmObject currentType)
        {
            return (currentType.AsOdcmMethod().IsFunction ?
                "BaseFunctionRequestBuilder" : "BaseActionRequestBuilder") +
                $"<{(currentType.AsOdcmMethod()?.ReturnType == null ? currentType.AsOdcmMethod().Class.TypeName() : currentType.ReturnType()) ?? "Void"}>";
        }

        public static string GetMethodCollectionRequestBuilderSuperClass(this OdcmObject currentType)
        {
            var returnTypeIsNull = currentType.AsOdcmMethod()?.ReturnType == null;
            var typeName = returnTypeIsNull ? currentType.AsOdcmMethod().Class.TypeName() : currentType.ReturnType();
            var requestBuilderName = currentType.TypeCollectionRequestBuilder();
            var collectionResponseName = currentType.TypeCollectionResponse();
            var collectionRequestName = currentType.TypeCollectionRequest();
            return (currentType.AsOdcmMethod().IsFunction ?
                "BaseFunctionCollectionRequestBuilder" : "BaseActionCollectionRequestBuilder") +
                $"<{typeName}, {requestBuilderName}, {collectionResponseName}, {currentType.TypeCollectionPage()}, {collectionRequestName}>";
        }

        private static Regex lastIdReplacer = new Regex(@"\/{id}$");

        /// <summary>
        /// Gets the canonical path of the entity for reference request
        /// </summary>
        /// <param name="c">Current navigation property class</param>
        /// <param name="m">Current model</param>
        /// <returns>The canonical version relative path of the entity for reference request</returns>
        public static string CanonicalPath(this OdcmObject c, OdcmModel m)
        {
            var propertyInformation = GetCanonicalNavigationProperties(c, m, true); //singleton type all have ContainsTarget=True to support navigation properties bindings, but they don't actually contain target
            var shortestCanonicalPath = propertyInformation
                                        .Select(x => GetNavigationPathFromProperty(x, m))
                                        .Select(x => new { path = x, slashCount = x.Count(y => y == '/') })
                                        .OrderBy(x => x.slashCount)
                                        .Select(x => x.path)
                                        .FirstOrDefault();
            return string.IsNullOrEmpty(shortestCanonicalPath) ? string.Empty : lastIdReplacer.Replace(shortestCanonicalPath, string.Empty);
        }
        public static bool ShouldIncludeReferenceUpdateMethods(this OdcmObject c, OdcmModel m)
        {
            return GetCanonicalNavigationProperties(c, m).Any();
        }
        /// <summary>
        /// Enumerates all navigation properties for a specific type in order to determine the canonical path for reference requests
        /// </summary>
        /// <param name="c">Type to find navigation properties for</param>
        /// <param name="m">Model to find navigation properties from</param>
        /// <param name="includeContainsTarget">Whether or not to include navigation properties that include target.</param>
        /// <param name="includeNonCollectionProperties">Whether or not to include navigation properties that return single items.</param>
        /// <returns>navigation properties for a specific type</returns>
        private static IEnumerable<OdcmProperty> GetCanonicalNavigationProperties(this OdcmObject c, OdcmModel m, bool includeContainsTarget = false, bool includeNonCollectionProperties = false)
        {
            Func<OdcmProperty, bool> propertiesFilter = x => x.IsNavigation() && x.Projection.Type == c && (x.IsCollection || includeNonCollectionProperties) && (x.IsReference() || includeContainsTarget);
            return m.EntityContainer
                    .Properties
                    .OfType<OdcmSingleton>()
                    .SelectMany(x => x.Projection
                                      .Type
                                      .AsOdcmClass()
                                      .Properties)
                    .Union(m.EntityContainer.Properties)
                    .Union(m.Namespaces
                            .SelectMany(x => x.Classes)
                            .SelectMany(x => x.Properties))
                    .Where(propertiesFilter);
        }
        private static string GetNavigationPathFromProperty(this OdcmProperty p, OdcmModel m, int currentDepthLevel = 0)
        {
            currentDepthLevel++;//some models can have circular references, this is a failsafe
            if (p == null || currentDepthLevel > 10) return string.Empty;
            if (p.Class == m.EntityContainer)
                if(p is OdcmSingleton)
                    return $"/{p.Name}";
                else
                    return $"/{p.Name}/{{id}}";
            else
                return GetNavigationPathFromProperty(GetCanonicalNavigationProperties(p.Class, m, true, true).FirstOrDefault(), m, currentDepthLevel) + $"/{p.Name}/{{id}}";
        }
        public static string TypeName(this OdcmObject c)
        {
            if (c is OdcmMethod)
            {
                return ((OdcmMethod)c).Class.Name.ToUpperFirstChar() + c.Name.Substring(c.Name.IndexOf(".") + 1).ToUpperFirstChar();
            }
            else if (c is OdcmProperty && c.AsOdcmProperty().IsCollection)
            {
                return c.AsOdcmProperty().Projection.Type.Name.ToUpperFirstChar();
            }
            else if (c is OdcmProperty && c.AsOdcmProperty().Projection.Type is OdcmPrimitiveType)
            {
                return c.ClassTypeName() + c.Name.SanitizePropertyName(c).ToUpperFirstChar() + c.AsOdcmProperty().Projection.Type.Name.ToUpperFirstChar();
            }
            else if (c is OdcmProperty)
            {
                return c.AsOdcmProperty().Projection.Type.Name.ToUpperFirstChar();
            }
            return c.Name.ToUpperFirstChar();
        }

        public static string MethodName(this OdcmObject c)
        {
            return c.Name.Substring(c.Name.IndexOf(".") + 1).SanitizePropertyName(c).ToUpperFirstChar();
        }

        public static string MethodFullName(this OdcmObject c)
        {
            if (c is OdcmMethod)
            {
                return string.Format("{0}.{1}", ((OdcmMethod)c).Class.Namespace.Name,
                                                c.Name.Substring(c.Name.IndexOf(".") + 1));
            }
            return c.Name.Substring(c.Name.IndexOf(".") + 1).ToUpperFirstChar();
        }

        public static string BaseTypeName(this OdcmObject c)
        {
            return "Base" + c.TypeName();
        }

        public static string TypeStreamRequest(this OdcmObject c)
        {
            if (c.TypeName().EndsWith("stream", StringComparison.InvariantCultureIgnoreCase))
            {
                return c.TypeName() + "Request";
            }
            return c.TypeName() + "StreamRequest";
        }

        public static string BaseTypeStreamRequest(this OdcmObject c)
        {
            return "Base" + c.TypeStreamRequest();
        }

        public static string TypeStreamRequestBuilder(this OdcmObject c)
        {
            return c.TypeStreamRequest() + "Builder";
        }

        public static string BaseTypeStreamRequestBuilder(this OdcmObject c)
        {
            return "Base" + c.TypeStreamRequestBuilder();
        }

        public static string TypeRequest(this OdcmObject c)
        {
            if (c is OdcmProperty && c.AsOdcmProperty().IsReference())
            {
                return c.TypeWithReferencesRequest();
            }
            else
            {
                return c.TypeName() + "Request";
            }
        }

        public static string BaseTypeRequest(this OdcmObject c)
        {
            return "Base" + c.TypeRequest();
        }

        public static string TypeRequestBuilder(this OdcmObject c)
        {
            return c.TypeRequest() + "Builder";
        }

        public static string BaseTypeRequestBuilder(this OdcmObject c)
        {
            return "Base" + c.TypeRequestBuilder();
        }

        public static string TypeWithReferencesRequest(this OdcmObject c)
        {
            return c.TypeName() + "WithReferenceRequest";
        }

        public static string BaseTypeWithReferencesRequest(this OdcmObject c)
        {
            return "Base" + c.TypeWithReferencesRequest();
        }

        public static string TypeWithReferencesRequestBuilder(this OdcmObject c)
        {
            return c.TypeWithReferencesRequest() + "Builder";
        }

        public static string BaseTypeWithReferencesRequestBuilder(this OdcmObject c) => $"Base{c.TypeWithReferencesRequestBuilder()}";

        public static string TypeReferenceRequest(this OdcmObject c)
        {
            return c.TypeName() + "ReferenceRequest";
        }

        public static string BaseTypeReferenceRequest(this OdcmObject c)
        {
            return "Base" + c.TypeReferenceRequest();
        }

        public static string TypeReferenceRequestBuilder(this OdcmObject c)
        {
            return c.TypeReferenceRequest() + "Builder";
        }

        public static string BaseTypeReferenceRequestBuilder(this OdcmObject c)
        {
            return "Base" + c.TypeReferenceRequestBuilder();
        }

        public static string TypeCollectionPage(this OdcmObject c)
        {
            return c.TypeName() + "CollectionPage";
        }

        public static string TypeCollectionWithReferencesPage(this OdcmObject c)
        {
            return $"{c.TypeName()}CollectionWithReferencesPage";
        }

        public static string BaseTypeCollectionPage(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionPage();
        }

        public static string BaseTypeCollectionWithReferencesPage(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionWithReferencesPage();
        }

        public static string BaseTypeCollectionResponse(this OdcmObject c)
        {
            return c.TypeCollectionResponse();
        }

        public static string TypeCollectionResponse(this OdcmObject c)
        {
            return c.TypeName() + "CollectionResponse";
        }

        public static string TypeCollectionRequest(this OdcmObject c)
        {
            if (c is OdcmProperty && c.AsOdcmProperty().IsReference())
            {
                return c.TypeCollectionWithReferencesRequest();
            }
            else
            {
                return c.TypeName() + "CollectionRequest";
            }
        }

        public static string BaseTypeCollectionRequest(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionRequest();
        }

        public static string TypeCollectionRequestBuilder(this OdcmObject c)
        {
            return c.TypeCollectionRequest() + "Builder";
        }

        public static string BaseTypeCollectionRequestBuilder(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionRequestBuilder();
        }

        public static string TypeCollectionWithReferencesRequest(this OdcmObject c)
        {
            return c.TypeName() + "CollectionWithReferencesRequest";
        }

        public static string BaseTypeCollectionWithReferencesRequest(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionWithReferencesRequest();
        }

        public static string TypeCollectionWithReferencesRequestBuilder(this OdcmObject c)
        {
            return c.TypeCollectionWithReferencesRequest() + "Builder";
        }

        public static string BaseTypeCollectionWithReferencesRequestBuilder(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionWithReferencesRequestBuilder();
        }

        public static string TypeCollectionReferenceRequest(this OdcmObject c)
        {
            return c.TypeName() + "CollectionReferenceRequest";
        }

        public static string BaseTypeCollectionReferenceRequest(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionReferenceRequest();
        }

        public static string TypeCollectionReferenceRequestBuilder(this OdcmObject c)
        {
            return c.TypeCollectionReferenceRequest() + "Builder";
        }

        public static string BaseTypeCollectionReferenceRequestBuilder(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionReferenceRequestBuilder();
        }

        public static string TypeParameterSet(this OdcmObject c)
        {
            return c.TypeName() + "ParameterSet";
        }

        public static string TypeParameterSetBuilder(this OdcmObject c)
        {
            return c.TypeParameterSet() + "Builder";
        }

        /// Returns the name of the service <parameter> with the first char lowercase
        public static string ParamName(this OdcmParameter c)
        {
            return c.Name.ToLowerFirstChar();
        }

        /// The Type of this param, as a string
        public static string ParamType(this OdcmParameter c)
        {
            var typeString = c.Type.GetTypeString();
            if (c.IsCollection)
            {
                typeString = String.Format("java.util.List<{0}>", c.Type.GetTypeString());
            }
            else if (typeString.Equals("Stream"))
            {

                // Excel introduced the use of "Edm.Stream" types
                // normally this would be addressed at the TypeHelper level
                // but because streams will use different data containers
                // for outgoing vs. incoming streams I'm going to apply the type
                // here. Outbound Streams will use byte[]

                typeString = "byte[]";
            }
            return typeString;
        }

        /// The Type of this param, as a string
        public static string ParamType(this OdcmObject c)
        {
            if (c is OdcmParameter p)
            {
                return p.ParamType();
            }

            return string.Empty;
        }
        public static string ReturnType(this OdcmObject c)
        {
            var returnType = c.AsOdcmMethod()?.ReturnType;
            if (returnType != null)
            {
                var returnTypeString = returnType.GetTypeString();

                // Excel introduced the use of "Edm.Stream" types
                // normally this would be addressed at the TypeHelper level
                // but because streams will use different data containers
                // for outgoing vs. incoming streams I'm going to apply the type
                // here. Inbound Streams will use java.io.InputStream

                if (returnTypeString.Equals("Stream"))
                {
                    returnTypeString = "java.io.InputStream";
                }
                return returnTypeString;
            }
            return "Void";
        }

        /// <summary>
        /// Takes a property object and gets to its type, then returns that types fully qualified namespace: e.g. com.microsoft.graph.callrecords
        /// </summary>
        /// <param name="p">property object</param>
        /// <returns>fully qualified namespace of a property</returns>
        public static string GetPropertyNamespace(this OdcmProperty p) => p.Projection.Type.Namespace.Name.AddPrefix();
        public static bool MethodHasParameters(this OdcmMethod m) => m?.Parameters?.Any() ?? false;
        public static string MethodParametersValues(this OdcmMethod method)
        {
            var parameterValuesBuilder = new StringBuilder();
            foreach (var p in method.Parameters)
            {
                parameterValuesBuilder.AppendFormat(", {0}", p.ParamName());
            }
            return parameterValuesBuilder.ToString();
        }

        public static string MethodFieldValues(this OdcmObject c)
        {
            var parameterValuesBuilder = new StringBuilder();
            foreach (var p in c.AsOdcmMethod().Parameters)
            {
                parameterValuesBuilder.AppendFormat(", {0}", p.ParamName());
            }
            return parameterValuesBuilder.ToString();
        }

        public static string MethodPageValues(this OdcmObject c)
        {
            var pageValuesBuilder = new StringBuilder();
            foreach (var param in c.AsOdcmMethod().Parameters)
            {
                pageValuesBuilder.AppendFormat(", ({0}) null", ParamType(param));
            }
            return pageValuesBuilder.ToString();
        }

        public static string ClassTypeName(this OdcmObject c)
        {
            if (c is OdcmMethod)
            {
                return c.AsOdcmMethod().Class.Name.ToUpperFirstChar();
            }
            else if (c is OdcmProperty && c.AsOdcmProperty().Class is OdcmServiceClass)
            {
                return c.AsOdcmProperty().Projection.Type.Name.ToUpperFirstChar();
            }
            else if (c is OdcmProperty)
            {
                return c.AsOdcmProperty().Class.Name.ToUpperFirstChar();
            }
            return c.Name.ToUpperFirstChar();
        }

        public static string ClientType(this OdcmObject c, CustomT4Host host)
        {
            return host.CurrentModel.EntityContainer.Name.ToUpperFirstChar() + "Client";
        }

        public static string IClientType(this OdcmObject c, CustomT4Host host)
        {
            return "I" + c.ClientType(host);
        }

        public static string BaseClientType(this OdcmObject c, CustomT4Host host)
        {
            return "Base" + c.ClientType(host);
        }

        public static string IBaseClientType(this OdcmObject c, CustomT4Host host)
        {
            return "I" + c.BaseClientType(host);
        }

        public static string IBaseClientType()
        {
            return "IBaseClient";
        }

        public static string BaseClassName(this OdcmObject o)
        {
            return o.BaseClass()?.TypeName() ?? string.Empty;
        }

        public static OdcmClass BaseClass(this OdcmObject o)
        {
            return o.AsOdcmClass()?.Base;
        }

        public static string OdcmMethodReturnType(this OdcmMethod method)
        {
            return method.ReturnType is OdcmPrimitiveType
                ? method.ReturnType.GetTypeString() : method.ReturnType.Name.ToCheckedCase();
        }
        public static string TargetCollectionType(this OdcmObject c)
        {
            return c is OdcmMethod ? OdcmMethodReturnType(c as OdcmMethod) : TypeName(c);
        }

        //Creating package definition for Enum.java.tt template file
        public static string CreatePackageDefForEnum(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            var packageFormat = @"package {0}.{1};";
            sb.AppendFormat(packageFormat,
                            host.CurrentNamespace(),
                            host.TemplateInfo.OutputParentDirectory.Replace("_", "."));
            sb.Append("\n");
            return sb.ToString();
        }
        public static string GetPackagePrefix(this OdcmObject obj)
        {
            switch (obj)
            {
                case OdcmEnum e:
                    return "models.generated";
                case OdcmType t:
                    return GetPrefixForModels();
                case OdcmParameter p:
                    return GetPackagePrefix(p.Type);
                case OdcmProperty p:
                    return GetPackagePrefix(p.Type);
                default:
                    throw new ArgumentException(nameof(obj));
            }
        }
        public static string CreatePackageDefinition(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            var packageFormat = @"package {0}.{1};";
            sb.AppendFormat(packageFormat,
                            host.CurrentNamespace(),
                            host.TemplateInfo.OutputParentDirectory.Replace("_", "."));
            sb.Append("\n");
            return sb.ToString();
        }

        public static string CreatePackageDefForBaseCollectionResponse(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            sb.Append(host.CreatePackageDefinition());
            var importFormat = @"import {0}.{1}.{2};";
            if (!(host.CurrentType is OdcmMethod) || !((host.CurrentType as OdcmMethod).ReturnType is OdcmPrimitiveType))
            {
                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            (host.CurrentType as OdcmMethod)?.ReturnType is OdcmEnum ? "models.generated" : GetPrefixForModels(),
                            TypeName((host.CurrentType as OdcmMethod)?.ReturnType ?? host.CurrentType));
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public static string CreatePackageDefForBaseMethodRequest(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            sb.Append(host.CreatePackageDefinition());
            var returnType = host.CurrentType.ReturnType();
            var importFormat = @"import {0}.{1}.{2};";
            var currentMethod = host.CurrentType.AsOdcmMethod();
            if (returnType != "Void" && !(currentMethod.ReturnType is OdcmPrimitiveType))
            {
                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            (currentMethod.ReturnType is OdcmEnum ? "models.generated" : GetPrefixForModels()),
                            returnType);
                sb.Append("\n");
            }
            if(currentMethod.ReturnType != currentMethod.Class)
            {
                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            GetPrefixForModels(),
                            currentMethod.Class.TypeName());
                sb.Append("\n");
            }

            sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            GetPrefixForRequests(),
                            host.CurrentType.TypeRequest());
            sb.Append("\n");
            return sb.ToString();
        }

        public static string CreatePackageDefForBaseMethodRequestBuilder(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            sb.Append(host.CreatePackageDefinition());
            const string importFormat = @"import {0}.{1}.{2};";
            sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            GetPrefixForRequests(),
                            host.CurrentType.TypeRequest());
            sb.Append("\n");
            var currentMethod = host.CurrentType.AsOdcmMethod();
            if (currentMethod.ReturnType != currentMethod.Class)
            {
                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            GetPrefixForModels(),
                            currentMethod.Class.TypeName());
                sb.Append("\n");
            }

            var imports = host.CurrentType.AsOdcmMethod().WithOverloads().SelectMany(x => ImportClassesOfMethodParameters(x));
            sb.Append(imports.Any() ? imports.Aggregate((x, y) => $"{x}{Environment.NewLine}{y}") : string.Empty);
            return sb.ToString();
        }

        public static string ImportClassesOfMethodParametersAsString(OdcmMethod method, bool withOverloads = false, string importFormat = "import {0}.{1}.{2};", string importTypeToExclude = null)
        {
            var imports = withOverloads ? 
                            method.WithOverloads().SelectMany(x => ImportClassesOfMethodParameters(x, importFormat, importTypeToExclude)).Distinct():
                            ImportClassesOfMethodParameters(method, importFormat, importTypeToExclude);
            return imports.Any() ? imports.Aggregate((x, y) => $"{x}{Environment.NewLine}{y}") : string.Empty;
        }

        /// <summary>
        /// Appends import statements of types that appear as a method's parameters
        /// </summary>
        /// <param name="method">Method whose parameter types will be consumed</param>
        /// <param name="importFormat">import format, e.g. "import {0}.{1}.{2}"</param>
        /// <returns>Hashset with import statements to insert</returns>
        public static HashSet<string> ImportClassesOfMethodParameters(OdcmMethod method, string importFormat = "import {0}.{1}.{2};", string importTypeToExclude = null)
        {
            var importStatements = new HashSet<string>();
            var appendEnumSet = false;
            foreach (var p in method.Parameters)
            {
                if (!(p.Type is OdcmPrimitiveType) && p.Type.GetTypeString() != "com.google.gson.JsonElement")
                {
                    var propertyType = p.Type.GetTypeString();
                    if (propertyType.StartsWith("EnumSet"))
                    {
                        propertyType = propertyType.Substring("EnumSet<".Length, propertyType.Length - ("EnumSet<".Length + 1));
                        appendEnumSet = true;
                    }
                    if (string.IsNullOrEmpty(importTypeToExclude) || propertyType != importTypeToExclude)
                        importStatements.Add(string.Format(importFormat, p.Type.Namespace.Name.AddPrefix(), p.GetPackagePrefix(), propertyType));
                }
            }
            if (!(method?.ReturnType == null || method.ReturnType is OdcmPrimitiveType))
                importStatements.Add(string.Format(importFormat, method.ReturnType.Namespace.Name.AddPrefix(), method.ReturnType.GetPackagePrefix(), method.OdcmMethodReturnType()));

            if (appendEnumSet)
            {
                importStatements.Add("import java.util.EnumSet;");
            }

            return importStatements;
        }
        public static string CreatePackageDefForBaseMethodBodyRequest(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            sb.Append(host.CreatePackageDefinition());
            var importFormat = @"import {0}.{1}.{2};";

            sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            GetPrefixForModels(),
                            host.CurrentType.TypeParameterSet());
            sb.Append("\n");
            var returnType = host.CurrentType.ReturnType();
            if (returnType != "Void" && !(host.CurrentType.AsOdcmMethod().ReturnType is OdcmPrimitiveType))
            {
                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            GetPrefixForModels(),
                            returnType);
                sb.Append("\n");
            }

            if (host.CurrentType.AsOdcmMethod().IsCollection)
            {
                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            GetPrefixForRequests(),
                            host.CurrentType.TypeCollectionRequest());
                sb.Append("\n");

                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            GetPrefixForRequests(),
                            host.CurrentType.TypeCollectionRequest());
                sb.Append("\n");
            }
            else
            {
                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            GetPrefixForRequests(),
                            host.CurrentType.TypeRequest());
                sb.Append("\n");
            }
            return sb.ToString();
        }
        public static string CreatePackageDefForBaseEntityCollectionPage(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            sb.Append(host.CreatePackageDefinition());

            var importFormat = @"import {0}.{1}.{2};";
            string modelClassName;
            if (host.CurrentType is OdcmMethod)
                modelClassName = OdcmMethodReturnType(host.CurrentType as OdcmMethod);
            else
                modelClassName = TypeName(host.CurrentType);


            sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            GetPrefixForModels(),
                            modelClassName);
            sb.Append("\n");
            sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            GetPrefixForRequests(),
                            host.CurrentType.TypeCollectionRequestBuilder());

            return sb.ToString();
        }

        //Fixing package and import statement for model classes
        public static string CreatePackageDefForEntity(this CustomT4Host host)
        {
            var type = host.CurrentType as OdcmClass;
            IEnumerable<OdcmProperty> properties = type.Properties;
            var sb = new StringBuilder();
            var packageFormat = @"package {0}.{1};";
            sb.AppendFormat(packageFormat,
                            host.CurrentNamespace(),
                            host.TemplateInfo.OutputParentDirectory.Replace("_", "."));
            sb.Append("\n");

            sb.AppendFormat(@"import {0}.serializer.ISerializer;
import {0}.serializer.IJsonBackedObject;
import {0}.serializer.AdditionalDataManager;
import java.util.EnumSet;", host.CurrentModel.GetNamespace().AddPrefix());

            sb.Append("\n");
            var importFormat = @"import {0}.{1}.{2};";
            Dictionary<string, int> uniqueStore = new Dictionary<string, int>();
            if (properties.Any(x => x.IsCollection))
            {
                sb.AppendFormat(importFormat, host.CurrentModel.GetNamespace().AddPrefix(), "http", "BaseCollectionPage");
                sb.Append("\n");
            }

            foreach (var property in properties.Where(p => !p.Projection.Type.Name.Equals("Stream") && p.ParentPropertyType == null))
            {
                var propertyType = property.GetTypeString();
                if (property.Type is OdcmPrimitiveType)
                    continue;

                if (propertyType == "com.google.gson.JsonElement" || propertyType == "com.google.gson.JsonElement" || propertyType.StartsWith("com.microsoft.graph.models"))
                    continue;

                if (propertyType.StartsWith("EnumSet"))
                    propertyType = propertyType.Substring("EnumSet<".Length, propertyType.Length - ("EnumSet<".Length + 1));

                string prefixValue = property.GetPackagePrefix();
                string importstr = String.Format(importFormat,
                            property.Projection.Type.Namespace.Name.AddPrefix(),
                            prefixValue,
                            propertyType);
                if (!uniqueStore.ContainsKey(importstr))
                {
                    uniqueStore.Add(importstr, 0);
                    sb.Append(importstr);
                    sb.Append("\n");
                }

            }

            string baseClassNameType = host.CurrentType.BaseClassName();
            if (baseClassNameType != "")
            {
                string prefixValue = GetPrefixForModels();
                string importstr = String.Format(importFormat,
                            (host.CurrentType.BaseClass() as OdcmClass).Namespace.Name.AddPrefix(),
                            prefixValue,
                            baseClassNameType);
                if (!uniqueStore.ContainsKey(importstr))
                {
                    uniqueStore.Add(importstr, 0);
                    sb.Append(importstr);
                    sb.Append("\n");
                }
            }

            string baseTypeNameStr = BaseTypeName(host.CurrentType);
            if (baseTypeNameStr == "BasePlannerAssignments")
            {
                string importstr = String.Format(importFormat,
                                host.CurrentModel.GetNamespace().AddPrefix(),
                            GetPrefixForModels(),
                             "PlannerAssignment");
                if (!uniqueStore.ContainsKey(importstr))
                {
                    uniqueStore.Add(importstr, 0);
                    sb.Append(importstr);
                    sb.Append("\n");
                }
            }
            if (baseTypeNameStr == "BasePlannerChecklistItems")
            {
                string importstr = String.Format(importFormat,
                            host.CurrentModel.GetNamespace().AddPrefix(),
                            GetPrefixForModels(),
                             "PlannerChecklistItem");
                if (!uniqueStore.ContainsKey(importstr))
                {
                    uniqueStore.Add(importstr, 0);
                    sb.Append(importstr);
                    sb.Append("\n");
                }
            }

            if (properties != null)
            {
                foreach (var property in properties.Where(p => p.IsCollection() && p.IsNavigation() && p.ParentPropertyType == null))
                {
                    if (property.Type is OdcmPrimitiveType)
                        continue;

                    string propertyValue = TypeCollectionPage(property);
                    string importstr1 = String.Format(importFormat,
                        property.Projection.Type.Namespace.Name.AddPrefix(),
                        GetPrefixForRequests(),
                        propertyValue);
                    if (!uniqueStore.ContainsKey(importstr1))
                    {
                        uniqueStore.Add(importstr1, 0);
                        sb.Append(importstr1);
                        sb.Append("\n");
                    }
                }
            }
            return sb.ToString();
        }

        public static string CreatePackageDef(this CustomT4Host host)
        {
            // {0}: type's namespace with prefix, e.g. com.microsoft.graph.callrecords
            // {1}: relative path, e.g. "models.extensions" or "models.requests" etc.
            // {2}: main namespace, i.e. microsoft.graph
            // {3}: fully qualified import for disambiguation, e.g. TimeOff vs TimeOffRequest
            // {4}: method parameters fully qualified imports
            var format = @"package {0}.{1};

import {2}.http.IRequestBuilder;
import {2}.core.ClientException;
{3}{4}
import java.util.Arrays;
import java.util.EnumSet;
import javax.annotation.Nullable;
import javax.annotation.Nonnull;";

            // We need this for disambiguation of generated model class/interfaces references.
            string fullyQualifiedImport = host.GetFullyQualifiedImportStatementForModel();

            // determine current namespace and generate method imports if applicable
            string @namespace;
            var methodImports = new HashSet<string>();
            const string importFormat = "import {0}.{1}.{2};";
            const string graphServiceEntityName = "GraphService";
            const string interfaceTemplatePrefix = "I";
            switch (host.CurrentType)
            {
                case OdcmProperty p:
                    @namespace = p.Type.Namespace.Name.AddPrefix();
                    if (p.Class.GetTypeString() != graphServiceEntityName)
                        methodImports.Add(string.Format(importFormat, p.Class.Namespace.Name.AddPrefix(), p.Class.GetPackagePrefix(), p.Class.GetTypeString()));
                    if (!(p.Projection.Type is OdcmPrimitiveType))
                        methodImports.Add(string.Format(importFormat, p.Projection.Type.Namespace.Name.AddPrefix(), p.Projection.Type.GetPackagePrefix(), p.Projection.Type.GetTypeString()));
                    p.Projection?.Type?.AsOdcmClass()?.MethodsAndOverloads()
                        ?.Distinct()
                        ?.SelectMany(o => ImportClassesOfMethodParameters(o))
                        ?.ToList()
                        ?.ForEach(x => methodImports.Add(x));
                    break;
                case OdcmMethod m:
                    m.WithDistinctOverloads()
                        .SelectMany(o => ImportClassesOfMethodParameters(o))
                        ?.ToList()
                        ?.ForEach(x => methodImports.Add(x));
                    goto default;
                case OdcmClass c:
                    if (c.GetTypeString() != graphServiceEntityName)
                        methodImports.Add(string.Format(importFormat, c.Namespace.Name.AddPrefix(), c.GetPackagePrefix(), c.GetTypeString()));

                    var importTypeToExclude = host.TemplateFile.EndsWith("BaseEntityRequest.java.tt") ? host.TemplateName : string.Empty;
                    c?.MethodsAndOverloads()
                        ?.Distinct()
                        ?.SelectMany(o => ImportClassesOfMethodParameters(o, importTypeToExclude: importTypeToExclude))
                        ?.ToList()
                        ?.ForEach(x => methodImports.Add(x));
                    c?.NavigationProperties()
                        ?.Where(x => x.IsCollection && x.ParentPropertyType == null)?
                        .Select(x => x.Projection.Type)
                        ?.Distinct()
                        ?.ToList()
                        ?.ForEach(x => ImportRequestBuilderTypes(host, x, methodImports, importFormat, interfaceTemplatePrefix, true));
                    c?.NavigationProperties()
                        ?.Where(x => !x.IsCollection && x.ParentPropertyType == null)
                        ?.Select(x => x.Projection.Type)
                        ?.Distinct()
                        ?.ToList()
                        ?.ForEach(x => ImportRequestBuilderTypes(host, x, methodImports, importFormat, interfaceTemplatePrefix, false));
                    goto default;
                default:
                    @namespace = host.CurrentNamespace();
                    break;
            }

            return string.Format(format,
                @namespace,
                host.TemplateInfo.OutputParentDirectory.Replace("_", "."),
                host.CurrentModel.GetNamespace().AddPrefix(),
                fullyQualifiedImport,
                methodImports.Any() ? methodImports.Aggregate((x, y) => $"{x}{Environment.NewLine}{y}") : string.Empty);
        }
        private static void ImportRequestBuilderTypes(CustomT4Host host, OdcmType x, HashSet<string> methodImports, string importFormat, string interfaceTemplatePrefix, bool includeCollectionTypes)
        {
            if (includeCollectionTypes)
                methodImports.Add(string.Format(importFormat, x.Namespace.Name.AddPrefix(), GetPrefixForRequests(), x.TypeCollectionRequestBuilder()));
            methodImports.Add(string.Format(importFormat, x.Namespace.Name.AddPrefix(), GetPrefixForRequests(), x.TypeRequestBuilder()));
            if (!host.TemplateInfo.TemplateName.StartsWith(interfaceTemplatePrefix))
            {
                if (includeCollectionTypes)
                    methodImports.Add(string.Format(importFormat, x.Namespace.Name.AddPrefix(), GetPrefixForRequests(), x.TypeCollectionRequestBuilder()));
                methodImports.Add(string.Format(importFormat, x.Namespace.Name.AddPrefix(), GetPrefixForRequests(), x.TypeRequestBuilder()));
            }
        }
        /// <summary>
        /// Determines which namespace current type belongs to
        /// </summary>
        /// <param name="host">Host object to reach current type being processed</param>
        /// <returns>namespace with prefix prepended, e.g. com.microsoft.graph.callrecords</returns>
        public static string CurrentNamespace(this CustomT4Host host)
        {
            switch (host.CurrentType)
            {
                case OdcmType t:
                    return t.Namespace.Name.AddPrefix();
                case OdcmProperty p:
                    return p.Projection.Type.Namespace.Name.AddPrefix();
                default:
                    return string.Empty;
            }
        }

        //Getting import prefix using property name for model classes
        public static string GetPrefixForModels() => "models.extensions";

        //Getting import prefix using property name for Request classes
        public static string GetPrefixForRequests() => "requests.extensions";

        public static string CreateParameterDef(IEnumerable<OdcmParameter> parameters)
        {
            var sb = new StringBuilder();

            var format =
    @"    /**
     * The {0}.
     * {1}{2}
     */
    @SerializedName(value = ""{3}"", alternate = {{""{4}""}})
    @Expose{5}{6}
    public {7} {8};

";
            foreach (var p in parameters)
            {
                sb.AppendFormat(
                    format,
                    p.ParamName().SplitCamelCase(),
                    ReplaceInvalidCharacters(p.LongDescription),
                    (p.IsDeprecated ? $"\r\n     * @deprecated {p.Deprecation?.Description}" : string.Empty),
                    p.ParamName(),
                    p.ParamName().ToUpperFirstChar(),
                    (p.IsNullable() ? "\r\n\t@Nullable" : string.Empty),
                    (p.IsDeprecated ? "\r\n@Deprecated" : string.Empty),
                    p.ParamType(),
                    p.ParamName().SanitizePropertyName(p).ToLowerFirstChar()
                );
            }
            return sb.ToString();
        }

        public static string CreateRawJsonObject(bool overrideGetSerializerMethod = true)
        {
            return
    @"    /**
     * The raw representation of this class
     */
    private JsonObject rawObject;

    /**
     * The serializer
     */
    private ISerializer serializer;

    /**
     * Gets the raw representation of this class
     *
     * @return the raw representation of this class
     */
    @Nullable
    public JsonObject getRawObject() {
        return rawObject;
    }

    /**
     * Gets serializer
     *
     * @return the serializer
     */"+ (overrideGetSerializerMethod ? "\n\t@Override" : "") +@"
    @Nullable
    public ISerializer getSerializer() {
        return serializer;
    }

    /**
     * Sets the raw JSON object
     *
     * @param serializer the serializer
     * @param json the JSON object to set this object to
     */
    public void setRawObject(@Nonnull final ISerializer serializer, @Nonnull final JsonObject json) {
        this.serializer = serializer;
        rawObject = json;
";
        }

        public static string UpdatePropertiesWithinSetRawObject(IEnumerable<OdcmProperty> properties = null, bool isComplexType = false)
        {
            var sb = new StringBuilder();
            if (!isComplexType && properties != null)
            {
                foreach (var property in properties.Where(p => p.IsCollection() && p.IsNavigation() && p.ParentPropertyType == null))
                {
                    sb.AppendFormat(
    @"
        if (json.has(""{0}"")) {{
            {0} = serializer.deserializeObject(json.get(""{0}"").toString(), {1}.class);
        }}
",
                property.Name.SanitizePropertyName(property),
                TypeCollectionPage(property));
                }
            }
            sb.Append("    }");
            return sb.ToString();
        }
        public static string CreateExtensiblityMessage()
        {
            return
    @"// This file is available for extending, afterwards please submit a pull request.";
        }

        public static string CreatAutogeneratedWarning()
        {
            return
    @"// **NOTE** This file was generated by a tool and any changes will be overwritten.";
        }

        /**
        * Replaces invalid Javadoc characters with HTML
        */
        private static string ReplaceInvalidCharacters(string inputString)
        {
            if (inputString != null)
            {
                return inputString.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;").Replace("\\", "&#92;");
            }
            return null;
        }

        /**
        * Get the description from the LongDescription or Description annotation and then return the sanitized string.
        */
        public static string GetSanitizedDescription(OdcmObject property)
        {
            var description = property.LongDescription ?? property.Description;

            return ReplaceInvalidCharacters(description);
        }

        public static string CreatePropertyDef(IEnumerable<OdcmProperty> properties, bool isComplexType = false)
        {
            var sb = new StringBuilder();

            var format =
    @"    /**
     * The {0}.
     * {1}{2}
     */{3}
    @SerializedName(value = ""{4}"", alternate = {{""{5}""}})
    @Expose{6}
    public {7} {8};

";
            var collectionFormat =
    @"    /**
     * The {0}.
     * {1}{2}
     */{3}{6}
    public {7} {8};

";

            foreach (var property in properties.Where(p => !p.Projection.Type.Name.Equals("Stream") && p.ParentPropertyType == null))
            {
                var propertyName = property.Name.ToUpperFirstChar();
                var propertyType = "";
                var propertyFormat = format;
                if (property.IsCollection)
                {
                    if (property.IsNavigation())
                    {
                        propertyType = TypeCollectionPage(property);
                        if (!property.ContainsTarget)
                            propertyFormat = collectionFormat;
                    }
                    else
                    {
                        propertyType = "java.util.List<" + property.GetTypeString() + ">";
                    }
                }
                else
                {
                    propertyType = property.GetTypeString();
                }

                sb.AppendFormat(propertyFormat,
                    propertyName.SplitCamelCase(),
                    GetSanitizedDescription(property),
                    property.IsDeprecated ? $"\r\n     * @deprecated {property.Deprecation?.Description}" : string.Empty,
                    property.IsDeprecated ? "\r\n    @Deprecated" : string.Empty,
                    property.Name,
                    propertyName,
                    (property.IsNullable() ? "\r\n\t@Nullable" : string.Empty),
                    propertyType,
                    property.Name.ToLowerFirstChar().SanitizePropertyName(property));
            }
            return sb.ToString();
        }

        /// Creates a class declaration
        /// name = the name of the class
        /// extends = the class it extends
        /// implements = the interface it extends
        public static string CreateClassDef(string name, string extends, string implements, string deprecationDescription)
        {
            return CreateClassOrInterface(name, true, extends, implements, deprecationDescription);
        }

        public static string CreateInterfaceDef(string name, string extends, string deprecationDescription)
        {
            return CreateClassOrInterface(name, false, extends, null, deprecationDescription);
        }

        public static string CreateClassOrInterface(string name, bool isClass = true, string extends = null, string implements = null, string deprecationDescription = null)
        {
            var extendsStr = string.Empty;
            if (!string.IsNullOrEmpty(extends))
            {
                extendsStr = string.Format(" extends {0}", extends);
            }

            var implementsStr = string.Empty;
            if (!string.IsNullOrEmpty(implements))
            {
                implementsStr = string.Format(" implements {0}", implements);
            }

            var format = @"

/**
 * The {1} for the {0}.{2}
 */{3}
public {1} {4}{5}{6} {{";
            string declaration = string.Format(format,
                isClass ? name.SplitCamelCase() : name.SplitCamelCase().Remove(0, 1),
                isClass ? "class" : "interface",
                string.IsNullOrEmpty(deprecationDescription) ? string.Empty : $"\r\n * @deprecated {deprecationDescription}",
                string.IsNullOrEmpty(deprecationDescription) ? string.Empty : "\r\n@Deprecated",
                name,
                extendsStr,
                implementsStr);

            return CreatAutogeneratedWarning() + declaration;
        }
    }
}
