using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    /// <summary>
    /// Interface of friends Business Logic
    /// </summary>
    public interface IFriendsBL
    {
        /// <summary>
        /// Creates the friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friend">The friend.</param>
        /// <returns>Created friend.</returns>
        Task<Friend> Create(Guid userId, Friend friend);

        /// <summary>
        /// Updates the friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friend">The friend.</param>
        /// <returns>Updated friend.</returns>
        Task<Friend> Update(Guid userId, Friend friend);

        /// <summary>
        /// Deletes the friend by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The friend identifier.</param>
        /// <returns></returns>
        Task Delete(Guid userId, Guid id);

        /// <summary>
        /// Gets the friend by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The friend identifier.</param>
        /// <returns>Requested friend.</returns>
        Task<Friend> GetOne(Guid userId, Guid id);

        /// <summary>
        /// Gets all friends for user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List if friends.</returns>
        Task<IEnumerable<Friend>> GetAll(Guid userId);

        /// <summary>
        /// Gets the friend lends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>Filtered lends with active and history records.</returns>
        Task<FilteredLends> GetFriendLends(Guid userId, Guid friendId);
    }
}
