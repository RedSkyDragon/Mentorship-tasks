using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CreateLend(Guid thingId, Lend lend)
        {
            var update = Builders<Thing>.Update.Set(t => t.Lend, lend);
            await _db.Things.UpdateOneAsync(t => t.Id == thingId, update);
        }

        public async Task DeleteLend(Guid thingId)
        {
            var update = Builders<Thing>.Update.Set(t => t.Lend, null);
            await _db.Things.UpdateOneAsync(t => t.Id == thingId, update);
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
