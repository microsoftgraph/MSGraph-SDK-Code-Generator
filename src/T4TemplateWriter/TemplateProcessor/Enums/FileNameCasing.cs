namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    using System;

    public enum FileNameCasing
    {
        UpperCamel, /* FooBarBaz */
        LowerCamel, /* fooBarBaz */
        Snake,      /* foo_bar_baz */
    }
}
