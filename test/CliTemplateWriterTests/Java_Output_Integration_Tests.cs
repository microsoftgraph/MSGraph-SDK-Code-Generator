using System.IO;
using Vipr;
using Xunit;

namespace CliTemplateWriterTests
{
    public class Java_Output_Integration_Tests
    {
        [Fact]
        public void When_passing_specific_Arguments_should_procces_ms_graph()
        {
            var args = string.Format("Metadata{0}microsoftgraph.xml --writer=T4TemplateWriter", Path.DirectorySeparatorChar).Split(' ');
            Program.Main(args);
        }
        
        [Fact]
        public void When_passing_specific_Arguments_should_procces_one_note_metadata()
        {
            var args = string.Format("Metadata{0}OneNote.edmx.xml --writer=T4TemplateWriter", Path.DirectorySeparatorChar).Split(' ');
            Program.Main(args);
        }

        //TODO:Verify Primary Namespace Settings
        [Fact]
        public void When_passing_specific_Arguments_should_procces_one_drive_consumer_metadata()
        {
            var args = string.Format("Metadata{0}OneDriveConsumer.xml --writer=T4TemplateWriter", Path.DirectorySeparatorChar).Split(' ');
            Program.Main(args);
        }

        [Fact]
        public void When_passing_specific_Arguments_should_procces_exchange_metadata()
        {
            var args = string.Format("Metadata{0}Exchange.edmx.xml --writer=T4TemplateWriter", Path.DirectorySeparatorChar).Split(' ');
            Program.Main(args);
        }

        [Fact]
        public void When_passing_specific_Arguments_should_procces_discovery_metadata()
        {
            var args = string.Format("Metadata{0}discovery.xml --writer=T4TemplateWriter", Path.DirectorySeparatorChar).Split(' ');
            Program.Main(args);
        }

        //TODO:Verify Primary Namespace Settings
        [Fact]
        public void When_passing_specific_Arguments_should_procces_files_metadata()
        {
            var args = string.Format("Metadata{0}files.xml --writer=T4TemplateWriter", Path.DirectorySeparatorChar).Split(' ');
            Program.Main(args);
        }

    }
}
