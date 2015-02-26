using TemplateWriter;
using Vipr.CLI;
using Vipr.CLI.Configuration;
using Xunit;

namespace CliTemplateWriterTests
{
    public class Java_Output_Integration_Tests
    {
        [Fact]
        public void When_passing_specific_Arguments_should_procces_one_note_metadata()
        {
            var args = "--language=java --inputFile=Metadata\\OneNote.edmx.xml --outputDir=Out".Split(' ');
            var builder = new ConfigurationBuilder().WithConfiguration(new OneNoteConfiguration())
                                                    .WithArguments(args);
            var entrypoint = new CLIEntryPoint(new TemplateProcessorManager(), builder);
            entrypoint.Process();
        }

        [Fact]
        public void When_passing_specific_Arguments_should_procces_exchange_metadata()
        {
            var args = "--language=java --inputFile=Metadata\\Exchange.edmx.xml --outputDir=Out".Split(' ');
            var builder = new ConfigurationBuilder().WithConfiguration(new ExchangeConfiguration())
                                                    .WithArguments(args);
            var entrypoint = new CLIEntryPoint(new TemplateProcessorManager(), builder);
            entrypoint.Process();
        }

        [Fact]
        public void When_passing_specific_Arguments_should_procces_discovery_metadata()
        {
            var args = "--language=java --inputFile=Metadata\\discovery.xml --outputDir=Out".Split(' ');
            var builder = new ConfigurationBuilder().WithConfiguration(new DisoveryConfiguration())
                                                    .WithArguments(args);
            var entrypoint = new CLIEntryPoint(new TemplateProcessorManager(), builder);
            entrypoint.Process();
        }

        //[Fact]
        //public void When_passing_specific_Arguments_should_procces_directory_metadata()
        //{
        //    var args = "--language=java --inputFile=Metadata\\aad_graph_v15_augmented_v4.xml --outputDir=Out".Split(' ');
        //    var builder = new ConfigurationBuilder().WithConfiguration(new DirectoryConfiguration())
        //                                            .WithArguments(args);
        //    var entrypoint = new CLIEntryPoint(builder, new TemplateProcessorManager());
        //    entrypoint.Process();
        //}

        [Fact]
        public void When_passing_specific_Arguments_should_procces_files_metadata()
        {
            var args = "--language=java --inputFile=Metadata\\files.xml --outputDir=Out".Split(' ');
            var builder = new ConfigurationBuilder().WithConfiguration(new FilesConfiguration())
                                                    .WithArguments(args);
            var entrypoint = new CLIEntryPoint(new TemplateProcessorManager(), builder);
            entrypoint.Process();
        }

    }
}
