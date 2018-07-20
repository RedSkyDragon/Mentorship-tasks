using System.Collections.Generic;

namespace IncomeAndExpenses.DataAccessInterface
{
    public interface IRepository<TId, T> 
        where T : Entity<TId>
    {
        IEnumerable<T> GetAll();

        T Get(TId id);

        void Create(T item);

        void Update(T item);

        void Delete(TId id);
    }
}
