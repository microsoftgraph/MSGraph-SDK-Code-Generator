using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vipr;

namespace T4TemplateWriterTests {
    class IntegrationTest {
        private Vipr.Bootstrapper bootstrapper;

        private String inputFile  = @"http://services.odata.org/V4/OData/OData.svc/$metadata";
        private String readerName = @"--reader=Vipr.Reader.OData.v4";
        private String writerName = @"--writer=T4TemplateWriter";
        private String outputPath = @"--outputPath=.\output";

        public IntegrationTest() {

             bootstrapper = new Vipr.Bootstrapper();
        }

        public void Run() {
            bootstrapper.Start(new String[] { inputFile, readerName, writerName, outputPath });
            Console.ReadKey();
        }
    }
}
