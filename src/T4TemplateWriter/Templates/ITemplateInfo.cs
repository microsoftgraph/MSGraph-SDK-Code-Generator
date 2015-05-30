using System;
namespace Vipr.T4TemplateWriter {
    interface ITemplateInfo {
        string Id { get; }
        string TemplateLanguage { get; set; }
        string TemplateName { get; set; }
        global::Vipr.T4TemplateWriter.TemplateType TemplateType { get; set; }

    }

    public enum TemplateType {
        Base,
        Model,
        Fetcher,
        Other,
        Unknown
    }
}
