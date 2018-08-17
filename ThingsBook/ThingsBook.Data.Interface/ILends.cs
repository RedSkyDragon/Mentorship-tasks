using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public interface ILends
    {
        Task<Lend> GetLend(Guid userId, Guid thingId);

        Task UpdateLend(Guid userId, Guid thingId, Lend lend);

        Task DeleteLend(Guid userId, Guid thingId);

        Task DeleteFriendLends(Guid userId, Guid friendId);

        Task CreateLend(Guid userId, Guid thingId, Lend lend);
    }
}
