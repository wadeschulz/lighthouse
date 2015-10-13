using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lighthouse.Storage;
using Lighthouse.Storage.Models;
using NUnit.Framework;

namespace Lighthouse.Tests.Fixtures
{
    /// <summary>
    /// Test for database creation and rule repository functions
    /// </summary>
    [TestFixture]
    public class RuleRepoTests
    {
        private LighthouseContext _db;
        private IRuleRepository _rules;

        [TestFixtureSetUp]
        public void Init()
        {
            Database.Delete("LighthouseContext"); // Verify that database does not exist
            var initializer = new LighthouseInitializer(); // Create and initialize new database
            _db = new LighthouseContext();
            initializer.InitializeDatabase(_db);
            Assert.IsNotNull(_db, "Database creation failed."); // Fail tests if database does not exist
            _rules = new RuleRepository(_db);
            Assert.IsNotNull(_rules, "Rule repository could not be created.");
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            Database.Delete("LighthouseContext"); // Delete database on test completion
        }

        [Test]
        public void RepoCountMatchesDb()
        {
            Assert.AreEqual(_db.AnnotationRules.Count(), _rules.Count());
        }

        [Test]
        public void RequireFieldsForRuleCreation()
        {
            var count = _rules.Count();
            Assert.AreEqual(_db.AnnotationRules.Count(), count);

            var noAnnotation = new AnnotationRule()
            {
                AnnotationType = "case",
                Name = "ASXL1 Positive",
                Panel = "yale/amlmds",
                Query = "WHERE GENE = 'ASXL1'"
            };
            Assert.Throws(typeof (Exception), delegate { _rules.Create(noAnnotation); });
            Assert.AreEqual(count, _rules.Count());

            var noAnnType = new AnnotationRule()
            {
                Annotation = "ASXL1 gene mutations are associated with...",
                Name = "ASXL1 Positive",
                Panel = "yale/amlmds",
                Query = "WHERE GENE = 'ASXL1'"
            };
            Assert.Throws(typeof (Exception), delegate { _rules.Create(noAnnType); });
            Assert.AreEqual(count, _rules.Count());

            var noName = new AnnotationRule()
            {
                Annotation = "ASXL1 gene mutations are associated with...",
                AnnotationType = "case",
                Panel = "yale/amlmds",
                Query = "WHERE GENE = 'ASXL1'"
            };
            Assert.Throws(typeof (Exception), delegate { _rules.Create(noName); });
            Assert.AreEqual(count, _rules.Count());

            var noPanel = new AnnotationRule()
            {
                Annotation = "ASXL1 gene mutations are associated with...",
                AnnotationType = "case",
                Name = "ASXL1 Positive",
                Query = "WHERE GENE = 'ASXL1'"
            };
            Assert.Throws(typeof (Exception), delegate { _rules.Create(noPanel); });
            Assert.AreEqual(count, _rules.Count());

            var noQuery = new AnnotationRule()
            {
                Annotation = "ASXL1 gene mutations are associated with...",
                AnnotationType = "case",
                Name = "ASXL1 Positive",
                Panel = "aml/mds"
            };
            Assert.Throws(typeof (Exception), delegate { _rules.Create(noQuery); });
            Assert.AreEqual(count, _rules.Count());

            var valid = new AnnotationRule()
            {
                Annotation = "ASXL1 gene mutations are associated with...",
                AnnotationType = "case",
                Name = "ASXL1 Positive",
                Panel = "aml/mds",
                Query = "WHERE GENE = 'ASXL1'"
            };
            AnnotationRule created = null;
            Assert.DoesNotThrow(delegate { created = _rules.Create(valid); });

            Assert.AreEqual(count + 1, _rules.Count());
            Assert.IsNotNull(created);
        }

