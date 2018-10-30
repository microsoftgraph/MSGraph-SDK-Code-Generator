namespace Typewriter
{
    using ApiDoctor.Validation.OData;
    using ApiDoctor.Validation.Utility;
    using System.Linq;

    /// <summary>
    /// From https://github.com/OneDrive/apidoctor/blob/master/ApiDoctor.Publishing/CSDL/csdlextensionmethods.cs
    /// </summary>
    internal static class CsdlExtensionMethods
    {
        /// <summary>
        /// Merge two EntityFramework instances together into the first framework
        /// </summary>
        /// <param name="framework1"></param>
        /// <param name="framework2"></param>
        internal static EntityFramework MergeWith(this EntityFramework framework1, EntityFramework framework2)
        {
            ObjectGraphMerger<EntityFramework> merger = new ObjectGraphMerger<EntityFramework>(framework1, framework2);
            var edmx = merger.Merge();

            // Clean up bindingParameters on actions and methods to be consistently the same
            foreach (var schema in edmx.DataServices.Schemas)
            {
                foreach (var action in schema.Actions)
                {
                    foreach (var param in action.Parameters.Where(x => x.Name == "bindingParameter" || x.Name == "this"))
                    {
                        param.Name = "bindingParameter";
                        param.IsNullable = null;
                    }
                }
                foreach (var func in schema.Functions)
                {
                    foreach (var param in func.Parameters.Where(x => x.Name == "bindingParameter" || x.Name == "this"))
                    {
                        param.Name = "bindingParameter";
                        param.IsNullable = null;
                    }
                }
            }

            return edmx;
        }
    }
}
