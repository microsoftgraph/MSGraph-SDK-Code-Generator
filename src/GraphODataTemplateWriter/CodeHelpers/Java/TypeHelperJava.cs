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

    public static class TypeHelperJava
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public const string ReservedPrefix = "msgraph";
        public static HashSet<string> ReservedNames
        {
            get
            {
                return new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
                    "abstract", "continue", "for", "new", "switch", "assert", "default", "if", "package", "synchronized", "boolean", "do", "goto", "private", "this", "break", "double", "implements", "protected", "throw", "byte", "else", "import", "public", "throws", "case", "enum", "instanceof", "return", "transient", "catch", "extends", "int", "short", "try", "char", "final", "interface", "static", "void", "class", "finally", "long", "strictfp", "volatile", "const", "float", "native", "super", "while", "true", "false", "null"
                };
            }
        }

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
                    return "com.microsoft.graph.models.extensions.DateOnly";
                case "TimeOfDay":
                    return "com.microsoft.graph.models.extensions.TimeOfDay";
                case "Duration":
                    return "javax.xml.datatype.Duration";
                case "Json":
                    return "com.google.gson.JsonElement";
                case "Binary":
                    return "byte[]";
                case "Single":
                    return "float";
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
            if (propertyType.Namespace != OdcmNamespace.Edm && ReservedNames.Contains(typeString))
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

        public static string GetToLowerFirstCharName(this OdcmProperty property)
        {
            return property.Name.ToLowerFirstChar();
        }

        public static string SanitizePropertyName(this string property, OdcmObject odcmProperty = null)
        {
            if (ReservedNames.Contains(property))
            {
                logger.Info("Property \"{0}\" is a reserved word in Java. Converting to \"{1}{0}\"", property, ReservedPrefix);
                return ReservedPrefix + property;
            }

            if (odcmProperty != null && property == odcmProperty.Name.ToUpperFirstChar())
            {
                // Check whether the property type is the same as the class name.
                if (odcmProperty.Projection.Type.Name.ToUpperFirstChar() == odcmProperty.Name.ToUpperFirstChar())
                {
                    // Name the property: {metadataName} + "Property"
                    logger.Info("Property type \"{0}\" has the same name as the class. Converting to \"{0}Property\"", property);
                    return string.Concat(property, "Property");
                }

                // Name the property by its type. Sanitize it in case the type is a reserved name.  
                return odcmProperty.Projection.Type.Name.ToUpperFirstChar().SanitizePropertyName(odcmProperty);
            }

            return property.Replace("@", string.Empty).Replace(".", string.Empty);
        }

        public static string GetToLowerImport(this OdcmProperty property)
        {
            var type = property.Projection.Type;
            var index = type.Name.LastIndexOf('.');
            return type.Name.Substring(0, index).ToLower() + type.Name.Substring(index);
        }

        /// Choose an intermediate class type based on GETs/POSTs
        public static string RequestBuilderSuperClass(this OdcmObject currentType)
        {
            return currentType.AsOdcmMethod().IsFunction ?
                "BaseFunctionRequestBuilder" : "BaseActionRequestBuilder";
        }

        /// <summary>
        /// Get the name of the current template being processed
        /// </summary>
        /// <param name="templateFile">The full path of the current template</param>
        /// <returns>The template name, relative to the Templates directory.</returns>
        public static string TemplateName(this string templateFile)
        {
            return templateFile.Substring(templateFile.LastIndexOf("Templates"));
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
                return c.ClassTypeName() + c.AsOdcmProperty().Projection.Type.Name.ToUpperFirstChar();
            }
            else if (c is OdcmProperty)
            {
                return c.AsOdcmProperty().Projection.Type.Name.ToUpperFirstChar();
            }
            return c.Name.ToUpperFirstChar();
        }

        public static string MethodName(this OdcmObject c)
        {
            return c.Name.Substring(c.Name.IndexOf(".") + 1).ToUpperFirstChar();
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

        public static string ITypeName(this OdcmObject c)
        {
            return "I" + c.TypeName();
        }

        public static string BaseTypeName(this OdcmObject c)
        {
            return "Base" + c.TypeName();
        }

        public static string IBaseTypeName(this OdcmObject c)
        {
            return "I" + c.BaseTypeName();
        }

        public static string TypeStreamRequest(this OdcmObject c)
        {
            if (c.TypeName().EndsWith("stream", StringComparison.InvariantCultureIgnoreCase))
            {
                return c.TypeName() + "Request";
            }
            return c.TypeName() + "StreamRequest";
        }

        public static string ITypeStreamRequest(this OdcmObject c)
        {
            return "I" + c.TypeStreamRequest();
        }

        public static string BaseTypeStreamRequest(this OdcmObject c)
        {
            return "Base" + c.TypeStreamRequest();
        }

        public static string IBaseTypeStreamRequest(this OdcmObject c)
        {
            return "I" + c.BaseTypeStreamRequest();
        }

        public static string TypeStreamRequestBuilder(this OdcmObject c)
        {
            return c.TypeStreamRequest() + "Builder";
        }

        public static string ITypeStreamRequestBuilder(this OdcmObject c)
        {
            return "I" + c.TypeStreamRequestBuilder();
        }

        public static string BaseTypeStreamRequestBuilder(this OdcmObject c)
        {
            return "Base" + c.TypeStreamRequestBuilder();
        }

        public static string IBaseTypeStreamRequestBuilder(this OdcmObject c)
        {
            return "I" + c.BaseTypeStreamRequestBuilder();
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

        public static string ITypeRequest(this OdcmObject c)
        {
            return "I" + c.TypeRequest();
        }

        public static string IBaseTypeRequest(this OdcmObject c)
        {
            return "I" + c.BaseTypeRequest();
        }

        public static string TypeRequestBuilder(this OdcmObject c)
        {
            return c.TypeRequest() + "Builder";
        }

        public static string ITypeRequestBuilder(this OdcmObject c)
        {
            return "I" + c.TypeRequestBuilder();
        }

        public static string BaseTypeRequestBuilder(this OdcmObject c)
        {
            return "Base" + c.TypeRequestBuilder();
        }

        public static string IBaseTypeRequestBuilder(this OdcmObject c)
        {
            return "I" + c.BaseTypeRequestBuilder();
        }

        public static string TypeWithReferencesRequest(this OdcmObject c)
        {
            return c.TypeName() + "WithReferenceRequest";
        }

        public static string BaseTypeWithReferencesRequest(this OdcmObject c)
        {
            return "Base" + c.TypeWithReferencesRequest();
        }

        public static string ITypeWithReferencesRequest(this OdcmObject c)
        {
            return "I" + c.TypeWithReferencesRequest();
        }

        public static string IBaseTypeWithReferencesRequest(this OdcmObject c)
        {
            return "I" + c.BaseTypeWithReferencesRequest();
        }

        public static string TypeWithReferencesRequestBuilder(this OdcmObject c)
        {
            return c.TypeWithReferencesRequest() + "Builder";
        }

        public static string ITypeWithReferencesRequestBuilder(this OdcmObject c)
        {
            return $"I{c.TypeWithReferencesRequestBuilder()}";
        }

        public static string BaseTypeWithReferencesRequestBuilder(this OdcmObject c) => $"Base{c.TypeWithReferencesRequestBuilder()}";

        public static string IBaseTypeWithReferencesRequestBuilder(this OdcmObject c)
        {
            return "I" + c.BaseTypeWithReferencesRequestBuilder();
        }

        public static string TypeReferenceRequest(this OdcmObject c)
        {
            return c.TypeName() + "ReferenceRequest";
        }

        public static string BaseTypeReferenceRequest(this OdcmObject c)
        {
            return "Base" + c.TypeReferenceRequest();
        }

        public static string ITypeReferenceRequest(this OdcmObject c)
        {
            return "I" + c.TypeReferenceRequest();
        }

        public static string IBaseTypeReferenceRequest(this OdcmObject c)
        {
            return "I" + c.BaseTypeReferenceRequest();
        }

        public static string TypeReferenceRequestBuilder(this OdcmObject c)
        {
            return c.TypeReferenceRequest() + "Builder";
        }

        public static string ITypeReferenceRequestBuilder(this OdcmObject c)
        {
            return "I" + c.TypeReferenceRequestBuilder();
        }

        public static string BaseTypeReferenceRequestBuilder(this OdcmObject c)
        {
            return "Base" + c.TypeReferenceRequestBuilder();
        }

        public static string IBaseTypeReferenceRequestBuilder(this OdcmObject c)
        {
            return "I" + c.BaseTypeReferenceRequestBuilder();
        }

        public static string TypeCollectionPage(this OdcmObject c)
        {
            return c.TypeName() + "CollectionPage";
        }

        public static string ITypeCollectionPage(this OdcmObject c)
        {
            return "I" + c.TypeCollectionPage();
        }

        public static string TypeCollectionWithReferencesPage(this OdcmObject c)
        {
            return $"{c.TypeName()}CollectionWithReferencesPage";
        }

        public static string ITypeCollectionWithReferencesPage(this OdcmObject c)
        {
            return "I" + c.TypeCollectionWithReferencesPage();
        }

        public static string BaseTypeCollectionPage(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionPage();
        }

        public static string IBaseTypeCollectionPage(this OdcmObject c)
        {
            return "I" + c.BaseTypeCollectionPage();
        }

        public static string BaseTypeCollectionWithReferencesPage(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionWithReferencesPage();
        }

        public static string IBaseTypeCollectionWithReferencesPage(this OdcmObject c)
        {
            return "I" + c.BaseTypeCollectionWithReferencesPage();
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

        public static string ITypeCollectionRequest(this OdcmObject c)
        {
            return "I" + c.TypeCollectionRequest();
        }

        public static string BaseTypeCollectionRequest(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionRequest();
        }

        public static string IBaseTypeCollectionRequest(this OdcmObject c)
        {
            return "I" + c.BaseTypeCollectionRequest();
        }

        public static string TypeCollectionRequestBuilder(this OdcmObject c)
        {
            return c.TypeCollectionRequest() + "Builder";
        }

        public static string ITypeCollectionRequestBuilder(this OdcmObject c)
        {
            return "I" + c.TypeCollectionRequestBuilder();
        }

        public static string BaseTypeCollectionRequestBuilder(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionRequestBuilder();
        }

        public static string IBaseTypeCollectionRequestBuilder(this OdcmObject c)
        {
            return "I" + c.BaseTypeCollectionRequestBuilder();
        }

        public static string TypeCollectionWithReferencesRequest(this OdcmObject c)
        {
            return c.TypeName() + "CollectionWithReferencesRequest";
        }

        public static string ITypeCollectionWithReferencesRequest(this OdcmObject c)
        {
            return "I" + c.TypeCollectionWithReferencesRequest();
        }

        public static string BaseTypeCollectionWithReferencesRequest(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionWithReferencesRequest();
        }

        public static string IBaseTypeCollectionWithReferencesRequest(this OdcmObject c)
        {
            return "I" + c.BaseTypeCollectionWithReferencesRequest();
        }

        public static string TypeCollectionWithReferencesRequestBuilder(this OdcmObject c)
        {
            return c.TypeCollectionWithReferencesRequest() + "Builder";
        }

        public static string ITypeCollectionWithReferencesRequestBuilder(this OdcmObject c)
        {
            return "I" + c.TypeCollectionWithReferencesRequestBuilder();
        }

        public static string BaseTypeCollectionWithReferencesRequestBuilder(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionWithReferencesRequestBuilder();
        }

        public static string IBaseTypeCollectionWithReferencesRequestBuilder(this OdcmObject c)
        {
            return "I" + c.BaseTypeCollectionWithReferencesRequestBuilder();
        }

        public static string TypeCollectionReferenceRequest(this OdcmObject c)
        {
            return c.TypeName() + "CollectionReferenceRequest";
        }

        public static string ITypeCollectionReferenceRequest(this OdcmObject c)
        {
            return "I" + c.TypeCollectionReferenceRequest();
        }

        public static string BaseTypeCollectionReferenceRequest(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionReferenceRequest();
        }

        public static string IBaseTypeCollectionReferenceRequest(this OdcmObject c)
        {
            return "I" + c.BaseTypeCollectionReferenceRequest();
        }

        public static string TypeCollectionReferenceRequestBuilder(this OdcmObject c)
        {
            return c.TypeCollectionReferenceRequest() + "Builder";
        }

        public static string ITypeCollectionReferenceRequestBuilder(this OdcmObject c)
        {
            return "I" + c.TypeCollectionReferenceRequestBuilder();
        }

        public static string BaseTypeCollectionReferenceRequestBuilder(this OdcmObject c)
        {
            return "Base" + c.TypeCollectionReferenceRequestBuilder();
        }

        public static string IBaseTypeCollectionReferenceRequestBuilder(this OdcmObject c)
        {
            return "I" + c.BaseTypeCollectionReferenceRequestBuilder();
        }

        public static string TypeBody(this OdcmObject c)
        {
            return c.TypeName() + "Body";
        }

        public static string BaseTypeBody(this OdcmObject c)
        {
            return "Base" + c.TypeBody();
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
            var returnType = c.AsOdcmMethod().ReturnType;
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

        public static string MethodParametersJavadocSignature(this OdcmMethod method)
        {
            var parameterSignatureBuilder = new StringBuilder();
            foreach (var p in method.Parameters)
            {
                parameterSignatureBuilder.AppendFormat("\r\n     * @param {0} the {0}", p.ParamName());
            }
            return parameterSignatureBuilder.ToString();
        }

        public static string MethodParametersSignature(this OdcmMethod method)
        {
            var parameterSignatureBuilder = new StringBuilder();
            foreach (var p in method.Parameters)
            {
                parameterSignatureBuilder.AppendFormat(", final {0} {1}", p.ParamType(), p.ParamName());
            }
            return parameterSignatureBuilder.ToString();
        }

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
                var paramName = param.Name.ToLowerFirstChar();
                pageValuesBuilder.AppendFormat(", /* {0} */ null", paramName);
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

        public static string CollectionPageGeneric(this OdcmObject c)
        {
            if (c is OdcmMethod)
            {
                string returnType = OdcmMethodReturnType(c as OdcmMethod);
                return "<" + returnType + ", " + c.ITypeCollectionRequestBuilder() + ">";
            }
            return "<" + c.TypeName() + ", " + c.ITypeCollectionRequestBuilder() + ">";
        }

        public static string CollectionPageWithReferencesGeneric(this OdcmObject c)
        {
            if (c is OdcmMethod)
            {
                return "<" + c.ClassTypeName() + ", " + c.ITypeWithReferencesRequestBuilder() + ">";
            }
            return "<" + c.TypeName() + ", " + c.ITypeCollectionWithReferencesRequestBuilder() + ">";
        }

        public static string CollectionRequestGeneric(this OdcmObject c)
        {
            if (c is OdcmMethod)
            {
                return "<" + c.TypeCollectionResponse() + ", " + c.ITypeCollectionPage() + ">";
            }
            return "<" + c.TypeCollectionResponse() + ", " + c.ITypeCollectionPage() + ">";
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

        public static string CreatePackageDefForBaseEntityCollectionResponse(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            sb.Append(host.CreatePackageDefinition());
            var importFormat = @"import {0}.{1}.{2};";
            sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "models.extensions",
                            TypeName(host.CurrentType));
            sb.Append("\n");
            return sb.ToString();
        }

        public static string CreatePackageDefForIBaseMethodRequest(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            sb.Append(host.CreatePackageDefinition());
            var importFormat = @"import {0}.{1}.{2};";

            var returnType = host.CurrentType.ReturnType();
            if (returnType != "Void" && !(host.CurrentType.AsOdcmMethod().ReturnType is OdcmPrimitiveType))
            {
                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "models.extensions",
                            returnType);
                sb.Append("\n");
            }

            sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "requests.extensions",
                            host.CurrentType.ITypeRequest());
            sb.Append("\n");
            return sb.ToString();
        }

        public static string CreatePackageDefForIBaseEntityRequest(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            sb.Append(host.CreatePackageDefinition());
            var importFormat = @"import {0}.{1}.{2};";
            sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "models.extensions",
                            host.CurrentType.TypeName());
            sb.Append("\n");
            return sb.ToString();
        }

        public static string CreatePackageDefForBaseMethodRequest(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            sb.Append(host.CreatePackageDefForIBaseMethodRequest());
            var importFormat = @"import {0}.{1}.{2};";
            sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "requests.extensions",
                            host.CurrentType.TypeRequest());
            sb.Append("\n");
            return sb.ToString();
        }

        public static string CreatePackageDefForBaseMethodRequestBuilder(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            sb.Append(host.CreatePackageDefinition());
            var importFormat = @"import {0}.{1}.{2};";
            sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "requests.extensions",
                            host.CurrentType.ITypeRequest());
            sb.Append("\n");

            sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "requests.extensions",
                            host.CurrentType.TypeRequest());
            sb.Append("\n");

            foreach (var method in host.CurrentType.AsOdcmMethod().WithOverloads())
            {
                sb = ImportClassesOfMethodParameters(method, importFormat, sb);
            }
            return sb.ToString();
        }

        public static StringBuilder ImportClassesOfMethodParameters(OdcmMethod method, string importFormat, StringBuilder sb)
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

                    importStatements.Add(string.Format(importFormat, p.Type.Namespace.Name.NamespaceName(), p.GetPackagePrefix(), propertyType));
                }
            }

            sb.Append(string.Join("\n", importStatements));

            if (appendEnumSet)
            {
                sb.Append("\nimport java.util.EnumSet;\n");
            }

            return sb;
        }

        public static string CreatePackageDefForIBaseMethodBodyRequest(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            sb.Append(host.CreatePackageDefinition());
            var importFormat = @"import {0}.{1}.{2};";
            var returnType = host.CurrentType.ReturnType();
            if (returnType != "Void" && !(host.CurrentType.AsOdcmMethod().ReturnType is OdcmPrimitiveType))
            {
                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "models.extensions",
                            returnType);
                sb.Append("\n");
            }
            if (host.CurrentType.AsOdcmMethod().IsCollection)
            {
                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "requests.extensions",
                            host.CurrentType.ITypeCollectionRequest());
                sb.Append("\n");
            }
            else
            {
                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "requests.extensions",
                            host.CurrentType.ITypeRequest());
                sb.Append("\n");
            }

            return sb.ToString();
        }

        public static string CreatePackageDefForBaseMethodBodyRequest(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            sb.Append(host.CreatePackageDefinition());
            var importFormat = @"import {0}.{1}.{2};";

            sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "models.extensions",
                            host.CurrentType.TypeBody());
            sb.Append("\n");
            var returnType = host.CurrentType.ReturnType();
            if (returnType != "Void" && !(host.CurrentType.AsOdcmMethod().ReturnType is OdcmPrimitiveType))
            {
                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "models.extensions",
                            returnType);
                sb.Append("\n");
            }

            if (host.CurrentType.AsOdcmMethod().IsCollection)
            {
                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "requests.extensions",
                            host.CurrentType.ITypeCollectionRequest());
                sb.Append("\n");

                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "requests.extensions",
                            host.CurrentType.TypeCollectionRequest());
                sb.Append("\n");
            }
            else
            {
                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "requests.extensions",
                            host.CurrentType.ITypeRequest());
                sb.Append("\n");

                sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "requests.extensions",
                            host.CurrentType.TypeRequest());
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public static string CreatePackageDefIBaseMethodRequestBuilder(this CustomT4Host host)
        {
            var sb = new StringBuilder();
            sb.Append(host.CreatePackageDefinition());
            var importFormat = @"import {0}.{1}.{2};";
            sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "requests.extensions",
                            host.CurrentType.ITypeRequest());
            sb.Append("\n");
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
                            "models.extensions",
                            modelClassName);
            sb.Append("\n");
            sb.AppendFormat(importFormat,
                            host.CurrentNamespace(),
                            "requests.extensions",
                            host.CurrentType.ITypeCollectionRequestBuilder());

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

            sb.AppendFormat(@"import {0}.concurrency.*;
import {0}.core.*;
import {0}.http.*;
import {0}.serializer.*;
import java.util.Arrays;
import java.util.EnumSet;", host.CurrentModel.GetNamespace().NamespaceName());

            sb.Append("\n");
            var importFormat = @"import {0}.{1}.{2};";
            Dictionary<string, int> uniqueStore = new Dictionary<string, int>();

            foreach (var property in properties.Where(p => !p.Projection.Type.Name.Equals("Stream")))
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
                            property.Projection.Type.Namespace.Name.NamespaceName(),
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
                            (host.CurrentType.BaseClass() as OdcmClass).Namespace.Name.NamespaceName(),
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
                                host.CurrentModel.GetNamespace().NamespaceName(),
                            "models.extensions",
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
                            host.CurrentModel.GetNamespace().NamespaceName(),
                            "models.extensions",
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
                foreach (var property in properties.Where(p => p.IsCollection() && p.IsNavigation()))
                {
                    if (property.Type is OdcmPrimitiveType)
                        continue;

                    var propertyType = TypeCollectionResponse(property);
                    string importstr = String.Format(importFormat,
                                property.Projection.Type.Namespace.Name.NamespaceName(),
                                GetPrefixForRequests(),
                                propertyType);
                    if (!uniqueStore.ContainsKey(importstr))
                    {
                        uniqueStore.Add(importstr, 0);
                        sb.Append(importstr);
                        sb.Append("\n");
                    }

                    string propertyValue = TypeCollectionPage(property);
                    string importstr1 = String.Format(importFormat,
                        property.Projection.Type.Namespace.Name.NamespaceName(),
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
            var format = @"package {0}.{1};

import {2}.concurrency.*;
import {2}.core.*;
import {0}.models.extensions.*;
import {0}.models.generated.*;{3}{4}
import {2}.http.*;
import {0}.requests.extensions.*;
import {2}.serializer.*;

import java.util.Arrays;
import java.util.EnumSet;";

            // We need this for disambiguation of generated model class/interfaces references.
            string fullyQualifiedImport = host.GetFullyQualifiedImportStatementForModel();

            string @namespace;
            string methodImports = string.Empty;
            switch (host.CurrentType)
            {
                case OdcmProperty p:
                    @namespace = p.Type.Namespace.Name.NamespaceName();
                    break;
                case OdcmMethod m:
                    var sb = new StringBuilder(Environment.NewLine);
                    methodImports = ImportClassesOfMethodParameters(m, "import {0}.{1}.{2};", sb).ToString();
                    goto default;
                default:
                    @namespace = host.CurrentNamespace();
                    break;
            }

            return string.Format(format,
                @namespace,
                host.TemplateInfo.OutputParentDirectory.Replace("_", "."),
                host.CurrentModel.GetNamespace().NamespaceName(),
                fullyQualifiedImport,
                methodImports);
        }

        public static string CurrentNamespace(this CustomT4Host host)
        {
            switch (host.CurrentType)
            {
                case OdcmType t:
                    return t.Namespace.Name.NamespaceName();
                case OdcmProperty p:
                    return p.Projection.Type.Namespace.Name.NamespaceName();
                default:
                    return string.Empty;
            }
        }

        //Getting import prefix using property name for model classes
        private static string GetPrefixForModels() => "models.extensions";

        //Getting import prefix using property name for Request classes
        private static string GetPrefixForRequests() => "requests.extensions";

        public static string GetPackagePrefix(this OdcmProperty property)
        {
            if (property.Type is OdcmEnum)
                return "models.generated";

            return GetPrefixForModels();
        }

        //Get package prefix using OdcmProperty for model classes
        public static string GetPackagePrefix(this OdcmParameter parameter)
        {
            if (parameter.Type is OdcmEnum)
                return "models.generated";

            return GetPrefixForModels();
        }

        public static string CreateParameterDef(IEnumerable<OdcmParameter> parameters)
        {
            var sb = new StringBuilder();

            var format =
    @"    /**
     * The {0}.
     * {1}
     */
    @SerializedName(""{2}"")
    @Expose
    public {3} {4};

";
            foreach (var p in parameters)
            {
                sb.AppendFormat(
                    format,
                    p.ParamName().SplitCamelCase(),
                    ReplaceInvalidCharacters(p.LongDescription),
                    p.ParamName(),
                    p.ParamType(),
                    p.ParamName().SanitizePropertyName(p).ToLowerFirstChar()
                );
            }
            return sb.ToString();
        }

        public static string CreateRawJsonObject()
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
    public JsonObject getRawObject() {
        return rawObject;
    }

    /**
     * Gets serializer
     *
     * @return the serializer
     */
    protected ISerializer getSerializer() {
        return serializer;
    }

    /**
     * Sets the raw JSON object
     *
     * @param serializer the serializer
     * @param json the JSON object to set this object to
     */
    public void setRawObject(final ISerializer serializer, final JsonObject json) {
        this.serializer = serializer;
        rawObject = json;
";
        }

        public static string UpdatePropertiesWithinSetRawObject(IEnumerable<OdcmProperty> properties = null, bool isComplexType = false)
        {
            var sb = new StringBuilder();
            if (!isComplexType && properties != null)
            {
                foreach (var property in properties.Where(p => p.IsCollection() && p.IsNavigation()))
                {
                    sb.AppendFormat(
    @"
        if (json.has(""{0}"")) {{
            final {1} response = new {1}();
            if (json.has(""{0}@odata.nextLink"")) {{
                response.nextLink = json.get(""{0}@odata.nextLink"").getAsString();
            }}

            final JsonObject[] sourceArray = serializer.deserializeObject(json.get(""{0}"").toString(), JsonObject[].class);
            final {3}[] array = new {3}[sourceArray.length];
            for (int i = 0; i < sourceArray.length; i++) {{
                array[i] = serializer.deserializeObject(sourceArray[i].toString(), {3}.class);
                array[i].setRawObject(serializer, sourceArray[i]);
            }}
            response.value = Arrays.asList(array);
            {0} = new {2}(response, null);
        }}
",
                property.Name.SanitizePropertyName(property),
                TypeCollectionResponse(property),
                TypeCollectionPage(property),
                property.GetTypeString());
                }
            }
            sb.Append("    }");
            return sb.ToString();
        }

        public static string UpdateListPropertiesWithinSetRawObject(IEnumerable<string> listProperties)
        {
            var sb = new StringBuilder();
            foreach (var property in listProperties)
            {
                sb.AppendFormat(
    @"
        if (json.has(""{0}"")) {{
            final JsonArray array = json.getAsJsonArray(""{0}"");
            for (int i = 0; i < array.size(); i++) {{
                {0}.get(i).setRawObject(serializer, (JsonObject) array.get(i));
            }}
        }}
",
                property);
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
        private static string GetSanitizedDescription(OdcmProperty property)
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
     * {4}
     */
    @SerializedName(""{1}"")
    @Expose
    public {2} {3};

";
            var collectionFormat =
    @"    /**
     * The {0}.
     * {4}
     */
    public {2} {3};

";

            foreach (var property in properties.Where(p => !p.Projection.Type.Name.Equals("Stream")))
            {
                var propertyName = property.Name.ToUpperFirstChar();
                var propertyType = "";
                var propertyFormat = format;
                if (property.IsCollection)
                {
                    if (!property.IsNavigation())
                    {
                        propertyType = "java.util.List<" + property.GetTypeString() + ">";
                    }
                    else
                    {
                        propertyType = TypeCollectionPage(property);
                        propertyFormat = collectionFormat;
                    }
                }
                else
                {
                    propertyType = property.GetTypeString();
                }

                sb.AppendFormat(propertyFormat,
                    propertyName.SplitCamelCase(),
                    property.Name,
                    propertyType,
                    property.Name.SanitizePropertyName(property).ToLowerFirstChar(),
                    GetSanitizedDescription(property));
            }
            return sb.ToString();
        }

        /// Creates a class declaration
        /// name = the name of the class
        /// extends = the class it extends
        /// implements = the interface it extends
        public static string CreateClassDef(string name, string extends = null, string implements = null)
        {
            return CreateClassOrInterface(name, true, extends, implements);
        }

        public static string CreateInterfaceDef(string name, string extends = null)
        {
            return CreateClassOrInterface(name, false, extends, null);
        }

        public static string CreateClassOrInterface(string name, bool isClass = true, string extends = null, string implements = null)
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
 * The {1} for the {0}.
 */
public {1} {2}{3}{4} {{";
            string declaration = string.Format(format,
                isClass ? name.SplitCamelCase() : name.SplitCamelCase().Remove(0, 1),
                isClass ? "class" : "interface",
                name,
                extendsStr,
                implementsStr);

            return CreatAutogeneratedWarning() + declaration;
        }
    }
}
