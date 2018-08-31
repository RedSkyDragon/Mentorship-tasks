using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// DAL interface for categories.
    /// </summary>
    public interface ICategoriesDAL
    {
        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of categories.</returns>
        Task<IEnumerable<Category>> GetCategories(Guid userId);

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The category identifier.</param>
        /// <returns>The category.</returns>
        Task<Category> GetCategory(Guid userId, Guid id);

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="category">The category.</param>
        Task UpdateCategory(Guid userId, Category category);

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The category identifier.</param>
        Task DeleteCategory(Guid userId, Guid id);

        /// <summary>
        /// Deletes all categories for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        Task DeleteCategories(Guid userId);

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="category">The category.</param>
        Task CreateCategory(Guid userId, Category category);

    }
}
