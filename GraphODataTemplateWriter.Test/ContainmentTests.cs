using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Graph.ODataTemplateWriter.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vipr.Core;
using Vipr.Core.CodeModel;
using Vipr.Reader.OData.v4;

namespace GraphODataTemplateWriter.Test
{
    /// <summary>
    /// Test GetServiceCollectionNavigationPropertyForPropertyType method
    /// 
    /// We have revised the LINQ statement to support the following OData rules 
    /// regarding containment:
    /// 
    /// 1. If a navigation property specifies "ContainsTarget='true'", it is self-contained. 
    ///    Generate a direct path to the item (ie "parent/child").
    /// 2. If a navigation property does not specify ContainsTarget but there is a defined EntitySet 
    ///    of the given type, it is a reference relationship. Generate a reference path to the item (ie "item/$ref").
    /// 3. If a navigation property does not have a defined EntitySet but there is a Singleton which has 
    ///    a self-contained reference to the given type, we can make a relationship to the implied EntitySet of 
    ///    the singleton. Generate a reference path to the item (ie "singleton/item/$ref").
    /// 4. If none of the above pertain to the navigation property, it should be treated as a metadata error.
    /// </summary>
    [TestClass]
    public class ContainmentTests
    {
        /// <summary>
        /// The object model of the test containment metadata
        /// </summary>
        public OdcmModel model;

        /// <summary>
        /// These tests load a test metadata file from the file system using VIPR's OData V4 reader
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            string dir = Directory.GetCurrentDirectory();
            dir = dir.Replace("\\bin\\Debug", "");

            string edmx = File.ReadAllText(dir + "\\Edmx\\Containment.xml");
            OdcmReader reader = new OdcmReader();

            model = reader.GenerateOdcmModel(new List<TextFile> { new TextFile("$metadata", edmx) });
        }

        /// <summary>
        /// Test an implicit entity set is found for a given navigation property without 
        /// containment or an explicit entity set
        /// </summary>
        [TestMethod]
        public void TestImplicitEntitySet()
        {
            var type = model.GetEntityTypes().Where(t => t.Name == "testEntity").First();
            var prop = type.Properties.Where(p => p.Name == "testNav").First();

            OdcmProperty result = OdcmModelExtensions.GetServiceCollectionNavigationPropertyForPropertyType(prop);
            var singleton = model.GetEntityTypes().Where(t => t.Name == "testSingleton").First();
            Assert.AreEqual(singleton.Name, result.Name);
        }

        /// <summary>
        /// Test that null is returned when there is no valid explicit or implicit entity set for a 
        /// given navigation property
        /// </summary>
        [TestMethod]
        public void TestNoValidEntitySet()
        {
            var type = model.GetEntityTypes().Where(t => t.Name == "testEntity").First();
            var prop = type.Properties.Where(p => p.Name == "testInvalidNav").First(); ;

            OdcmProperty result = OdcmModelExtensions.GetServiceCollectionNavigationPropertyForPropertyType(prop);
            Assert.IsNull(result);
        }

        /// <summary>
        /// Test that an explicit entity set is returned instead of an implicit entity set when both
        /// are present in the metadata
        /// </summary>
        [TestMethod]
        public void TestExplicitEntitySet()
        {
            var type = model.GetEntityTypes().Where(t => t.Name == "testEntity").First();
            var prop = type.Properties.Where(p => p.Name == "testExplicitNav").First();

            OdcmProperty result = OdcmModelExtensions.GetServiceCollectionNavigationPropertyForPropertyType(prop);
            
            var entitySet = model.EntityContainer.Properties.Where(t => t.Name == "testTypes").First();
            Assert.AreEqual(entitySet.Name, result.Name);
        }
    }
}
