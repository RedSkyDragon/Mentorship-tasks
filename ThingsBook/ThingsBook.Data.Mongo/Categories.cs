using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    public class Categories : ICategories
    {
        private ThingsBookContext _db;

        public Categories(ThingsBookContext db)
        {
            _db = db;
        }

        public void CreateCategory(Guid userId, Category category)
        {
            var update = Builders<User>.Update.Push(u => u.Categories, category);
            _db.Users.FindOneAndUpdate(u => u.Id == userId, update);
        }

        public void DeleteCategory(Guid userId, Guid id)
        {
            var filter = Builders<Category>.Filter.Where(c => c.Id == id);
            var update = Builders<User>.Update.PullFilter(u => u.Categories, filter);
            _db.Users.FindOneAndUpdate(u => u.Id == userId, update);
        }

        public IEnumerable<Category> GetCategories(Guid userId)
        {
            return _db.Users.Find(u => u.Id == userId).FirstOrDefault().Categories;
        }

        public Category GetCategory(Guid userId, Guid id)
        {
            return _db.Users.Find(u => u.Id == userId).FirstOrDefault().Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public void UpdateCategory(Guid userId, Category category)
        {
            var update = Builders<User>.Update.Set(u => u.Categories.ElementAt(-1).Name, category.Name)
                .Set(u => u.Categories.ElementAt(-1).About, category.About);
            _db.Users.FindOneAndUpdate(u => u.Id == userId && u.Categories.Any(cat => cat.Id == category.Id), update);
        }
    }
}
