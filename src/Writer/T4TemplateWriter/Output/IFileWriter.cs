using T4TemplateWriter.Templates;

namespace T4TemplateWriter.Output
{
    public interface IFileWriter
    {
        void WriteText(Template template, string odcmObject, string output);

        void CreateDirectory(string directoryPath);

        bool DirectoryExists(string directoryPath);

        string FileExtension { get; }
    }
}