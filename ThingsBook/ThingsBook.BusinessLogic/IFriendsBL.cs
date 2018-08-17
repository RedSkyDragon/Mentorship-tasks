using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public interface IFriendsBL
    {
        Task<Friend> Create(Guid userId, Friend friend);

        Task<Friend> Update(Guid userId, Friend friend);

        Task Delete(Guid userId, Guid id);

        Task<Friend> GetOne(Guid userId, Guid id);

        Task<IEnumerable<Friend>> GetAll(Guid userId);

        Task<FilteredLends> GetFriendLends(Guid userId, Guid friendId);
    }
}
