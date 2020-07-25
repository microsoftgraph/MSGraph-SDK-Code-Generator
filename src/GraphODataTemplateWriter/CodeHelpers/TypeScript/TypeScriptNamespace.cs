using Microsoft.Graph.ODataTemplateWriter.CodeHelpers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vipr.Core.CodeModel;

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.TypeScript
{
    /// <summary>
    /// Printable version of a namespace in the context of TypeScript code generation
    /// </summary>
    public class TypeScriptNamespace
    {
        /// <summary>
        /// Namespace name, e.g. Microsoft.Graph.CallRecords
        /// </summary>
        public string NamespaceName { get; }

        public bool IsMainNamespace => NamespaceName == MainNamespaceName;
        private string NamespaceIndent => IsMainNamespace ? string.Empty : TabSpace;

        /// <summary>
        /// Groups of types to be printed
        /// </summary>
        private readonly List<OdcmEntityClass> Entities;
        private readonly List<OdcmComplexClass> ComplexTypes;
        private readonly List<OdcmEnum> Enums;

        /// <summary>
        /// StringBuilder to generate namespace output for Typewriter
        /// </summary>
        private StringBuilder sb;

        // constants
        private const int MaxLineLength = 120;
        private const string MainNamespaceName = "Microsoft.Graph";
        private const string TypeScriptMainNamespaceName = "microsoftgraph";
        private const string TabSpace = "    ";

        /// <summary>
        /// Groups entity, complex and enum types to be printed
        /// </summary>
        /// <param name="odcmNamespace">Odcm Namespace</param>
        public TypeScriptNamespace(OdcmNamespace odcmNamespace)
        {
            NamespaceName = odcmNamespace.GetNamespaceName();
            Entities = new List<OdcmEntityClass>();
            ComplexTypes = new List<OdcmComplexClass>();
            Enums = new List<OdcmEnum>();

            foreach (var type in odcmNamespace.Types)
            {
                switch (type)
                {
                    case OdcmEntityClass e:
                        Entities.Add(e);
                        break;
                    case OdcmComplexClass c:
                        if (c.CanonicalName().ToLowerInvariant() != "microsoft.graph.json")
                        {
                            ComplexTypes.Add(c);
                        }
                        break;
                    case OdcmEnum e:
                        Enums.Add(e);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Types in the order of
        /// 1. Enums
        /// 2. Entities
        /// 3. Complex Types
        /// </summary>
        /// <returns>String representation of a Typewriter TypeScript output</returns>
        public override string ToString()
        {
            sb = new StringBuilder();

            if (!IsMainNamespace)
            {
                sb.AppendLine($"export namespace {NamespaceName.Replace("Microsoft.Graph", "")} {{");
            }

            Enums.ForEach(@enum => AddEnum(@enum));
            Entities.ForEach(entity => AddEntityOrComplexType(entity));
            ComplexTypes.ForEach(complex => AddEntityOrComplexType(complex));

            if (!IsMainNamespace)
            {
                sb.AppendLine("}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Appends either:
        ///     export type EnumType = EnumValue1 | EnumValue2 | EnumValue3;
        /// or:
        ///     export type EnumType =
        ///     | EnumValue1
        ///     | EnumValue2
        ///     | EnumValue3;
        /// </summary>
        /// <param name="enumType">enum</param>
        private void AddEnum(OdcmEnum enumType)
        {
            var enumTypeName = enumType.Name.UpperCaseFirstChar();
            var enumValues = enumType.GetEnumValues();
            var exportTypeLength = "export type".Length + enumTypeName.Length + enumValues.Length + 3;
            if (exportTypeLength < MaxLineLength)
            {
                sb.AppendLine($"{NamespaceIndent}export type {enumTypeName} = {enumValues};");
            }
            else
            {
                sb.AppendLine($"{NamespaceIndent}export type {enumTypeName} =");
                var enums = enumValues.Split('|');
                sb.Append($"{NamespaceIndent}{TabSpace}| ");
                sb.Append(string.Join(Environment.NewLine + NamespaceIndent + TabSpace + "| ", enums.Select(@enum => @enum.Trim())));
                sb.AppendLine(";");
            }
        }

        /// <summary>
        /// Appends either:
        ///     export interface ClassName [extends BaseClassName] {}>;
        /// or:
        ///     export interface ClassName [extends BaseClassName] {
        ///     propertyName1?: propertType1
        ///     propertyName2?: propertType2
        ///     propertyName3?: propertType3
        ///     }
        /// </summary>
        /// <param name="class">entity or complex type</param>
        private void AddEntityOrComplexType(OdcmClass @class)
        {
            var propCount = @class.Properties.Count;
            var entityTypeName = @class.Name.UpperCaseFirstChar();
            if (propCount == 0 && entityTypeName[0] == 'I')
            {
                sb.AppendLine("// tslint:disable-next-line: interface-name no-empty-interface");
            }
            else if (entityTypeName[0] == 'I')
            {
                sb.AppendLine("// tslint:disable-next-line: interface-name");
            }
            else if (propCount == 0)
            {
                sb.AppendLine("// tslint:disable-next-line: no-empty-interface");
            }

            var extendsStatement = @class.Base == null
                ? string.Empty
                : $" extends {GetFullyQualifiedTypeScriptTypeName(@class.Base.GetTypeString(), @class.Base.Namespace.GetNamespaceName())}";
            var exportInterfaceLine = NamespaceIndent + "export interface " + entityTypeName + extendsStatement + " {";
            if (propCount == 0)
            {
                sb.AppendLine(exportInterfaceLine + "}");
            }
            else
            {
                sb.AppendLine(exportInterfaceLine);
                @class.Properties.ForEach(prop => AddProperties(prop));
                sb.AppendLine(NamespaceIndent + "}");
            }
        }

        /// <summary>
        /// Adds a property line inside an entity or complex type object
        /// </summary>
        /// <param name="prop">property</param>
        private void AddProperties(OdcmProperty prop)
        {
            if (prop.LongDescription != null || prop.Description != null)
            {
                List<string> multiLineDescriptions = SplitString(prop.GetSanitizedLongDescription());
                if (multiLineDescriptions.Count() == 1)
                {
                    sb.AppendLine($"{NamespaceIndent}{TabSpace}// {multiLineDescriptions.First()}");
                }
                else
                {
                    sb.AppendLine($"{NamespaceIndent}{TabSpace}/**");
                    foreach (var descriptionLine in multiLineDescriptions)
                    {
                        sb.AppendLine($"{NamespaceIndent}{TabSpace} * {descriptionLine}");
                    }

                    sb.AppendLine($"{NamespaceIndent}{TabSpace} */");
                }
            }

            sb.AppendLine($"{NamespaceIndent}{TabSpace}{prop.Name}?: {GetFullyQualifiedTypeScriptTypeName(prop.GetTypeString(), prop.Projection.Type.Namespace.GetNamespaceName())};");
        }

        /// <summary>
        /// Takes a string and splits into multiple lines based on MaxLineLength
        /// </summary>
        /// <param name="description">description to be divided into multiple lines</param>
        /// <returns>a list of lines containing description</returns>
        private static List<string> SplitString(string description)
        {
            if (description == null)
            {
                return new List<string>();
            }

            // Removing non-breaking space in a string
            var words = description.Replace("\u00A0", " ")
                .Split(' ')
                .Where(word => word != string.Empty);

            var sb = new StringBuilder();
            List<string> multiLineDescription = new List<string>();
            foreach (var word in words)
            {
                if (sb.Length + word.Length < MaxLineLength)
                {
                    sb.Append(word + " ");
                }
                else // reached line limit
                {
                    multiLineDescription.Add(sb.ToString().Trim());
                    sb = new StringBuilder(word + " ");
                }
            }

            if (sb.Length > 0)
            {
                multiLineDescription.Add(sb.ToString().Trim());
            }

            return multiLineDescription;
        }

        /// <summary>
        /// Gets either fully qualified or plain type name for a type
        /// </summary>
        /// <param name="type">plain type name</param>
        /// <param name="namespace">namespace that type belongs to</param>
        /// <returns>fully qualified or plain type name</returns>
        private string GetFullyQualifiedTypeScriptTypeName(string type, string @namespace)
        {
            if (@namespace == NamespaceName || @namespace == "Edm")
            {
                return type;
            }

            // replace Microsoft.Graph with microsoftgraph
            return @namespace.Replace(MainNamespaceName, TypeScriptMainNamespaceName) + "." + type;
        }
    }
}
