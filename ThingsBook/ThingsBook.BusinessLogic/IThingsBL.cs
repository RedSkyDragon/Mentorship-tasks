using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    /// <summary>
    /// Interface for things business logic
    /// </summary>
    public interface IThingsBL
    {
        /// <summary>
        /// Creates the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thing">The thing.</param>
        /// <returns>Created thing.</returns>
        Task<Thing> CreateThing(Guid userId, Thing thing);

        /// <summary>
        /// Updates the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thing">The thing.</param>
        /// <returns>Updated thing.</returns>
        Task<Thing> UpdateThing(Guid userId, Thing thing);

        /// <summary>
        /// Deletes the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The thing identifier.</param>
        /// <returns></returns>
        Task DeleteThing(Guid userId, Guid id);

        /// <summary>
        /// Gets the thing.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The thing identifier.</param>
        /// <returns>Requested thing.</returns>
        Task<Thing> GetThing(Guid userId, Guid id);

        /// <summary>
        /// Gets all things for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of things.</returns>
        Task<IEnumerable<Thing>> GetThings(Guid userId);

        /// <summary>
        /// Gets the things of requested category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>List of things.</returns>
        Task<IEnumerable<Thing>> GetThingsForCategory(Guid userId, Guid categoryId);

        /// <summary>
        /// Gets the thing active lend and lends history.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="thingId">The thing identifier.</param>
        /// <returns>Filtered lends</returns>
        Task<FilteredLends> GetThingLends(Guid userId, Guid thingId);

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="category">The category.</param>
        /// <returns>Created category.</returns>
        Task<Category> CreateCategory(Guid userId, Category category);

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="category">The category.</param>
        /// <returns>Updated category</returns>
        Task<Category> UpdateCategory(Guid userId, Category category);

        /// <summary>
        /// Deletes the category with things.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The category identifier.</param>
        /// <returns></returns>
        Task DeleteCategoryWithThings(Guid userId, Guid id);

        /// <summary>
        /// Deletes the category with replacement things category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="replacementId">The replacement category identifier.</param>
        /// <returns></returns>
        Task DeleteCategoryWithReplacement(Guid userId, Guid categoryId, Guid replacementId);

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The category identifier.</param>
        /// <returns>Requested category</returns>
        Task<Category> GetCategory(Guid userId, Guid id);

        /// <summary>
        /// Gets all categories of user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of categories</returns>
        Task<IEnumerable<Category>> GetCategories(Guid userId);
    }
}
