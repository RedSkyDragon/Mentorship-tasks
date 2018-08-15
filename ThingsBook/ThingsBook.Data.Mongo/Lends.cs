using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;
using System.Collections.Generic;

namespace ThingsBook.Data.Mongo
{
    public class Lends : ILends
    {
        private ThingsBookContext _db;

        public Lends(ThingsBookContext db)
        {
            _db = db;
        }

        public async Task CreateLend(Guid thingId, Lend lend)
        {
            var update = Builders<Thing>.Update.Set(t => t.Lend, lend);
            await _db.Things.UpdateOneAsync(t => t.Id == thingId, update);
        }

        public async Task DeleteFriendLends(Guid friendId)
        {
            var update = Builders<Thing>.Update.Set(t => t.Lend, null);
            await _db.Things.UpdateManyAsync(t => t.Lend.FriendId == friendId, update);
        }

        public async Task DeleteLend(Guid thingId)
        {
            var update = Builders<Thing>.Update.Set(t => t.Lend, null);
            await _db.Things.UpdateOneAsync(t => t.Id == thingId, update);
        }

        public async Task<IEnumerable<Lend>> GetFriendLends(Guid userId, Guid friendId)
        {
            var lends = new List<Lend>();
            var things = (await _db.Things.FindAsync(t => t.UserId == userId && t.Lend.FriendId == friendId)).ToEnumerable();
            foreach (var thing in things)
            {
                lends.Add(thing.Lend);
            }
            return lends;
        }

        public async Task<Lend> GetLend(Guid thingId)
        {
            var result = await _db.Things.FindAsync(t => t.Id == thingId);
            return result.FirstOrDefault().Lend;
        }

        public async Task UpdateLend(Guid thingId, Lend lend)
        {
            var update = Builders<Thing>.Update
                .Set(t => t.Lend.FriendId, lend.FriendId)
                .Set(t => t.Lend.LendDate, lend.LendDate)
                .Set(t => t.Lend.Comment, lend.Comment);
            await _db.Things.UpdateOneAsync(t => t.Id == thingId, update);
        }
    }
}
