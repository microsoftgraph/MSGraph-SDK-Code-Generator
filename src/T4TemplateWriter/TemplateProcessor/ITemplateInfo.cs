using System;
using Vipr.Core.CodeModel;

namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    public enum TemplateType
    {
        Model,
        Request,
        Client,
        Shared,
        Other,
        Unknown
    }

    public interface ITemplateInfo
    {
        
        string Id { get; }
        string TemplateLanguage { get; set; }
        string TemplateName { get; set; }
        TemplateType TemplateType { get; set; }
        SubProcessorType SubprocessorType { get; set; }
        string TemplateDirectoryName { get; set; }
        string TemplateBaseName { get; set; }
        string FullPath { get; set; }
        string FileExtension { get; set; }

        bool ShouldIncludeObject(OdcmObject odcmObject);

        string BaseFileName(string className = "", string propertyName = "", string methodName = "");

    }
}
