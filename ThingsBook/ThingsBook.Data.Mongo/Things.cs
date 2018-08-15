using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CreateThing(Thing thing)
        {
            await _db.Things.InsertOneAsync(thing);
        }

        public async Task DeleteThing(Guid id)
        {
            await _db.Things.DeleteOneAsync(t => t.Id == id);
        }

        public async Task DeleteThings(Guid userId)
        {
            await _db.Things.DeleteManyAsync(t => t.UserId == userId);
        }

        public async Task DeleteThingsForCategory(Guid categoryId)
        {
            await _db.Things.DeleteManyAsync(t => t.CategoryId == categoryId);
        }

        public async Task<Thing> GetThing(Guid id)
        {
            var result = await _db.Things.FindAsync(t => t.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<Thing> GetThingForLend(Guid lendId)
        {
            var result = await _db.Things.FindAsync(t => t.Lend.Id == lendId);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Thing>> GetThings(Guid userId)
        {
            var result = await _db.Things.FindAsync(t => t.UserId == userId);
            return result.ToEnumerable();
        }

        public async Task<IEnumerable<Thing>> GetThingsForCategory(Guid categoryId)
        {
            var result = await _db.Things.FindAsync(t => t.CategoryId == categoryId);
            return result.ToEnumerable();
        }

        public async Task UpdateThing(Thing thing)
        {
            var update = Builders<Thing>.Update
                .Set(t => t.Name, thing.Name)
                .Set(t => t.About, thing.About)
                .Set(t => t.CategoryId, thing.CategoryId)
                .Set(t => t.UserId, thing.UserId);  
            await _db.Things.UpdateOneAsync(t => t.Id == thing.Id, update);
        }
    }
}
