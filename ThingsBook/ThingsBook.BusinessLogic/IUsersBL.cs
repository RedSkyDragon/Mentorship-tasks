using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public interface IUsersBL
    {
        Task<User> Create(User user);

        Task Delete(Guid id);

        Task<User> Get(Guid id);

        Task<IEnumerable<User>> GetAll();

        Task<User> CreateOrUpdate(User user);

        Task<User> Update(User user);
    }
}
