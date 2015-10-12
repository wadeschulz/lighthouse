using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lighthouse.Storage;
using NUnit.Framework;

namespace Lighthouse.Tests.Fixtures
{
    /// <summary>
    /// Test for database creation VCF data load
    /// </summary>
    [TestFixture]
    public class VcfLoadTests
    {
        private LighthouseContext _db;

        [TestFixtureSetUp]
        public void Init()
        {
            Database.Delete("LighthouseContext"); // Verify that database does not exist
            var initializer = new LighthouseInitializer(); // Create and initialize new database
            _db = new LighthouseContext();
            initializer.InitializeDatabase(_db);
            Assert.IsNotNull(_db, "Database creation failed."); // Fail tests if database does not exist
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            Database.Delete("LighthouseContext"); // Delete database on test completion
        }
    }
}
