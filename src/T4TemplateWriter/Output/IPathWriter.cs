using T4TemplateWriter.Templates;

namespace T4TemplateWriter.Output
{
    public interface IPathWriter
    {
        string WritePath(Template template, string odcmObject);

        void CreateDirectory(string directoryPath);

        bool DirectoryExists(string directoryPath);

        string FileExtension { get; }
    }
}