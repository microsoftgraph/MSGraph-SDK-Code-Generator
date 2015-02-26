using TemplateWriter.Output;
using Vipr.Core.CodeModel;

namespace TemplateWriter.Strategies
{
    public class JavaTemplateProcessor : BaseTemplateProcessor
    {
        public JavaTemplateProcessor(IFileWriter fileWriter, OdcmModel model, string baseFilePath) : base(fileWriter, model, baseFilePath)
        {
            StrategyName = "Java";

        }
    }
}