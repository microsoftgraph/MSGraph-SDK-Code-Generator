using System.Linq;

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.CSharp
{
    /// <summary>
    /// Contains the parameter details about an OData method.
    /// </summary>
    public class ParameterInfo
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string ParameterName { get; set; }
        public string PropertyName { get; set; }
        public bool IsNullable { get; set; }
    }
}
