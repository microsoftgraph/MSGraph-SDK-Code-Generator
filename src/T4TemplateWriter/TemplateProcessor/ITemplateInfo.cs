using System;
namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    interface ITemplateInfo
    {
        string Id { get; }
        string TemplateLanguage { get; set; }
        string TemplateName { get; set; }
        global::Vipr.T4TemplateWriter.TemplateProcessor.TemplateType TemplateType { get; set; }

    }

    public enum TemplateType
    {
        Base,
        Model,
        Fetchers,
        Other,
        Unknown
    }
}
