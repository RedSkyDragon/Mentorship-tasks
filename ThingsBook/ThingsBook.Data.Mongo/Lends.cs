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

        public Task CreateLend(Guid userId, Guid thingId, Lend lend)
        {
            var update = Builders<Thing>.Update.Set(t => t.Lend, lend);
            return _db.Things.UpdateOneAsync(t => t.UserId == userId && t.Id == thingId, update);
        }

        public Task DeleteFriendLends(Guid userId, Guid friendId)
        {
            var update = Builders<Thing>.Update.Set(t => t.Lend, null);
            return _db.Things.UpdateManyAsync(t => t.UserId == userId && t.Lend.FriendId == friendId, update);
        }

        public Task DeleteLend(Guid userId, Guid thingId)
        {
            var update = Builders<Thing>.Update.Set(t => t.Lend, null);
            return _db.Things.UpdateOneAsync(t => t.UserId == userId && t.Id == thingId, update);
        }

        public async Task<Lend> GetLend(Guid userId, Guid thingId)
        {
            var result = await _db.Things.FindAsync(t => t.UserId == userId && t.Id == thingId);
            return result.FirstOrDefault().Lend;
        }

        public Task UpdateLend(Guid userId, Guid thingId, Lend lend)
        {
            var update = Builders<Thing>.Update
                .Set(t => t.Lend.FriendId, lend.FriendId)
                .Set(t => t.Lend.LendDate, lend.LendDate)
                .Set(t => t.Lend.Comment, lend.Comment);
            return _db.Things.UpdateOneAsync(t => t.UserId == userId && t.Id == thingId, update);
        }
    }
}
