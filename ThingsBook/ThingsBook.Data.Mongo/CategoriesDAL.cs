using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using ThingsBook.Data.Interface;
using System.Threading.Tasks;

namespace ThingsBook.Data.Mongo
{
    /// <summary>
    /// Mongo implementation of DAL interface for categories.
    /// </summary>
    /// <seealso cref="ThingsBook.Data.Interface.ICategoriesDAL" />
    public class CategoriesDAL : ICategoriesDAL
    {
        private ThingsBookContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesDAL"/> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        public CategoriesDAL(ThingsBookContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="category">The category.</param>
        public async Task CreateCategory(Guid userId, Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            if (userId != category.UserId)
            {
                throw new ArgumentException("Param userId must be equal to category.UserId.", nameof(userId));
            }
            await _db.Categories.InsertOneAsync(category);
        }

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The category identifier.</param>
        public Task DeleteCategory(Guid userId, Guid id)
        {
            return _db.Categories.DeleteOneAsync(c => c.UserId == userId && c.Id == id);
        }

        /// <summary>
        /// Deletes all categories for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public Task DeleteCategories(Guid userId)
        {
            return _db.Categories.DeleteManyAsync(c => c.UserId == userId);
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of categories.
        /// </returns>
        public async Task<IEnumerable<Category>> GetCategories(Guid userId)
        {
            var result = await _db.Categories.FindAsync(c => c.UserId == userId);
            return await result.ToListAsync();
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The category identifier.</param>
        /// <returns>
        /// The category.
        /// </returns>
        public async Task<Category> GetCategory(Guid userId, Guid id)
        {
            var result = await _db.Categories.FindAsync(c => c.UserId == userId && c.Id == id);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="category">The category.</param>
        public Task UpdateCategory(Guid userId, Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            if (userId != category.UserId)
            {
                throw new ArgumentException("Param userId must be equal to category.UserId.", nameof(userId));
            }
            var update = Builders<Category>.Update
                .Set(c => c.Name, category.Name)
                .Set(c => c.About, category.About);
            return _db.Categories.UpdateOneAsync(c => c.UserId == userId && c.Id == category.Id, update);
        }
    }
}
