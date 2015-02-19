using TemplateWriter;

namespace Vipr.CLI
{
    public interface ITemplateProcessorManager
    {
        void Process(IConfigArguments configuration);
    }
}