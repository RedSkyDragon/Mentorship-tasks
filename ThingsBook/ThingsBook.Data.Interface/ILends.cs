using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// DAL interface for lends.
    /// </summary>
    public interface ILends
    {
        /// <summary>
        /// Updates the lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="lend">The lend.</param>
        Task UpdateLend(Guid userId, Guid thingId, Lend lend);

        /// <summary>
        /// Deletes the lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        Task DeleteLend(Guid userId, Guid thingId);

        /// <summary>
        /// Deletes the friend lends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        Task DeleteFriendLends(Guid userId, Guid friendId);

        /// <summary>
        /// Creates the lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="lend">The lend.</param>
        Task CreateLend(Guid userId, Guid thingId, Lend lend);
    }
}
