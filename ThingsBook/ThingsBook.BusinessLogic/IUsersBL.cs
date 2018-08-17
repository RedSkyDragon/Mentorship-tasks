using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    /// <summary>
    /// Interface for users business logic.
    /// </summary>
    public interface IUsersBL
    {
        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Created user.</returns>
        Task<User> Create(User user);

        /// <summary>
        /// Deletes the specified by identifier user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns></returns>
        Task Delete(Guid id);

        /// <summary>
        /// Gets the specified user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>Requested user.</returns>
        Task<User> Get(Guid id);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>List of users.</returns>
        Task<IEnumerable<User>> GetAll();

        /// <summary>
        /// Creates or update the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Created or updated user.</returns>
        Task<User> CreateOrUpdate(User user);

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Updated user.</returns>
        Task<User> Update(User user);
    }
}
