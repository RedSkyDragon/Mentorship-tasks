using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public interface IHistory
    {
        Task<IEnumerable<HistoricalLend>> GetHistLends(Guid userId);

        Task<IEnumerable<HistoricalLend>> GetFriendHistLends(Guid userId, Guid friendId);

        Task<IEnumerable<HistoricalLend>> GetThingHistLends(Guid userId, Guid thingId);

        Task<HistoricalLend> GetHistLend(Guid userId, Guid id);

        Task UpdateHistLend(Guid userId, HistoricalLend lend);

        Task DeleteHistLend(Guid userId, Guid id);

        Task DeleteUserHistory(Guid userId);

        Task DeleteFriendHistory(Guid userId, Guid friendId);

        Task DeleteThingHistory(Guid userId, Guid thingId);

        Task CreateHistLend(Guid userId, HistoricalLend lend);
    }
}
