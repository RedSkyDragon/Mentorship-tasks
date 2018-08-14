using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public interface IUsers
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUser(Guid id);

        Task UpdateUser(User user);

        Task DeleteUser(Guid id);

        Task CreateUser(User user);
    }
}
