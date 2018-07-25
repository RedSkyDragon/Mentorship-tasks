using System;

namespace IncomeAndExpenses.DataAccessInterface
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<TId, T> Repository<TId, T>() 
            where T: Entity<TId>;
        IRepository<int, T> Repository<T>()
            where T : Entity<int>;
        void Save();
    }
}
