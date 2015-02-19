namespace Vipr.CLI.Output
{
    public interface IFileWriter
    {
        void WriteText(Template template, string odcmObject, string output);

        void CreateDirectory(string directoryPath);

        bool DirectoryExists(string directoryPath);

		string FileExtension { get; }
    }
}