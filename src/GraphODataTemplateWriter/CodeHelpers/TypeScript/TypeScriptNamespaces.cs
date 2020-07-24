using System.Collections.Generic;

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.TypeScript
{
    /// <summary>
    /// Data structure to hold printable namespaces
    /// </summary>
    public class TypeScriptNamespaces
    {
        /// <summary>
        /// Main printable namespace, i.e. Microsoft.Graph
        /// </summary>
        public TypeScriptNamespace MainNamespace;

        /// <summary>
        /// Mapping between a namespace name and its printable namespace
        /// e.g. Microsoft.Graph.CallRecords
        /// </summary>
        public Dictionary<string, TypeScriptNamespace> SubNamespaces;
    }
}
