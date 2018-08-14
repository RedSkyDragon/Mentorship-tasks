using System;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public interface ILendsBL
    {
        Task Create(Guid thingId, Lend lend);

        Task Delete(Guid thingId);

        Task Update(Guid thingId, Lend lend);

        Task<HistoricalLend> GetHistoricalLend(Guid id);

        Task<HistoricalLend> GetHistoricalLends(Guid userId);

        Task<FilteredLends> GetFriendLends(Guid userId, Guid friendId);

        Task<FilteredLends> GetThingLends(Guid userId, Guid thingId);

        Task DeleteHistoricalLend(Guid id);
    }
}
