using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public interface IFriends
    {
        Task<IEnumerable<Friend>> GetFriends(Guid userId);

        Task<Friend> GetFriend(Guid id);

        Task UpdateFriend(Friend friend);

        Task DeleteFriend(Guid id);

        Task CreateFriend(Friend friend);
    }
}
