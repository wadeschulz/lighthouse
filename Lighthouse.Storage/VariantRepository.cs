using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lighthouse.Storage.Models;

namespace Lighthouse.Storage
{
    public interface IVariantRepository
    {
        void Create(IList<Variant> variantList);
        void Delete(string guid);
        IList<Variant> Query(string guid, string query);
    }

    public class VariantRepository : IVariantRepository, IDisposable
    {
        private readonly LighthouseContext _db;

        public VariantRepository(LighthouseContext db)
        {
            _db = db;
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

        public void Create(IList<Variant> variantList)
        {
            _db.Variants.AddRange(variantList);
            _db.SaveChanges();
        }

        public void Delete(string guid)
        {
            var variants = _db.Variants.Where(v => v.CaseGuid.Equals(guid)).ToList();
            foreach (var variant in variants)
            {
                _db.Variants.Remove(variant);
            }
            _db.SaveChanges();
        }

        public IList<Variant> Query(string guid, string query)
        {
            var result = _db.Variants.SqlQuery(string.Format("SELECT Id FROM dbo.Variants WHERE guid = {0} AND ({1})", guid, query));

            return result.ToList();
        }
    }
}
