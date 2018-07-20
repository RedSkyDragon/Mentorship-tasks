using System;

namespace IncomeAndExpenses.DataAccessInterface
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<TId, T> Repository<TId, T>() 
            where T: Entity<TId>;
        void Save();
    }
}
