using System;
using System.Collections.Generic;
using MongoDB.Driver;
using ThingsBook.Data.Interface;
using System.Threading.Tasks;

namespace ThingsBook.Data.Mongo
{
    public class History : IHistory
    {
        private ThingsBookContext _db;

        public History(ThingsBookContext db)
        {
            _db = db;
        }

        public async Task CreateHistLend(HistoricalLend lend)
        {
            await _db.History.InsertOneAsync(lend);
        }

        public async Task DeleteFriendHistory(Guid friendId)
        {
            await _db.History.DeleteManyAsync(h => h.FriendId == friendId);
        }

        public async Task DeleteHistLend(Guid id)
        {
            await _db.History.DeleteOneAsync(h => h.Id == id);
        }

        public async Task DeleteThingHistory(Guid thingId)
        {
            await _db.History.DeleteManyAsync(h => h.ThingId == thingId);
        }

        public async Task DeleteUserHistory(Guid userId)
        {
            await _db.History.DeleteManyAsync(h => h.UserId == userId); 
        }

        public async Task<IEnumerable<HistoricalLend>> GetFriendHistLends(Guid friendId)
        {
            var result = await _db.History.FindAsync(h => h.FriendId == friendId);
            return result.ToEnumerable();
        }

        public async Task<HistoricalLend> GetHistLend(Guid id)
        {
            var result = await _db.History.FindAsync(h => h.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<HistoricalLend>> GetHistLends(Guid userId)
        {
            var result = await _db.History.FindAsync(h => h.UserId == userId);
            return result.ToEnumerable();
        }

        public async Task<IEnumerable<HistoricalLend>> GetThingHistLends(Guid thingId)
        {
            var result = await _db.History.FindAsync(h => h.ThingId == thingId);
            return result.ToEnumerable();
        }

        public async Task UpdateHistLend(HistoricalLend lend)
        {
            var update = Builders<HistoricalLend>.Update
                .Set(h => h.UserId, lend.UserId)
                .Set(h => h.FriendId, lend.FriendId)
                .Set(h => h.ThingId, lend.ThingId)
                .Set(h => h.LendDate, lend.LendDate)
                .Set(h => h.ReturnDate, lend.ReturnDate)
                .Set(h => h.Comment, lend.Comment);
            await _db.History.UpdateOneAsync(h => h.Id == lend.Id, update);
        }
    }
}
