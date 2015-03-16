using Moq;
using T4TemplateWriter;
using T4TemplateWriter.Templates;
using Xunit;

namespace CliTemplateWriterTests
{
    public class Given_a_Single_File_Processing_Strategy
    {
        [Fact]
        public void When_Instantiated_should_have_a_valid_state()
        {
            var tempLocationWriter = new Mock<ITemplateTempLocationFileWriter>();
            var processorManager = new TemplateProcessorManager(tempLocationWriter.Object);

            Assert.NotNull(processorManager);
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