using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    public class Things : IThings
    {
        private ThingsBookContext _db;

        public Things(ThingsBookContext db)
        {
            _db = db;
        }

        public void CreateThing(Guid userId, Guid categoryId, Thing thing)
        {
            var update = Builders<User>.Update.AddToSet(u => u.Categories.ElementAt(-1).Things, thing);
            _db.Users.FindOneAndUpdate(u => u.Id == userId && u.Categories.Any(cat => cat.Id == categoryId), update);
        }

        public void DeleteThing(Guid userId, Guid categoryId, Guid id)
        {
            var update = Builders<User>.Update.PullFilter(u => u.Categories.ElementAt(-1).Things, t => t.Id == id);
            _db.Users.FindOneAndUpdate(u => u.Id == userId && u.Categories.Any(cat => cat.Id == categoryId), update);
        }

        public Thing GetThing(Guid userId, Guid categoryId, Guid id)
        {
            return _db.Users.Find(u => u.Id == userId).FirstOrDefault().Categories.Where(c => c.Id == categoryId).FirstOrDefault().Things.Where(t => t.Id == id).FirstOrDefault();
        }

        public IEnumerable<Thing> GetThings(Guid userId)
        {
            var cats = _db.Users.Find(u => u.Id == userId).FirstOrDefault().Categories;
            IEnumerable<Thing> things = null;
            foreach (var cat in cats)
            {
                things = things?.Concat(cat.Things) ?? cat.Things;
            }
            return things;
        }

        public IEnumerable<Thing> GetThings(Guid userId, Guid categoryId)
        {
            return _db.Users.Find(u => u.Id == userId).FirstOrDefault().Categories.Where(c => c.Id == categoryId).FirstOrDefault().Things;
        }

        public void UpdateThing(Guid userId, Guid categoryId, Thing thing)
        {
            var update = Builders<User>.Update
                .Set("categories.$[cat].things.$[thing].name", thing.Name)
                .Set("categories.$[cat].things.$[thing].about", thing.About);
            var arrayFilters = new List<ArrayFilterDefinition>
            {
                new BsonDocumentArrayFilterDefinition<User>(new BsonDocument("cat._id", categoryId)),
                new BsonDocumentArrayFilterDefinition<User>(new BsonDocument("thing._id", thing.Id))
            };
            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };
            _db.Users.UpdateOne(u => u.Id == userId, update, options: updateOptions);
        }
    }
}
