using IncomeAndExpenses.Models;

namespace IncomeAndExpenses.Web.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private InAndExDbContext _db = new InAndExDbContext();
        private bool _disposed = false;

        public UnitOfWork()
        {
            Users = new UserRepository(_db);
        }

        public UserRepository Users { get; }

        public void Save()
        {
            _db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}