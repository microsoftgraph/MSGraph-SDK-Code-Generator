using System;
using Moq;
using TemplateWriter;
using Vipr.CLI;
using Vipr.CLI.Configuration;
using Xunit;

namespace CliTemplateWriterTests
{
    public class Given_a_Set_of_Arguments_to_CLI
    {
        [Fact]
        public void When_the_CLI_receives_a_set_of_arguments()
        {
            var configArguments = new Mock<IConfigArguments>();
            var configBuilder = new Mock<IConfigurationBuilder>();
            var processorManager = new Mock<ITemplateProcessorManager>();

            configBuilder.Setup(x => x.WithArguments(It.IsAny<string>()));
            configBuilder.Setup(x => x.WithJsonConfig());
            configBuilder.Setup(x => x.Build()).Returns(configArguments.Object);

            var entryPoint = new CLIEntryPoint(processorManager.Object,configBuilder.Object);
            Assert.NotNull(entryPoint);
        }

        [Fact]
        public void When_the_CLI_has_arguments_should_call_processor()
        {
            var configBuilder = new Mock<IConfigurationBuilder>();
            var processorManager = new Mock<ITemplateProcessorManager>();
            var configArguments = new Mock<IConfigArguments>();

            configBuilder.Setup(x => x.Build())
                         .Returns(configArguments.Object);
            processorManager.Setup(x => x.Process(configArguments.Object));

            var entryPoint = new CLIEntryPoint(processorManager.Object, configBuilder.Object);
            entryPoint.Process();

            configBuilder.VerifyAll();
            processorManager.VerifyAll();
        }

        [Fact]
        public void When_the_CLI_has_no_arguments_should_throw_exception()
        {
            var configBuilder = new Mock<IConfigurationBuilder>();
            var processorManager = new Mock<ITemplateProcessorManager>();
            var configArguments = new Mock<IConfigArguments>();

            configBuilder.Setup(x => x.Build());
            processorManager.Setup(x => x.Process(configArguments.Object));

            var entryPoint = new CLIEntryPoint(processorManager.Object, configBuilder.Object);
            Assert.Throws<InvalidOperationException>(() => entryPoint.Process());
        }


        [Fact]
        public void When_passing_specific_Arguments_should_procces_templates_objc()
        {
            var args = "--language=objectivec --inputFile=Metadata\\Exchange.edmx.xml --outputDir=Out".Split(' ');
            var builder = new ConfigurationBuilder().WithArguments(args);
            var entrypoint = new CLIEntryPoint(new TemplateProcessorManager(), builder);
            entrypoint.Process();
        }

		[Fact]
		public void When_passing_specific_Arguments_should_procces_files_templates_objc()
		{
			var args = "--language=objectivec --inputFile=Metadata\\files.xml --outputDir=Out".Split(' ');
			var builder = new ConfigurationBuilder().WithConfiguration(new FilesConfiguration()).WithArguments(args);
            var entrypoint = new CLIEntryPoint(new TemplateProcessorManager(), builder);
			entrypoint.Process();
		}
    }
}
