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
            var update = Builders<User>.Update.Push(u => u.Categories.Where(c => c.Id == categoryId).FirstOrDefault().Things, thing);
            _db.Users.FindOneAndUpdate(u => u.Id == userId, update);
        }

        public void DeleteThing(Guid userId, Guid categoryId, Guid id)
        {
            var update = Builders<User>.Update.PullFilter(u => u.Categories.Where(c => c.Id == categoryId).FirstOrDefault().Things, t => t.Id == id);
            _db.Users.FindOneAndUpdate(u => u.Id == userId, update);
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
            var update = Builders<User>.Update.Set(u => u.Categories.ElementAt(-1).Things.ElementAt(-1).Name, thing.Name)
                .Set(u => u.Categories.ElementAt(-1).Things.ElementAt(-1).About, thing.About);
            _db.Users.FindOneAndUpdate(u => u.Id == userId && u.Categories.Any(cat => cat.Id == categoryId) && u.Categories.Any(cat => cat.Things.Any(t => t.Id == thing.Id)), update);
        }
    }
}
