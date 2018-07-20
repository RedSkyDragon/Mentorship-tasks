using IncomeAndExpenses.DataAccessInterface;
using System.Data.Entity;

namespace IncomeAndExpenses.DataAccessImplement
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _db;

        public UnitOfWork(DbContext db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IRepository<TId, T> Repository<TId, T>() 
            where T : Entity<TId>
        {
            return new Repository<TId, T>(_db);
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}