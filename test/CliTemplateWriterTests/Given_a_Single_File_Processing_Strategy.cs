using Moq;
using TemplateWriter;
using Vipr.Core;
using Xunit;

namespace CliTemplateWriterTests
{
    public class Given_a_Single_File_Processing_Strategy
    {

        [Fact]
        public void When_Instantiated_should_have_a_valid_state()
        {
            var reader = new Mock<IOdcmReader>();
            var tempLocationWriter = new Mock<ITemplateTempLocationFileWriter>();
            var processorManager = new TemplateProcessorManager(reader.Object, tempLocationWriter.Object);

            Assert.NotNull(processorManager);
        }

        [Fact]
        public void Can_create_a_representation_of_templates_from_an_assembly()
        {
            var reader = new TemplateSourceReader();
            reader.Read(typeof(CustomHost), new BuilderArguments { Language = "Java" });
        }

        [Fact]
        public void Can_write_template_into_temp_location()
        {
            var reader = new Mock<ITemplateSourceReader>();
            var templateWriter = new TemplateTempLocationFileWriter(reader.Object);
            Assert.NotNull(templateWriter);
        }
    }
}