        [Test]
        public void CreateRule()
        {
            var count = _rules.Count();
            Assert.AreEqual(_db.AnnotationRules.Count(), count);

            var createdUtc = DateTime.UtcNow.AddDays(-1);

            const string annotation = "ASXL1 gene mutations are associated with...";
            const string annType = "case";
            const string description = "Presence of mutation in ASXL1";
            const string name = "ASXL1 Positive";
            const string panel = "yale/amlmds";
            const string query = "WHERE GENE = 'ASXL1'";

            // Create new rule, set validated=true to verify that create method sets to false (can only validate through Validate())
            // Created date should also be set by method, not calling function, so verify that date differs
            var valid = new AnnotationRule()
            {
                Annotation = annotation,
                AnnotationType = annType,
                CreatedUtc = createdUtc,
                Description = description,
                Name = name,
                Panel = panel,
                Query = query,
                IsValidated = true
            };

            var created = _rules.Create(valid);
            Assert.AreNotEqual(0, created.Id);
            Assert.AreEqual(count + 1, _rules.Count());
            Assert.AreEqual(annotation, created.Annotation);
            Assert.AreEqual(annType, created.AnnotationType);
            Assert.AreEqual(description, created.Description);
            Assert.AreEqual(name, created.Name);
            Assert.AreEqual(panel, created.Panel);
            Assert.AreEqual(query, created.Query);
            Assert.IsFalse(created.IsValidated);
            Assert.AreNotEqual(valid.CreatedUtc, created.CreatedUtc);
        }

        [Test]
        public void NullIfNotExists()
        {
            Assert.IsNull(_rules.Find(-1));
        }

        [Test]
        public void CanValidateTest()
        {
            const string annotation = "ASXL1 gene mutations are associated with...";
            const string annType = "case";
            const string description = "Presence of mutation in ASXL1";
            const string name = "ASXL1 Positive";
            const string panel = "yale/amlmds";
            const string query = "WHERE GENE = 'ASXL1'";
            
            var valid = new AnnotationRule()
            {
                Annotation = annotation,
                AnnotationType = annType,
                Description = description,
                Name = name,
                Panel = panel,
                Query = query
            };

            var created = _rules.Create(valid);
            Assert.IsFalse(created.IsValidated);
            Assert.IsFalse(created.IsArchived);

            _rules.ValidateRule(created.Id);
            var verify = _rules.Find(created.Id);
            Assert.IsTrue(verify.IsValidated);
            Assert.IsFalse(created.IsArchived);
        }

        [Test]
        public void CanArchiveTest()
        {
            const string annotation = "ASXL1 gene mutations are associated with...";
            const string annType = "case";
            const string description = "Presence of mutation in ASXL1";
            const string name = "ASXL1 Positive";
            const string panel = "yale/amlmds";
            const string query = "WHERE GENE = 'ASXL1'";
            
            var valid = new AnnotationRule()
            {
                Annotation = annotation,
                AnnotationType = annType,
                Description = description,
                Name = name,
                Panel = panel,
                Query = query
            };

            var created = _rules.Create(valid);
            Assert.IsFalse(created.IsValidated);
            Assert.IsFalse(created.IsArchived);

            _rules.ArchiveRule(created.Id);
            var verify = _rules.Find(created.Id);
            Assert.IsFalse(verify.IsValidated);
            Assert.IsTrue(created.IsArchived);
        }

        [Test]
        public void ArchivePreventsValidation()
        {
            const string annotation = "ASXL1 gene mutations are associated with...";
            const string annType = "case";
            const string description = "Presence of mutation in ASXL1";
            const string name = "ASXL1 Positive";
            const string panel = "yale/amlmds";
            const string query = "WHERE GENE = 'ASXL1'";
            
            var valid = new AnnotationRule()
            {
                Annotation = annotation,
                AnnotationType = annType,
                Description = description,
                Name = name,
                Panel = panel,
                Query = query
            };

            var created = _rules.Create(valid);
            Assert.IsFalse(created.IsValidated);
            Assert.IsFalse(created.IsArchived);

            _rules.ArchiveRule(created.Id);

            Assert.Throws(typeof(Exception), delegate { _rules.ValidateRule(created.Id); });
            var verify = _rules.Find(created.Id);
            Assert.IsFalse(verify.IsValidated);
            Assert.IsTrue(created.IsArchived);
        }

        [Test]
        public void ArchiveValidateExceptWhenNull()
        {
            long id = 0;
            var lastRule = _db.AnnotationRules.OrderByDescending(r => r.Id).FirstOrDefault();
            if (lastRule != null && lastRule.Id > id)
                id = lastRule.Id + 1;

            Assert.Throws(typeof (Exception), delegate { _rules.ArchiveRule(id); });
            Assert.Throws(typeof(Exception), delegate { _rules.ValidateRule(id); });
        }

