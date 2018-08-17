using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// DAL interface for things.
    /// </summary>
    public interface IThings
    {
        /// <summary>
        /// Gets all things.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of things</returns>
        Task<IEnumerable<Thing>> GetThings(Guid userId);

        /// <summary>
        /// Gets the things with specified category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>List of things.</returns>
        Task<IEnumerable<Thing>> GetThingsForCategory(Guid userId, Guid categoryId);

        /// <summary>
        /// Gets the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The thing identifier.</param>
        /// <returns>The thing.</returns>
        Task<Thing> GetThing(Guid userId, Guid id);

        /// <summary>
        /// Gets the things lended by the specified friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friedId">The fried identifier.</param>
        /// <returns>List of things.</returns>
        Task<IEnumerable<Thing>> GetThingsForFriend(Guid userId, Guid friedId);

        /// <summary>
        /// Updates the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thing">The thing.</param>
        Task UpdateThing(Guid userId, Thing thing);

        /// <summary>
        /// Updates the things category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="replacementId">The replacement identifier.</param>
        Task UpdateThingsCategory(Guid userId, Guid categoryId, Guid replacementId);

        /// <summary>
        /// Deletes the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The thing identifier.</param>
        Task DeleteThing(Guid userId, Guid id);

        /// <summary>
        /// Deletes all things.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        Task DeleteThings(Guid userId);

        /// <summary>
        /// Deletes all things with specified category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        Task DeleteThingsForCategory(Guid userId, Guid categoryId);

        /// <summary>
        /// Creates the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thing">The thing.</param>
        Task CreateThing(Guid userId, Thing thing);
    }
}
