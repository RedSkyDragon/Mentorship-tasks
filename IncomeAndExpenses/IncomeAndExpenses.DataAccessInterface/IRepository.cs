using System.Collections.Generic;
using System.Linq;

namespace IncomeAndExpenses.DataAccessInterface
{
    public interface IRepository<TId, T> 
        where T : Entity<TId>
    {
        IQueryable<T> GetAll();

        T Get(TId id);

        void Create(T item);

        void Update(T item);

        void Delete(TId id);
    }
}