        [Test]
        public void ArchivedTestCannotBeListed()
        {
            const string annotation = "ASXL1 gene mutations are associated with...";
            const string annType = "case";
            const string description = "Presence of mutation in ASXL1";
            const string name = "ASXL1 Positive";
            const string panel = "yale/amlmds";
            const string query = "WHERE GENE = 'ASXL1'";
            
            var valid = new AnnotationRule()
            {
                Annotation = annotation,
                AnnotationType = annType,
                Description = description,
                Name = name,
                Panel = panel,
                Query = query
            };

            var created = _rules.Create(valid);
            _rules.ValidateRule(created.Id);

            var rules = _rules.FindByPanel("yale/amlmds");
            var count = rules.Count;
            Assert.IsTrue(rules.Any(r => r.Id == created.Id));

            _rules.ArchiveRule(created.Id);
            rules = _rules.FindByPanel("yale/amlmds");

            Assert.AreEqual(count-1, rules.Count);
            Assert.IsFalse(rules.Any(r => r.Id == created.Id));
        }

        [Test]
        public void CanUpdateRule()
        {
            const string annotation = "ASXL1 gene mutations are associated with...";
            const string annType = "case";
            const string description = "Presence of mutation in ASXL1";
            const string name = "ASXL1 Positive";
            const string panel = "yale/amlmds";
            const string query = "WHERE GENE = 'ASXL1'";

            var valid = new AnnotationRule()
            {
                Annotation = annotation,
                AnnotationType = annType,
                Description = description,
                Name = name,
                Panel = panel,
                Query = query
            };

            var created = _rules.Create(valid);
            
            const string uAnn = "ASXL1 gene variants are assocaited with";
            const string uQuery = "WHERE GENE = 'ASXL1' AND AF > 5";
            const string uDesc = "ASXL1 variant >5% AF";

            Assert.Throws(typeof (Exception), delegate { _rules.UpdateRule(created.Id + 1, null, null, null); });

            _rules.UpdateRule(created.Id, uAnn, uQuery, uDesc);
            var updated = _rules.Find(created.Id);
            Assert.AreEqual(uAnn, updated.Annotation);
            Assert.AreEqual(uQuery, updated.Query);
            Assert.AreEqual(uDesc, updated.Description);

            _rules.UpdateRule(created.Id, null, null, null);
            var nullUpdate = _rules.Find(created.Id);
            Assert.AreEqual(uAnn, nullUpdate.Annotation);
            Assert.AreEqual(uQuery, nullUpdate.Query);
            Assert.AreEqual(uDesc, nullUpdate.Description);
        }

        [Test]
        public void CannotUpdateValidatedRule()
        {
            const string annotation = "ASXL1 gene mutations are associated with...";
            const string annType = "case";
            const string description = "Presence of mutation in ASXL1";
            const string name = "ASXL1 Positive";
            const string panel = "yale/amlmds";
            const string query = "WHERE GENE = 'ASXL1'";

            var valid = new AnnotationRule()
            {
                Annotation = annotation,
                AnnotationType = annType,
                Description = description,
                Name = name,
                Panel = panel,
                Query = query
            };

            var created = _rules.Create(valid);
            _rules.ValidateRule(created.Id);

            const string uAnn = "ASXL1 gene variants are assocaited with";
            const string uQuery = "WHERE GENE = 'ASXL1' AND AF > 5";
            const string uDesc = "ASXL1 variant >5% AF";

            Assert.Throws(typeof(Exception), delegate { _rules.UpdateRule(created.Id, uAnn, uQuery, uDesc); });
        }

        [Test]
        public void CannotUpdateArchivedRule()
        {
            const string annotation = "ASXL1 gene mutations are associated with...";
            const string annType = "case";
            const string description = "Presence of mutation in ASXL1";
            const string name = "ASXL1 Positive";
            const string panel = "yale/amlmds";
            const string query = "WHERE GENE = 'ASXL1'";

            var valid = new AnnotationRule()
            {
                Annotation = annotation,
                AnnotationType = annType,
                Description = description,
                Name = name,
                Panel = panel,
                Query = query
            };

            var created = _rules.Create(valid);
            _rules.ArchiveRule(created.Id);

            const string uAnn = "ASXL1 gene variants are assocaited with";
            const string uQuery = "WHERE GENE = 'ASXL1' AND AF > 5";
            const string uDesc = "ASXL1 variant >5% AF";

            Assert.Throws(typeof(Exception), delegate { _rules.UpdateRule(created.Id, uAnn, uQuery, uDesc); });
        }
    }
}
