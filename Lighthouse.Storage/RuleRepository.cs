using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Lighthouse.Storage.Models;

namespace Lighthouse.Storage
{
    public interface IRuleRepository
    {
        AnnotationRule Create(AnnotationRule rule);
        AnnotationRule Find(long id);
        AnnotationRule UpdateRule(long ruleId, string annotation, string query, string description);
        IList<AnnotationRule> FindByPanel(string panel);
        IQueryable<AnnotationRule> FindAll();
        int Count();
        void ValidateRule(long ruleId);
        void ArchiveRule(long ruleId);
    }

    public class RuleRepository : IRuleRepository, IDisposable
    {
        private readonly LighthouseContext _db;

        public RuleRepository(LighthouseContext db)
        {
            _db = db;
        }

        public IList<AnnotationRule> FindByPanel(string panel)
        {
            panel = panel.ToLower();
            return _db.AnnotationRules.Where(r => r.Panel.Equals(panel) && r.IsValidated && !r.IsArchived).ToList();
        }

        public IQueryable<AnnotationRule> FindAll()
        {
            return _db.AnnotationRules;
        }

        public int Count()
        {
            return _db.AnnotationRules.Count();
        }

        public AnnotationRule Create(AnnotationRule rule)
        {
            if (string.IsNullOrEmpty(rule.Panel) || string.IsNullOrEmpty(rule.Annotation) ||
                string.IsNullOrEmpty(rule.AnnotationType) || string.IsNullOrEmpty(rule.Name) ||
                string.IsNullOrEmpty(rule.Query))
                throw new Exception("Create rule failed: must include panel, annotation, annotation type, rule name, and query.");

            var newRule = new AnnotationRule()
            {
                Annotation = rule.Annotation,
                AnnotationType = rule.AnnotationType,
                CreatedUtc = DateTime.UtcNow,
                Description = rule.Description,
                Name = rule.Name,
                Panel = rule.Panel.ToLower(),
                Query = rule.Query,
                IsValidated = false,
                IsArchived = false
            };

            _db.AnnotationRules.Add(newRule);
            _db.SaveChanges();

            return newRule;
        }

        public void ValidateRule(long ruleId)
        {
            var rule = Find(ruleId);
            if(rule == null)
                throw new Exception("Rule does not exist, so cannot be validated.");
            if(rule.IsArchived)
                throw new Exception("Rule is archived and cannot be validated.");

            rule.IsValidated = true;
            rule.ValidatedUtc = DateTime.UtcNow;
            _db.SaveChanges();
        }

        public void ArchiveRule(long ruleId)
        {
            var rule = Find(ruleId);
            if (rule == null)
                throw new Exception("Rule does not exist, so cannot be validated.");

            rule.IsArchived = true;
            rule.ArchivedUtc = DateTime.UtcNow;
            _db.SaveChanges();
        }

        public AnnotationRule UpdateRule(long ruleId, string annotation, string query, string description)
        {
            var rule = Find(ruleId);
            if (rule == null)
                throw new Exception("Rule does not exist, so cannot be updated.");

            if(rule.IsValidated)
                throw new Exception("Rule is validated and cannot be updated.");

            if (rule.IsArchived)
                throw new Exception("Rule is archived and cannot be updated.");

            if (!string.IsNullOrEmpty(annotation))
            {
                rule.Annotation = annotation;
                _db.SaveChanges();
            }
            if (!string.IsNullOrEmpty(query))
            {
                rule.Query = query;
                _db.SaveChanges();
            }
            if (!string.IsNullOrEmpty(description))
            {
                rule.Description = description;
                _db.SaveChanges();
            }

            return rule;
        }

        public AnnotationRule Find(long id)
        {
            var rule = _db.AnnotationRules.Find(id);
            return rule;
        }
        
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
