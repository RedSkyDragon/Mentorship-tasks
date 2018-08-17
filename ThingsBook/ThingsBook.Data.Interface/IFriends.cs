using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public interface IFriends
    {
        Task<IEnumerable<Friend>> GetFriends(Guid userId);

        Task<Friend> GetFriend(Guid userId, Guid id);

        Task UpdateFriend(Guid userId, Friend friend);

        Task DeleteFriend(Guid userId, Guid id);

        Task DeleteFriends(Guid userId);

        Task CreateFriend(Guid userId, Friend friend);
    }
}
