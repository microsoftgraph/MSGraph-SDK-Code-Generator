using System.Linq;

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.CSharp
{
    /// <summary>
    /// Contains the OData method information used in method request builders.
    /// </summary>
    public class MethodInfo
    {
        public IOrderedEnumerable<ParameterInfo> Parameters { get; set; }
        public string ParametersAsArguments { get; set; }
        public string ParameterComments { get; set; }
    }
}
