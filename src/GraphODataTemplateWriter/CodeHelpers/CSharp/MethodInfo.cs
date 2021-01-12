using System.Linq;

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.CSharp
{
    /// <summary>
    /// Contains the OData method information used in method request builders.
    /// </summary>
    public class MethodInfo
    {
        /// <summary>
        /// Specifies the list of parameters in the method. Not used in the
        /// creation of request builders for the methods bound to the return 
        /// type of this method. 
        /// </summary>
        public IOrderedEnumerable<ParameterInfo> Parameters { get; set; }
        /// <summary>
        /// Specifies the parameter arguments to fill out the method constructor.
        /// </summary>
        public string ParametersAsArguments { get; set; }
        /// <summary>
        /// Specifies the parameter arguments to fill out the method signatures
        /// for the request builders that call the methods bound to the return 
        /// type of this method.
        /// </summary>
        public string ParamArgsForConstructor { get; set; }
        /// <summary>
        /// Contains the parameter doc comments for both the overload and 
        /// the methods bound to the return type of this method.
        /// </summary>
        public string ParameterComments { get; set; }
        /// <summary>
        /// Specifies the request builder type for the methods bound to the 
        /// return type of this method.
        /// </summary>
        public string RequestBuilderType { get; set; }
        /// <summary>
        /// Specifies the method name for the methods bound to the return
        /// type of this method. This is the same name as found in the CSDL.
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// Specifies the fully qualified method name for the methods bound to 
        /// the return type of this method. 
        /// </summary>
        public string MethodFullName { get; set; }
        /// <summary>
        /// Specifies the parameter arguments to fill out the method constructor
        /// for the methods bound to the return type of this method.
        /// </summary>
        public string MethodParametersAsArguments { get; set; }
    }
}
