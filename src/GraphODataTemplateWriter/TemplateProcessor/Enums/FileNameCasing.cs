// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.TemplateProcessor.Enums
{
    public enum FileNameCasing
    {
        UpperCamel, /* FooBarBaz */
        LowerCamel, /* fooBarBaz */
        Snake,      /* foo_bar_baz */
    }
}
