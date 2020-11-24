namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.CSharp
{
    /// <summary>
    /// Contains the navigation property information used in method request builders.
    /// </summary>
    public class NavigationPropertyInfo
    {
        /// <summary>
        /// Specifies the name of the interface which is the property type.
        /// </summary>
        public string ReturnInterfaceRequestBuilderName { get; set; }
        /// <summary>
        /// Specifies the name of the returned request builder.
        /// </summary>
        public string ReturnClassRequestBuilderName { get; set; }
        /// <summary>
        /// Specifies the path segment for the navigation property.
        /// </summary>
        public string Segment { get; set; }
        /// <summary>
        /// Specfies the name of the navigation property which is the 
        /// name of the request builder property.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Doc comments
        /// </summary>
        public string Description { get; set; }
    }
}
