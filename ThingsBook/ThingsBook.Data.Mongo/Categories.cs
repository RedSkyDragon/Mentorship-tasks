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

        public async Task CreateCategory(Category category)
        {
            await _db.Categories.InsertOneAsync(category);
        }

        public async Task DeleteCategory(Guid id)
        {
            await _db.Categories.DeleteOneAsync(c => c.Id == id);
        }

        public async Task DeleteCategories(Guid userId)
        {
            await _db.Categories.DeleteManyAsync(c => c.UserId == userId);
        }

        public async Task<IEnumerable<Category>> GetCategories(Guid userId)
        {
            var result = await _db.Categories.FindAsync(c => c.UserId == userId);
            return result.ToEnumerable();
        }

        public async Task<Category> GetCategory(Guid id)
        {
            var result = await _db.Categories.FindAsync(c => c.Id == id);
            return result.FirstOrDefault();
        }

        public async Task UpdateCategory(Category category)
        {
            var update = Builders<Category>.Update
                .Set(c => c.Name, category.Name)
                .Set(c => c.About, category.About)
                .Set(c => c.UserId, category.UserId);
            await _db.Categories.UpdateOneAsync(c => c.Id == category.Id, update);
        }
    }
}
