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
        /// Creates a TemplateInfo for the given path to the template.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns>an ITemplateInfo object</returns>
        ITemplateInfo Create(string fullPath);
    }
}
