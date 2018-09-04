using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// DAL interface for users.
    /// </summary>
    public interface IUsersDAL
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>List of users</returns>
        Task<IEnumerable<User>> GetUsers();

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>The user.</returns>
        Task<User> GetUser(Guid id);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        Task UpdateUser(User user);

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        Task DeleteUser(Guid id);

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        Task CreateUser(User user);
    }
}
