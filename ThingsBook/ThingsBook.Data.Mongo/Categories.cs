using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using ThingsBook.Data.Interface;
using System.Threading.Tasks;

namespace ThingsBook.Data.Mongo
{
    public class Categories : ICategories
    {
        private ThingsBookContext _db;

        public Categories(ThingsBookContext db)
        {
            _db = db;
        }

        public async Task CreateCategory(Guid userId, Category category)
        {
            if (userId == category.UserId)
            {
                await _db.Categories.InsertOneAsync(category);
            }
        }

        public Task DeleteCategory(Guid userId, Guid id)
        {
            return _db.Categories.DeleteOneAsync(c => c.UserId == userId && c.Id == id);
        }

        public Task DeleteCategories(Guid userId)
        {
            return _db.Categories.DeleteManyAsync(c => c.UserId == userId);
        }

        public async Task<IEnumerable<Category>> GetCategories(Guid userId)
        {
            var result = await _db.Categories.FindAsync(c => c.UserId == userId);
            return result.ToEnumerable();
        }

        public async Task<Category> GetCategory(Guid userId, Guid id)
        {
            var result = await _db.Categories.FindAsync(c => c.UserId == userId && c.Id == id);
            return result.FirstOrDefault();
        }

        public Task UpdateCategory(Guid userId, Category category)
        {
            var update = Builders<Category>.Update
                .Set(c => c.Name, category.Name)
                .Set(c => c.About, category.About);
            return _db.Categories.UpdateOneAsync(c => c.UserId == userId && c.Id == category.Id, update);
        }
    }
}
