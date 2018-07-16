using IncomeAndExpenses.DataAccessInterface;
using System.Collections.Generic;

namespace IncomeAndExpenses.DataAccessImplement
{
    public class Repository<TId, T> : IRepository<TId, T> 
        where T: Entity<TId>
    {
        private InAndExDbContext _db;

        public Repository(InAndExDbContext db)
        {
            _db = db;
        }

        public void Create(T item)
        {
            _db.Set<T>().Add(item);
        }

        public void Delete(TId id)
        {
            T item = _db.Set<T>().Find(id);
            if (item != null)
            {
                _db.Set<T>().Remove(item);
            }
        }

        public T Get(TId id)
        {
            return _db.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>();
        }

        public void Update(T item)
        {
            var aitem = _db.Set<T>().Find(item.Id);
            if (item != null)
            {
                _db.Entry(item).CurrentValues.SetValues(item);
            }
        }
    }
}
