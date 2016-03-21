using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vipr.T4TemplateWriter.TemplateProcessor
{
  
    public interface ITemplateInfoProvider
    {
        /// <summary>
        /// Creates an IEnumerable of templates.
        /// </summary>
        /// <returns> an IEnumerable of templates.</returns>
        IEnumerable<ITemplateInfo> Templates();
    }
}
