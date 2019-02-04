using System;
using System.Threading.Tasks;
using TabHelper.Data.ORM;

namespace TabHelper.Data.Transaction
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext db;

        private bool disposed = false;

        public UnitOfWork(AppDbContext db)
        {
            this.db = db;
        }

        public async Task Commit()
        {
            await db.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Limpa a transição e zera o garbage collection
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

