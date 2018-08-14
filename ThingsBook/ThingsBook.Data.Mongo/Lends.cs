using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    public class Lends : ILends
    {
        private ThingsBookContext _db;

        public Lends(ThingsBookContext db)
        {
            _db = db;
        }

        public void CreateLend(Guid userId, Guid categoryId, Guid thingId, Lend lend)
        {
            var update = Builders<User>.Update.Set("categories.$[cat].things.$[thing].lend", lend);
            var arrayFilters = new List<ArrayFilterDefinition>
            {
                new BsonDocumentArrayFilterDefinition<User>(new BsonDocument("cat._id", categoryId)),
                new BsonDocumentArrayFilterDefinition<User>(new BsonDocument("thing._id", thingId))
            };
            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };
            _db.Users.UpdateOne(u => u.Id == userId, update, options: updateOptions);
        }

        public void DeleteLend(Guid userId, Guid categoryId, Guid thingId)
        {
            var update = Builders<User>.Update.Set<Lend>("categories.$[cat].things.$[thing].lend", null);
            var arrayFilters = new List<ArrayFilterDefinition>
            {
                new BsonDocumentArrayFilterDefinition<User>(new BsonDocument("cat._id", categoryId)),
                new BsonDocumentArrayFilterDefinition<User>(new BsonDocument("thing._id", thingId))
            };
            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };
            _db.Users.UpdateOne(u => u.Id == userId, update, options: updateOptions);
        }

        public Lend GetLend(Guid userId, Guid categoryId, Guid thingId)
        {
            return _db.Users.Find(u => u.Id == userId).FirstOrDefault().Categories.Where(c => c.Id == categoryId).FirstOrDefault().Things.Where(t => t.Id == thingId).FirstOrDefault().Lend;
        }

        public IEnumerable<Lend> GetLends(Guid userId)
        {
            var cats = _db.Users.Find(u => u.Id == userId).FirstOrDefault().Categories;
            IEnumerable<Thing> things = null;
            foreach (var cat in cats)
            {
                things = things?.Concat(cat.Things) ?? cat.Things;
            }
            var lends = new List<Lend>();
            foreach (var thing in things)
            {
                lends.Add(thing.Lend);
            }
            return lends;
        }

        public void UpdateLend(Guid userId, Guid categoryId, Guid thingId, Lend lend)
        {
            var update = Builders<User>.Update.Set("categories.$[cat].things.$[thing].lend", lend);
            var arrayFilters = new List<ArrayFilterDefinition>
            {
                new BsonDocumentArrayFilterDefinition<User>(new BsonDocument("cat._id", categoryId)),
                new BsonDocumentArrayFilterDefinition<User>(new BsonDocument("thing._id", thingId))
            };
            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };
            _db.Users.UpdateOne(u => u.Id == userId, update, options: updateOptions);
        }
    }
}
