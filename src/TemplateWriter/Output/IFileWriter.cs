using TemplateWriter.Templates;

namespace TemplateWriter.Output
{
    public interface IFileWriter
    {
        void WriteText(Template template, string odcmObject, string output);

        void CreateDirectory(string directoryPath);

        bool DirectoryExists(string directoryPath);

        string FileExtension { get; }
    }
}