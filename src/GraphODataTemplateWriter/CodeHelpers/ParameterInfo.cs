using System.Linq;

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers
{
    /// <summary>
    /// Contains the parameter details about an OData method.
    /// </summary>
    public class ParameterInfo
    {
        public ParameterInfo(string type, string name, string parameterName, string propertyName, bool isNullable)
        {
            Type = type;
            Name = name;
            ParameterName = parameterName;
            PropertyName = propertyName;
            IsNullable = isNullable;

        }

        public string Type { get; set; }
        public string Name { get; set; }
        public string ParameterName { get; set; }
        public string PropertyName { get; set; }
        public bool IsNullable { get; set; }
    }
}
