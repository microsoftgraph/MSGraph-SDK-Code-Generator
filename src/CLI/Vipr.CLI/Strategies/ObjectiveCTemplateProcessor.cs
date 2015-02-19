using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateWriter;
using Vipr.CLI.Output;
using Vipr.Core.CodeModel;

namespace Vipr.CLI.Strategies
{
    public class ObjectiveCTemplateProcessor : BaseTemplateProcessor
    {
        public ObjectiveCTemplateProcessor(IFileWriter fileWriter, OdcmModel model, string baseFilePath)
            : base(fileWriter, model, baseFilePath)
        {
            StrategyName = "ObjectiveC";
            Templates.Add("Models", Models);
            Templates.Add("Protocols", Protocols);
            Templates.Add("ODataEntities", Models);
        }

        private void Models(Template template)
        {
            var enums = Model.GetEntityTypes();
            ProcessingAction(enums, template);
        }

        private void Protocols(Template template)
        {
            var enums = Model.GetEntityTypes();
            ProcessingAction(enums, template);
        }
    }
}
