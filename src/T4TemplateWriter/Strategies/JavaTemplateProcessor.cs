using T4TemplateWriter.Output;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter.Strategies
{
    public class JavaTemplateProcessor : BaseTemplateProcessor
    {
        public JavaTemplateProcessor(IFileWriter fileWriter, OdcmModel model, string baseFilePath) : base(fileWriter, model, baseFilePath)
        {
            StrategyName = "Java";

        }
    }
}