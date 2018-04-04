// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

using System.Collections.Generic;

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Android
{
    /**
     * This class contains a list of constants that define code snippets that should
     * be overwritten at generation time 
     * The field name should be equal to the name of the class you wish to edit
     * The dictionary value should contain two string, the first is the search value
     * and the latter is the replace value
     * This will be used in BaseModel.template.tt in the PostProcess() method
     */
    public class CustomOverwrites : Java.CustomOverwrites
    {
    }
}
