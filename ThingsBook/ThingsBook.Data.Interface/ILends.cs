using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public interface ILends
    {
        Task<Lend> GetLend(Guid thingId);

        Task<IEnumerable<Lend>> GetFriendLends(Guid userId, Guid friendId);

        Task UpdateLend(Guid thingId, Lend lend);

        Task DeleteLend(Guid thingId);

        Task DeleteFriendLends(Guid friendId);

        Task CreateLend(Guid thingId, Lend lend);
    }
}
