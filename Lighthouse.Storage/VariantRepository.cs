using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lighthouse.Storage
{
    public interface IVariantRepository
    {
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
    }
}
