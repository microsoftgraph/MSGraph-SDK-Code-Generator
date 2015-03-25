using T4TemplateWriter.Output;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter.Strategies
{
    public class JavaTemplateProcessor : BaseTemplateProcessor
    {
        public JavaTemplateProcessor(IPathWriter pathWriter, OdcmModel model, string baseFilePath) : base(pathWriter, model, baseFilePath)
        {
            StrategyName = "Java";
        }
    }
}