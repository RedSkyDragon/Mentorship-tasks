using System;
using System.Collections.Generic;
using MongoDB.Driver;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    public class History : IHistory
    {
        private ThingsBookContext _db;

        public History(ThingsBookContext db)
        {
            _db = db;
        }

        public void CreateHistLend(HistoricalLend lend)
        {
            _db.History.InsertOne(lend);
        }

        public void DeleteHistLend(Guid id)
        {
            _db.History.FindOneAndDelete(h => h.Id == id);
        }

        public HistoricalLend GetHistLend(Guid id)
        {
            return _db.History.Find(h => h.Id == id).FirstOrDefault();
        }

        public IEnumerable<HistoricalLend> GetHistLends(Guid userId)
        {
            return _db.History.Find(h => h.UserId == userId).ToEnumerable();
        }

        public void UpdateHistLend(HistoricalLend lend)
        {
            var update = Builders<HistoricalLend>.Update
                .Set(h => h.UserId, lend.UserId)
                .Set(h => h.FriendId, lend.FriendId)
                .Set(h => h.ThingId, lend.ThingId)
                .Set(h => h.LendDate, lend.LendDate)
                .Set(h => h.ReturnDate, lend.ReturnDate)
                .Set(h => h.Comment, lend.Comment);
            _db.History.UpdateOne(h => h.Id == lend.Id, update);
        }
    }
}
