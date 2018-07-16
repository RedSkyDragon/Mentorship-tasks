using System;

namespace IncomeAndExpenses.Models
{
    public interface IUnitOfWork: IDisposable
    {
        void Save();
    }
}
