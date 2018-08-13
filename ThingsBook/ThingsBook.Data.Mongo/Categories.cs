using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    public class CategoriesBL : ICategoriesBL
    {
        private ThingsBookContext _db;

        public CategoriesBL(ThingsBookContext db)
        {
            _db = db;
        }

        public void CreateCategory(Guid userId, Category category)
        {
            var filter = Builders<User>.Filter.Where(u => u.Id == userId);
            var update = Builders<User>.Update.Push(u => u.Categories, category);
            _db.Users.FindOneAndUpdate(filter, update);
        }

        public void DeleteCategory(Guid userId, Guid id)
        {
            var filter = Builders<User>.Filter.Where(u => u.Id == userId);
            var catFilter = Builders<Category>.Filter.Where(c => c.Id == id);
            var update = Builders<User>.Update.PullFilter(u => u.Categories, catFilter);
            _db.Users.FindOneAndUpdate(filter, update);
        }

        public IEnumerable<Category> GetCategories(Guid userId)
        {
            var filter = Builders<User>.Filter.Where(u => u.Id == userId);
            return _db.Users.Find(filter).FirstOrDefault().Categories;
        }

        public Category GetCategory(Guid userId, Guid id)
        {
            var filter = Builders<User>.Filter.Where(u => u.Id == userId); ;
            return _db.Users.Find(filter).FirstOrDefault().Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public void UpdateCategory(Guid userId, Category category)
        {
            var filter = Builders<User>.Filter.Where(u => u.Id == userId && u.Categories.Any(cat => cat.Id == category.Id));
            var update = Builders<User>.Update.Set(u => u.Categories.ElementAt(-1).Name, category.Name)
                .Set(u => u.Categories.ElementAt(-1).About, category.About);
            _db.Users.FindOneAndUpdate(filter, update);
        }
    }
}
