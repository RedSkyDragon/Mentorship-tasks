using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public interface IFriendsBL
    {
        Task Create(Friend friend);

        Task Update(Friend friend);

        Task Delete(Friend friend);

        Task<Friend> GetOne(Guid id);

        Task<IEnumerable<Friend>> GetAll(Guid userId);
    }
}
