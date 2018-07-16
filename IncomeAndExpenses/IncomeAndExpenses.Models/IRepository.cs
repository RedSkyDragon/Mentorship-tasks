using System.Collections.Generic;

namespace IncomeAndExpenses.Models
{
    public interface IRepository<TId, T> where T : class
    {
        IEnumerable<T> GetAll();

        T Get(TId id);

        void Create(T item);

        void Update(T item);

        void Delete(TId id);
    }
}
