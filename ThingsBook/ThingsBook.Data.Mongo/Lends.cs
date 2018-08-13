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
            var update = Builders<User>.Update.Set(u => u.Categories.Where(c => c.Id == categoryId).FirstOrDefault().Things.Where(t => t.Id == thingId).FirstOrDefault().Lend, lend);
            _db.Users.FindOneAndUpdate(u => u.Id == userId, update);
        }

        public void DeleteLend(Guid userId, Guid categoryId, Guid thingId)
        {
            var update = Builders<User>.Update.Set(u => u.Categories.Where(c => c.Id == categoryId).FirstOrDefault().Things.Where(t => t.Id == thingId).FirstOrDefault().Lend, null);
            _db.Users.FindOneAndUpdate(u => u.Id == userId, update);
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
            var update = Builders<User>.Update.Set(u => u.Categories.Where(c => c.Id == categoryId).FirstOrDefault().Things.Where(t => t.Id == thingId).FirstOrDefault().Lend, lend);
            _db.Users.FindOneAndUpdate(u => u.Id == userId, update);
        }
    }
}
