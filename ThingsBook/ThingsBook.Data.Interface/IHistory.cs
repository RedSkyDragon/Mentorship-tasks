using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public interface IHistory
    {
        Task<IEnumerable<HistoricalLend>> GetHistLends(Guid userId);

        Task<IEnumerable<HistoricalLend>> GetFriendHistLends(Guid friendId);

        Task<IEnumerable<HistoricalLend>> GetThingHistLends(Guid thingId);

        Task<HistoricalLend> GetHistLend(Guid id);

        Task UpdateHistLend(HistoricalLend lend);

        Task DeleteHistLend(Guid id);

        Task DeleteUserHistory(Guid userId);

        Task DeleteFriendHistory(Guid friendId);

        Task DeleteThingHistory(Guid thingId);

        Task CreateHistLend(HistoricalLend lend);
    }
}
