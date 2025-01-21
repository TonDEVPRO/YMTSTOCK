using System;
using System.Data.Entity;

namespace RfqEbr.Data.Core
{
    public class CustomCore : IDisposable
    {
        public DbContext Db;

        public CustomCore()
        {
            Db = new DbContext(ContextCore.ConnectionString);
        }

        public CustomCore(DbContext context)
        {
            Db = context;
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Db != null)
                {
                    Db.Dispose();
                }
            }
        }

        #endregion
    }
}