using System;
using System.Collections.Generic;

namespace ThingsBook.Data.Interface
{
    public interface IFriendsBL
    {
        IEnumerable<Friend> GetFriends(Guid userId);

        Friend GetFriend(Guid userId, Guid id);

        void UpdateFriend(Guid userId, Friend friend);

        void DeleteFriend(Guid userId, Guid id);

        void CreateFriend(Guid userId, Friend friend);

    }
}
