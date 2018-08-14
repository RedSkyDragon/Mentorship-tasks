using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public interface IHistory
    {
        Task<IEnumerable<HistoricalLend>> GetHistLends(Guid userId);

        Task<HistoricalLend> GetHistLend(Guid id);

        Task UpdateHistLend(HistoricalLend lend);

        Task DeleteHistLend(Guid id);

        Task CreateHistLend(HistoricalLend lend);
    }
}
