
namespace Microsoft.Graph.ODataTemplateWriter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Inflector;
    using Vipr.Core.CodeModel;

    public static class OdcmNamespaceExtensions
    {
        private static readonly Inflector inflector = new Inflector(CultureInfo.GetCultureInfo("en-US"));
        public static string GetNamespaceName(this OdcmNamespace namespaceObject)
        {
            var nameSpaceName = ReservedNameSpaces.Contains(namespaceObject.Name) ? $"{namespaceObject.Name}Namespace" : namespaceObject.Name;
            return inflector.Titleize(nameSpaceName).Replace(" ", "");
        }
        public static string GetTypeString(this OdcmType type)
        {
            return type.Name.GetTypeString();
        }
        public static string GetTypeString(this string type)
        {
            switch (type.ToLowerInvariant())
            {
                case "string":
                case "double":
                    return type.ToLowerFirstChar();
                case "binary":
                    return "byte[]";
                case "boolean":
                case "bool":
                    return "bool";
                case "date":
                    return "Date";
                case "json":
                    return "System.Text.Json.JsonDocument";
                default:
                    return type.ToCheckedCase();
            }
        }
        private static HashSet<string> ReservedNameSpaces = 
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Microsoft.Graph.Security",
            "Microsoft.Graph.IdentityGovernance",
            "Microsoft.Graph.DeviceManagement"
        };
    }
}
