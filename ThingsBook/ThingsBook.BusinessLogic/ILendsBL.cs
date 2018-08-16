using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public interface ILendsBL
    {
        Task Create(Guid userId, Guid thingId, Lend lend);

        Task Delete(Guid userId, Guid thingId, DateTime returnDate);

        Task Update(Guid userId, Guid thingId, Lend lend);

        Task<HistLend> GetHistoricalLend(Guid userId, Guid id);

        Task<IEnumerable<HistLend>> GetHistoricalLends(Guid userId);

        Task DeleteHistoricalLend(Guid userId, Guid id);
    }
}
