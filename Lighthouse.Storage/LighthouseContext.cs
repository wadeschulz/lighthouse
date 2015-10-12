using System;
using System.Collections.Generic;
using System.Data.Entity;
using Lighthouse.Storage.Models;

namespace Lighthouse.Storage
{
    public class LighthouseContext : DbContext
    {
        public DbSet<Variant> Variants { get; set; }
        public DbSet<AnnotationRule> AnnotationRules { get; set; }
        public DbSet<AnnotationRuleVersion> AnnotationRuleVersions { get; set; }
    }

    public class LighthouseInitializer : CreateDatabaseIfNotExists<LighthouseContext>
    {
        protected override void Seed(LighthouseContext context)
        {
            var columns = new List<string> {"caseguid", "chr", "loc", "chgvs", "phgvs", "effect", "region"};

            foreach (var column in columns)
            {
                context.Database.ExecuteSqlCommand(string.Format("CREATE NONCLUSTERED INDEX IX_{0} ON {1} ([{0}] ASC)",
                    column, "Variants"));
            }

            context.Database.ExecuteSqlCommand(string.Format("CREATE NONCLUSTERED INDEX IX_{0} ON {1} ([{0}] DESC)",
                    "CreatedUtc", "AnnotationRuleVersions"));
        }
    }
}
