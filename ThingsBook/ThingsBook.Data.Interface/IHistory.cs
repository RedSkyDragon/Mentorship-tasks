using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// DAL interface for history.
    /// </summary>
    public interface IHistory
    {
        /// <summary>
        /// Gets all historical lends of user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of historical lends.</returns>
        Task<IEnumerable<HistoricalLend>> GetHistLends(Guid userId);

        /// <summary>
        /// Gets the friend historical lends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>Dictionary with lend as key and lended thing as value.</returns>
        Task<IDictionary<HistoricalLend, Thing>> GetFriendHistLends(Guid userId, Guid friendId);

        /// <summary>
        /// Gets the thing historical lends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <returns>Dictionary with lend as key and friend as value.</returns>
        Task<IDictionary<HistoricalLend, Friend>> GetThingHistLends(Guid userId, Guid thingId);

        /// <summary>
        /// Gets the historical lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The historical lend identifier.</param>
        /// <returns>Historical lend</returns>
        Task<HistoricalLend> GetHistLend(Guid userId, Guid id);

        /// <summary>
        /// Updates the historical lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="lend">The lend.</param>
        Task UpdateHistLend(Guid userId, HistoricalLend lend);

        /// <summary>
        /// Deletes the historical lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The lend identifier.</param>
        Task DeleteHistLend(Guid userId, Guid id);

        /// <summary>
        /// Deletes the user history.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        Task DeleteUserHistory(Guid userId);

        /// <summary>
        /// Deletes the friend history.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        Task DeleteFriendHistory(Guid userId, Guid friendId);

        /// <summary>
        /// Deletes the thing history.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        Task DeleteThingHistory(Guid userId, Guid thingId);

        /// <summary>
        /// Creates the historical lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="lend">The lend.</param>
        Task CreateHistLend(Guid userId, HistoricalLend lend);
    }
}
