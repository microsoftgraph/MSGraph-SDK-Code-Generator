// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Java
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
