namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    using System.Collections.Generic;

    public interface ITemplateInfoProvider
    {
        /// <summary>
        /// Creates an IEnumerable of templates.
        /// </summary>
        /// <returns> an IEnumerable of templates.</returns>
        IEnumerable<ITemplateInfo> Templates();
    }
}
