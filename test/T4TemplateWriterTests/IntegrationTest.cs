// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace T4TemplateWriterTests {
    class IntegrationTest {
        private Vipr.Bootstrapper bootstrapper;

        private String inputFile  = @"http://services.odata.org/V4/OData/OData.svc/$metadata";
        private String readerName = @"--reader=Vipr.Reader.OData.v4";
        private String writerName = @"--writer=Vipr.T4TemplateWriter";
        private String outputPath = @"--outputPath=.\output";

        public IntegrationTest() {

             bootstrapper = new Vipr.Bootstrapper();
        }

        public void Run(Boolean interactive) {
            bootstrapper.Start(new String[] { inputFile, readerName, writerName, outputPath });
            if (interactive) Console.ReadKey();
        }
    }
}
