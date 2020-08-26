// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.ObjC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Vipr.Core.CodeModel;

    public class CodeWriterObjC : CodeWriterBase
    {

        public CodeWriterObjC() : base()
        {
        }

        public CodeWriterObjC(OdcmModel model) : base(model)
        {
        }

        public string GetPrefix() => ConfigurationService.Settings.NamespacePrefix;

        public string GetNamespacePrefixForType(OdcmType type) => TypeHelperObjC.GetNamespacePrefixForType(type);

        public string GetStaticCodePrefix()
        {
            return ConfigurationService.Settings.StaticCodePrefix;
        }

        public override string WriteOpeningCommentLine()
        {
            return "";
        }

        public override string WriteClosingCommentLine()
        {
            return this.NewLineCharacter;
        }

        public override string WriteInlineCommentChar()
        {
            return "// ";
        }

        public string GetInterfaceLine(OdcmClass entityType, string baseClass = null)
        {
            string baseEntity = null;
            if (baseClass != null)
            {
                baseEntity = baseClass;
            }
            else
            {
                baseEntity = entityType.Base == null ? "NSObject"
                                           : GetNamespacePrefixForType(entityType.Base) + entityType.Base.Name.Substring(entityType.Base.Name.LastIndexOf(".") + 1);
            }
            var interfaceLineBuilder = new StringBuilder();
            // NSObject lives in Foundation/Foundation.h
            var baseImport = (baseEntity.Equals("NSObject")) ? "#import <Foundation/Foundation.h>" : String.Format("#import \"{0}.h\"", baseEntity);
            interfaceLineBuilder.AppendLine(baseImport);

            interfaceLineBuilder.AppendLine().AppendLine().AppendLine(this.GetHeaderDoc(entityType.Name))
            .AppendFormat("@interface {0}{1} : {2}", GetNamespacePrefixForType(entityType), entityType.Name.ToUpperFirstChar(), baseEntity);

            return interfaceLineBuilder.ToString();
        }

        public string GetHeaderDoc(string name)
        {

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(@"/**");
            stringBuilder.AppendLine().AppendFormat(@"* The header for type {0}.", name);
            stringBuilder.AppendLine().AppendLine(@"*/");

            return stringBuilder.ToString();
        }

        public string GetImplementationDoc(string name)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(@"/**");
            stringBuilder.AppendLine().AppendFormat(@"* The implementation file for type {0}.", name);
            stringBuilder.AppendLine().AppendLine(@"*/");

            return stringBuilder.ToString();
        }

        public string GetMethodDoc(string name, List<OdcmProperty> parameters)
        {
            return "";
        }

        public string GetParamsForRaw(IEnumerable<string> parameters)
        {
            string param = "With";

            foreach (var p in parameters)
            {
                param += param == "With" ? string.Format("{0}:(NSString *) {1} ", char.ToUpper(p[0]) + p.Substring(1), p.ToLowerFirstChar()) :
                 string.Format("{0}:(NSString *) {0} ", p.ToLowerFirstChar());
            }

            param += parameters.Count() > 0 ? "callback" : "Callback";

            return param;
        }

        public string GetParamRaw(string type)
        {
            return "NSString *" + type.ToLowerFirstChar();
        }

        public string GetImportsClass(IEnumerable<OdcmProperty> references, IEnumerable<string> extraImports = null, IEnumerable<string> extraClasses = null)
        {
            if (references != null && references.Any())
            {
                var imports = new StringBuilder();
                var classes = new StringBuilder("@class ");
                var classType = references.First().Class.GetTypeString();
                foreach (var type in references.Select(prop => prop.Projection.Type).Distinct())
                {
                    if (type is OdcmEnum)
                    {
                        imports.AppendFormat("#import \"{0}.h\"", type.GetTypeString()).AppendLine();
                    }
                    // CGFloat is in UIKit/UIKit
                    else if (type.GetTypeString().Equals("CGFloat"))
                    {
                        imports.AppendFormat("#import <UIKit/UiKit.h>").AppendLine();
                    }
                    else if (!type.IsSystem() && type.IsComplex() && type.GetTypeString() != "id" &&
                             type.GetTypeString() != classType)
                    {
                        classes.AppendFormat("{0}, ", type.GetTypeString());
                    }
                }
                if (extraImports != null)
                {
                    foreach (var extraType in extraImports)
                    {
                        imports.AppendFormat("#import \"{0}.h\"", extraType).AppendLine();
                    }
                }

                if (extraClasses != null)
                {
                    foreach (var extraType in extraClasses)
                    {
                        classes.AppendFormat("{0}, ", extraType);
                    }
                }

                var classString = classes.AppendLine().ToString();
                int lastOccurance = classString.LastIndexOf(',');
                if (lastOccurance < 0)
                {
                    classString = @"";
                }
                else
                {
                    classString = classString.Remove(lastOccurance, 1).Insert(lastOccurance, ";");
                }

                var importsString = imports.ToString();
                return (string.IsNullOrWhiteSpace(classString.Trim()) ? "" : classString) +
                       (string.IsNullOrWhiteSpace(importsString.Trim()) ? "" : importsString);
            }
            return "";
        }

        public string GetParametersToJsonRaw(IEnumerable<string> parameters)
        {
            if (!parameters.Any()) { return new StringBuilder().AppendLine().ToString(); }

            var result = new StringBuilder();

            if (parameters.Any()) {
                result.Append("NSArray *parameters = [[NSArray alloc] initWithObjects:");

                foreach (var name in parameters) {
                    result.AppendLine().Append("                          ")
                    .AppendFormat("[[NSDictionary alloc] initWithObjectsAndKeys :{0},@\"{1}\", nil],", name.ToLowerFirstChar(), name);
                }

                result.Append(" nil];");
                result.AppendLine().AppendLine().Append("\t").Append("NSData* payload = " +
              "[[MSOrcBaseContainer generatePayloadWithParameters:parameters dependencyResolver:self.resolver] dataUsingEncoding:NSUTF8StringEncoding];");

                result.AppendLine().AppendLine().Append("\t").AppendLine("[request setContent:payload];").AppendLine();
            }

            return result.ToString();
        }

        public string GetParametersToJson(List<OdcmParameter> parameters) {
            if (!parameters.Any()) { return string.Empty; }

            var result = new StringBuilder();

            foreach (var param in parameters) {
                if (param.Type.GetTypeString() == "BOOL") {
                    result.AppendLine().Append("\t").AppendFormat("NSString *{0}String = [self.resolver.jsonSerializer serialize:({0} ? @\"true\" : @\"false\") property:@\"{1}\"];", param.Name.ToLowerFirstChar(), param.Name);
                } else if (param.Type.GetTypeString() == "int") {
                    result.AppendLine().Append("\t").AppendFormat("NSString *{0}String = [self.resolver.jsonSerializer serialize:[[NSString alloc] initWithFormat:@\"%d\", {0}],@\"{1}\"],", param.Name.ToLowerFirstChar(), param.Name);
                } else if (param.IsCollection) {
                    result.AppendLine().Append("\t")
                    .AppendFormat("NSString *{0}String = [self.resolver.jsonSerializer serialize:{0} property:@\"{1}\"];",
                    param.Name.ToLowerFirstChar(),
                    param.Name);
                } else {
                    result.AppendLine().Append("\t").AppendFormat("NSString *{0}String = [self.resolver.jsonSerializer serialize:{0} property:@\"{1}\"];", param.Name.ToLowerFirstChar(), param.Name);
                }
            }
            return result.ToString();
        }

        public string GetParametersForRawCall(IEnumerable<String> parameters) {
            //if(!parameters.Any()) { return string.Empty;}

            string result = "With";
            foreach (var param in parameters) {
                if (result == "With") {
                    result += string.Format("{0}:{1}String ", char.ToUpper(param[0]) + param.Substring(1)
                                        , param.ToLowerFirstChar());
                } else {
                    result += string.Format("{0}:{0}String ", param.ToLowerFirstChar());
                }
            }
            result += parameters.Count() > 0 ? "callback" : "Callback";
            return result;
        }

        public string CreateEndOfFile(string name) {
            return string.Format("/n{0} EndOfFile", name);
        }

        public string GetParamsString(IEnumerable<OdcmParameter> parameters)
        {
            string param = "";
            foreach (OdcmParameter p in parameters)
            {
                string paramName = (p == parameters.First()) ? p.Name.ToUpperFirstChar() : p.Name.ToLowerFirstChar();

                if (p.IsCollection)
                {
                    param += string.Format("{0}:(NSArray *){1}", paramName, p.Name.ToLowerFirstChar());

                }
                else
                {
                    if (p.Type.IsComplex())
                    {
                        param += string.Format("{0}:({1} *){2}", paramName, p.Type.GetFullType(), p.Name.ToLowerFirstChar());
                    }
                    else
                    {
                        param += string.Format("{0}:({1}){2}", paramName, p.Type.GetFullType(), p.Name.ToLowerFirstChar());
                    }
                }
                param += " ";
            }

            return param;
        }

        public string GetNetworkCompletionBlock(string parameterType, string parameterName)
        {
            if (!string.IsNullOrEmpty(parameterType) && !string.IsNullOrEmpty(parameterName))
            {
                return "(void (^)(" + parameterType + " *" + parameterName + ", NSError *error))";
            }
            else
            {
                return "(void(^)(NSError *error))";
            }
        }

        public string GetTypeForAction(OdcmMethod action) {
            if (action.ReturnType.IsComplex()) {
                if (action.IsCollection) {
                        return "NSArray *";
                }
                return action.ReturnType.IsSystem() ? action.ReturnType.GetTypeString() : action.ReturnType.GetTypeString() + " *";
            }
            return action.ReturnType.GetTypeString();
        }

        public string GetMethodHeader(OdcmMethod action) {
            var returnString = action.ReturnType == null ? "int returnValue"
            : this.GetTypeForAction(action) + action.ReturnType.Name.ToLowerFirstChar();
            return string.Format("- (void){0}{1}:(void (^)({2}, MSOrcError *error))callback",
            action.Name.ToLowerFirstChar(), this.GetParamsString(action.Parameters), returnString);
        }

        public string GetMethodHeaderRaw(OdcmMethod action) {
            return string.Format("- (void){0}Raw{1}:(void(^)(NSString *returnValue, MSOrcError *error))callback",
            action.Name.ToLowerFirstChar(),
            this.GetParamsForRaw(action.Parameters.Select(p => p.Name)), (action.ReturnType == null ? "NSString * resultCode " : this.GetParamRaw(action.ReturnType.Name)));
        }

        private static ICollection<string> semanticOwnedObjectsKeywords;
        public static ICollection<string> SemanticOwnedObjectsKeywords
        {
            get
            {
                if (semanticOwnedObjectsKeywords == null)
                {
                    semanticOwnedObjectsKeywords = new HashSet<string>
                    {
                        "alloc",
                        "copy",
                        "new",
                        "mutableCopy",
                    };
                }
                return semanticOwnedObjectsKeywords;
            }
        }

        public string GetGetterString(string propertyName)
        {
            //Fixes names that violate semantic rules for methods that
            //create owned objects

            if(SemanticOwnedObjectsKeywords.Any(x => propertyName.StartsWith(x,StringComparison.OrdinalIgnoreCase)))
            {
                return "get" + propertyName.ToPascalize();
            }

            return propertyName;
        }

        public string GetPropertyDeclaration(string propertyName, string type)
        {
            var getterName = this.GetGetterString(propertyName);

            return "@property (nonatomic, getter=" + getterName + ") " + type + " " + propertyName + ";";
        }

        public string GetName(string name) {
            if (name.Trim() == "description") return "$$__$$description";
            if (name.Trim() == "default") return "$$__$$default";
            if (name.Trim() == "self") return "$$__$$self";
            return name;
        }
    }
}
