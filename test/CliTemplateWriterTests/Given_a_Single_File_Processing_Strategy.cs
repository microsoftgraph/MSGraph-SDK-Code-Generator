using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TemplateWriter;
using Vipr.CLI;
using Vipr.Core;

namespace CliTemplateWriterTests
{
    [TestClass]
    public class Given_a_Single_File_Processing_Strategy
    {

        [TestMethod]
        public void When_Instantiated_should_have_a_valid_state()
        {
            var reader = new Mock<IReader>();
            var tempLocationWriter = new Mock<ITemplateTempLocationFileWriter>();
            var processorManager = new TemplateProcessorManager(reader.Object, tempLocationWriter.Object);

            Assert.IsNotNull(processorManager);
        }

        [TestMethod]
        public void Can_create_a_representation_of_templates_from_an_assembly()
        {
            var reader = new TemplateSourceReader();
            reader.Read(typeof(CustomHost), new BuilderArguments { Language = "Java" });
        }

        [TestMethod]
        public void Can_write_template_into_temp_location()
        {
            var reader = new Mock<ITemplateSourceReader>();
            var templateWriter = new TemplateTempLocationFileWriter(reader.Object);
            Assert.IsNotNull(templateWriter);
        }
    }
}