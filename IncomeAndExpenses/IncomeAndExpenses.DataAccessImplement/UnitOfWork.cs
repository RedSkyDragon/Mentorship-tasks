using IncomeAndExpenses.DataAccessInterface;


namespace IncomeAndExpenses.DataAccessImplement
{
    public class UnitOfWork : IUnitOfWork
    {
        private InAndExDbContext _db;
        private bool _disposed = false;

        public UnitOfWork(InAndExDbContext db)
        {
            _db = db;
        }

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

        public IRepository<TId, T> Repository<TId, T>() 
            where T : Entity<TId>
        {
            return new Repository<TId, T>(_db);
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}