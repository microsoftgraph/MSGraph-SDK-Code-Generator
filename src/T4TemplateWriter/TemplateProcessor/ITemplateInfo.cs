using System;
namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    public enum TemplateType
    {
        Model,
        Request,
        Client,
        Other
    }
    interface ITemplateInfo
    {
        string Id { get; }
        string TemplateLanguage { get; set; }
        string TemplateName { get; set; }

    }
}
