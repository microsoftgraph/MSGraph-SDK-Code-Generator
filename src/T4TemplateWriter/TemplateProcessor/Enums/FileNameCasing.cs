using System;

namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    public enum FileNameCasing
    {
        UpperCamel, /* FooBarBaz */
        LowerCamel, /* fooBarBaz */
        Snake,      /* foo_bar_baz */
    }
}
