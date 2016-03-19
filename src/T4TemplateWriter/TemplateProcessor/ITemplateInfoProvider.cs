namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITemplateInfoProvider
    {
        /// <summary>
        /// Creates an IEnumerable of templates.
        /// </summary>
        /// <returns> an IEnumerable of templates.</returns>
        IEnumerable<ITemplateInfo> Templates();
    }
}
