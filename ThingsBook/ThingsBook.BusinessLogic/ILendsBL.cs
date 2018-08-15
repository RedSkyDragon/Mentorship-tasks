using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public interface ILendsBL
    {
        Task Create(Guid thingId, Lend lend);

        Task Delete(Guid thingId, DateTime returnDate);

        Task Update(Guid thingId, Lend lend);

        Task<HistoricalLend> GetHistoricalLend(Guid id);

        Task<IEnumerable<HistoricalLend>> GetHistoricalLends(Guid userId);

        Task<FilteredLends> GetThingLends(Guid userId, Guid thingId);

        Task DeleteHistoricalLend(Guid id);
    }
}
