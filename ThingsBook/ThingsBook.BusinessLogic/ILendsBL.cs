using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;

namespace ThingsBook.BusinessLogic
{
    /// <summary>
    /// Interface of lends business logic
    /// </summary>
    public interface ILendsBL
    {
        /// <summary>
        /// Creates the lend for specified thing identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="lend">The lend.</param>
        /// <returns>Thing with created lend.</returns>
        Task<Thing> Create(Guid userId, Guid thingId, Lend lend);

        /// <summary>
        /// Deletes the specified lend record and creates history record.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="returnDate">The return date.</param>
        /// <returns></returns>
        Task Delete(Guid userId, Guid thingId, DateTime returnDate);

        /// <summary>
        /// Updates the lend for specified thing identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <param name="lend">The lend.</param>
        /// <returns>Updated thing with lend</returns>
        Task<Thing> Update(Guid userId, Guid thingId, Lend lend);

        /// <summary>
        /// Gets the historical lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The lend identifier.</param>
        /// <returns>Historacal record</returns>
        Task<HistLend> GetHistoricalLend(Guid userId, Guid id);

        /// <summary>
        /// Gets all historical lends for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of historical lends</returns>
        Task<IEnumerable<HistLend>> GetHistoricalLends(Guid userId);

        /// <summary>
        /// Deletes the historical lend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The lend identifier.</param>
        /// <returns></returns>
        Task DeleteHistoricalLend(Guid userId, Guid id);
    }
}
