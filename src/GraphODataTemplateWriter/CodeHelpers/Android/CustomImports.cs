using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Android
{
    /**
     * This class contains a list of constants that define import statements that should be
     * added at generation time
     * The field name should be equal to the name of the class you wish to edit
     * This will be used in BaseModel.template.tt in the AddCustomImports() method
     */
    public class CustomImports
    {
        public const string UploadSession = "import com.microsoft.graph.concurrency.ChunkedUploadProvider;\r\n";
    }
}
