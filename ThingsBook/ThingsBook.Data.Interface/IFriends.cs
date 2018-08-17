using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// DAL interface for friends.
    /// </summary>
    public interface IFriends
    {
        /// <summary>
        /// Gets the friends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of friends.</returns>
        Task<IEnumerable<Friend>> GetFriends(Guid userId);

        /// <summary>
        /// Gets the friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The friend identifier.</param>
        /// <returns>The friend.</returns>
        Task<Friend> GetFriend(Guid userId, Guid id);

        /// <summary>
        /// Updates the friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friend">The friend.</param>
        Task UpdateFriend(Guid userId, Friend friend);

        /// <summary>
        /// Deletes the friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The identifier.</param>
        Task DeleteFriend(Guid userId, Guid id);

        /// <summary>
        /// Deletes all friends for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        Task DeleteFriends(Guid userId);

        /// <summary>
        /// Creates the friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friend">The friend.</param>
        Task CreateFriend(Guid userId, Friend friend);
    }
}
