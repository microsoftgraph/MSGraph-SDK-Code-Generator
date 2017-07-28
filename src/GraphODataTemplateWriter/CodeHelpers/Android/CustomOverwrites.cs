using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Android
{
    /**
     * This class contains a list of constants that define code snippets that should
     * be overwritten at generation time 
     * The field name should be equal to the class name you wish to edit
     * The dictionary value should contain two string, the first is the search value
     * and the latter is the replace value
     * This will be used in BaseModel.template.tt in the PostProcess() method
     */
    public class CustomOverwrites
    {
        public static Dictionary<string, string> BaseOnenotePageCollectionRequest = new Dictionary<string, string>() {
            {
                "public void post(final OnenotePage newOnenotePage, final ICallback<OnenotePage> callback) {",
                "public void post(final byte[] newOnenotePage, final ICallback<OnenotePage> callback) {"
            },
            {
                "public OnenotePage post(final OnenotePage newOnenotePage) throws ClientException {",
                "public OnenotePage post(final byte[] newOnenotePage) throws ClientException {"
            }
        };

        public static Dictionary<string, string> BaseOnenotePageRequest = new Dictionary<string, string>()
        {
            {
                "public void post(final OnenotePage newOnenotePage, final ICallback<OnenotePage> callback) {",
                "public void post(final byte[] newOnenotePage, final ICallback<OnenotePage> callback) {"
            },
            {
                "public OnenotePage post(final OnenotePage newOnenotePage) throws ClientException {",
                "public OnenotePage post(final byte[] newOnenotePage) throws ClientException {"
            }
        };

        public static Dictionary<string, string> IBaseOnenotePageCollectionRequest = new Dictionary<string, string>()
        {
            {
                "void post(final OnenotePage newOnenotePage, final ICallback<OnenotePage> callback);",
                "void post(final byte[] newOnenotePage, final ICallback<OnenotePage> callback);"
            },
            {
                "OnenotePage post(final OnenotePage newOnenotePage) throws ClientException;",
                "OnenotePage post(final byte[] newOnenotePage) throws ClientException;"
            }
        };

        public static Dictionary<string, string> IBaseOnenotePageRequest = new Dictionary<string, string>()
        {
            {
                "void post(final OnenotePage newOnenotePage, final ICallback<OnenotePage> callback);",
                "void post(final byte[] newOnenotePage, final ICallback<OnenotePage> callback);"
            },
            {
                "OnenotePage post(final OnenotePage newOnenotePage) throws ClientException;",
                "OnenotePage post(final byte[] newOnenotePage) throws ClientException;"
            }
        };
    }
}
