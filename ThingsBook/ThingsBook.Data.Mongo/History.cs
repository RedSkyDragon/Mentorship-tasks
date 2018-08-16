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

        public async Task CreateHistLend(Guid userId, HistoricalLend lend)
        {
            if (userId == lend.UserId)
            {
                await _db.History.InsertOneAsync(lend);
            }
        }

        public Task DeleteFriendHistory(Guid userId, Guid friendId)
        {
            return _db.History.DeleteManyAsync(h => h.UserId == userId && h.FriendId == friendId);
        }

        public Task DeleteHistLend(Guid userId, Guid id)
        {
            return _db.History.DeleteOneAsync(h => h.UserId == userId && h.Id == id);
        }

        public Task DeleteThingHistory(Guid userId, Guid thingId)
        {
            return _db.History.DeleteManyAsync(h => h.UserId == userId && h.ThingId == thingId);
        }

        public Task DeleteUserHistory(Guid userId)
        {
            return _db.History.DeleteManyAsync(h => h.UserId == userId); 
        }

        public async Task<IDictionary<HistoricalLend, Thing>> GetFriendHistLends(Guid userId, Guid friendId)
        {
            var result = new Dictionary<HistoricalLend, Thing>();
            using (var cursor = await _db.History.FindAsync(h => h.UserId == userId && h.FriendId == friendId))
            {
                while (await cursor.MoveNextAsync())
                {
                    var lends = cursor.Current;
                    foreach (var lend in lends)
                    {
                        var thing = await _db.Things.FindAsync(t => t.Id == lend.ThingId);
                        result.Add(lend, thing.FirstOrDefault());
                    }
                }
            }
            return result;
        }

        public async Task<HistoricalLend> GetHistLend(Guid userId, Guid id)
        {
            var result = await _db.History.FindAsync(h => h.UserId == userId && h.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<HistoricalLend>> GetHistLends(Guid userId)
        {
            var result = await _db.History.FindAsync(h => h.UserId == userId);
            return result.ToEnumerable();
        }

        public async Task<IDictionary<HistoricalLend, Friend>> GetThingHistLends(Guid userId, Guid thingId)
        {
            var result = new Dictionary<HistoricalLend, Friend>();
            using (var cursor = await _db.History.FindAsync(h => h.UserId == userId && h.ThingId == thingId))
            {
                while (await cursor.MoveNextAsync())
                {
                    var lends = cursor.Current;
                    foreach (var lend in lends)
                    {
                        var friend = await _db.Friends.FindAsync(t => t.Id == lend.FriendId);
                        result.Add(lend, friend.FirstOrDefault());
                    }
                }
            }
            return result;
        }

        public Task UpdateHistLend(Guid userId, HistoricalLend lend)
        {
            var update = Builders<HistoricalLend>.Update
                .Set(h => h.FriendId, lend.FriendId)
                .Set(h => h.ThingId, lend.ThingId)
                .Set(h => h.LendDate, lend.LendDate)
                .Set(h => h.ReturnDate, lend.ReturnDate)
                .Set(h => h.Comment, lend.Comment);
            return _db.History.UpdateOneAsync(h => h.UserId == userId && h.Id == lend.Id, update);
        }
    }
}
